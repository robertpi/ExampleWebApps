namespace HappyShopper.Model
open System

type ImageType =
    | FullSized = 0
    | Thumbnail = 1

type ProductImage =
    { Position: int
      ImageUrl: string
      ImageType: ImageType }

type Product =
    { mutable Id: string
      Name: string
      Price: Decimal
      IsActive: bool
      ProductImages: ProductImage[]
      ProductCategories: string[] }
      with

        member x.GetThumbnailUrl() =
            match x.ProductImages |> Seq.tryFind (fun x -> x.ImageType = ImageType.Thumbnail) with
            | Some image -> image.ImageUrl
            | None -> "" // TODO add default no image url

        member x.GetMainPictureUrl() =
            match x.ProductImages |> Seq.tryFind (fun x -> x.ImageType = ImageType.FullSized) with
            | Some image -> image.ImageUrl
            | None ->
                match x.ProductImages |> Seq.tryFind (fun x -> true) with
                | Some image -> image.ImageUrl
                | None -> "" // TODO add default no image url

        member x.GetShortId() =
            x.Id.Split('/').[1]
