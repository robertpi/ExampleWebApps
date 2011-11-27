namespace HappyShopper.Web.Controllers

open System.Web
open System.Web.Mvc
open Raven.Client.Document
open Strangelights.FsRavenDbTools.DocumentStoreExt
open HappyShopper.Model
open HappyShopper.Core

/// handles show the user a list of items and items details
[<HandleError>]
type HomeController(storeContainter: IRavenContainer) =
    inherit Controller()

    /// gets a list of all items (bit silly as we assume the item list is small enough to fit on a page)
    member x.Index () : ActionResult =
        use session = storeContainter.GetStore().OpenSession()
        let products = session.Query<Product>("Products/AllFields")
        x.ViewData.["Products"] <- Array.ofSeq products
        x.View() :> ActionResult

    /// show the details of an item
    member x.Details (id: string) =
        use session = storeContainter.GetStore().OpenSession()
        let product = session.Load<Product>("products/" + id)
        x.ViewData.["Product"] <- product
        x.View() :> ActionResult