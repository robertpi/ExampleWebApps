
module HappyShopper.Products

open HappyShopper.Model


let products =
    [| { Id = null
         Name = "Ginger Cat"
         Price = 10.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/cat_ginger.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Cat" |] }
       { Id = null
         Name = "Norwegian Forest Cat"
         Price = 15.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/Cat_Norwegian_forest.jpg"
                    ImageType = ImageType.FullSized }
                  { Position = 0
                    ImageUrl = "/images/Cat_Norwegian_forest_thumb.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Cat" |] }
       { Id = null
         Name = "Siamese Cat"
         Price = 100.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/cat_siamese.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Cat" |] }
       { Id = null
         Name = "Tabby"
         Price = 5.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/cat_tabby.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Cat" |] }
       { Id = null
         Name = "Border Collie Dog"
         Price = 25.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/Dog_Border_Collie.jpg"
                    ImageType = ImageType.FullSized }
                  { Position = 0
                    ImageUrl = "/images/Dog_Border_Collie_thumb.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Dog" |] }
       { Id = null
         Name = "Golden Retriever Dog"
         Price = 50.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/Dog_Golden_Retriever.jpg"
                    ImageType = ImageType.FullSized }
                  { Position = 0
                    ImageUrl = "/images/Dog_Golden_Retriever_thumb.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Dog" |] }
       { Id = null
         Name = "Scottish Terrier Dog"
         Price = 20.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/Dog_ScottishTerrier.jpg"
                    ImageType = ImageType.FullSized }
                  { Position = 0
                    ImageUrl = "/images/Dog_ScottishTerrier_thumb.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "" |] }
       { Id = null
         Name = "Whippet"
         Price = 15.0m
         IsActive = true
         ProductImages =
               [| { Position = 0
                    ImageUrl = "/images/Dog_WhippetWhiteSaddled.jpg"
                    ImageType = ImageType.FullSized }
                  { Position = 0
                    ImageUrl = "/images/Dog_WhippetWhiteSaddled_thumb.jpg"
                    ImageType = ImageType.Thumbnail } |]
         ProductCategories = [| "Dog" |] } |]

open Raven.Client.Document
open Raven.Abstractions.Indexing
open Raven.Abstractions.Data
open Strangelights.FsRavenDbTools.DocumentStoreExt

let saveProducts() =
    use store = DocumentStore.OpenInitializedStore()
    use session = store.OpenSession()
    for product in products do
        session.Store product
    session.SaveChanges()

saveProducts()
