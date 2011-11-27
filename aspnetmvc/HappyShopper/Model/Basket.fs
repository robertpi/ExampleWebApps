namespace HappyShopper.Model
open System

type BasketItem =
    { Product: Product
      Quantity: int }
    with
        member x.GetTotalPrice() = 
            (decimal x.Quantity) * x.Product.Price

type Basket =
    { mutable Id: string
      BasketItems: BasketItem[] }
    with
        member x.GetTotalPrice() = 
            x.BasketItems |> Seq.sumBy (fun x -> x.GetTotalPrice())
