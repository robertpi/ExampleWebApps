namespace HappyShopper.Web.Controllers

open System
open System.Web
open System.Web.Mvc
open Raven.Client
open Raven.Client.Document
open Strangelights.FsRavenDbTools.DocumentStoreExt
open HappyShopper.Model
open HappyShopper.Core

/// Handles adding and removing stuff from the users shopping basket
[<HandleError>]
type BasketController(storeContainter: IRavenContainer) =
    inherit Controller()
    // helper functions
    let getUserId() =
        HttpContext.Current.Items.["user-id"] |> string
    let getBasket (session: IDocumentSession) (basketId: string) =
        session.Load<Basket>(basketId) :> obj

    /// renders the basket
    member x.Index () : ActionResult =
        let basketId = getUserId()
        let store = storeContainter.GetStore()
        use session = store.OpenSession()
        let basket = getBasket session basketId
        let basket =
            if basket = null then
                { Id = basketId; BasketItems =  [|  |] } :> obj
            else
                basket
        x.ViewData.["Basket"] <- basket
        x.View() :> ActionResult

    /// adds an item to the basket
    member x.Add (id: string) : ActionResult =
        let basketId = getUserId()
        let store = storeContainter.GetStore()
        use session = store.OpenSession()
        let basket = getBasket session basketId
        let product = session.Load<Product>("products/" + id)
        let newItem = { Product = product; Quantity = 1}
        let newBasket =
            match basket with
            | null -> { Id = basketId; BasketItems =  [| newItem |] }
            | :? Basket as oldBasket ->
                session.Advanced.Evict oldBasket
                // do nothing if item already exists, could show user an error
                if oldBasket.BasketItems |> Seq.exists (fun item -> item.Product = product) then
                    oldBasket
                else
                    { oldBasket with BasketItems =  [| yield newItem; yield! oldBasket.BasketItems |] }
            | _ -> failwith "unreashable case"
        session.Store(newBasket)
        session.SaveChanges()
        x.RedirectToAction("Index") :> ActionResult

    /// removes an item from the basket
    member x.Remove (id: string) : ActionResult =
        let productId = "products/" + id
        let basketId = getUserId()
        let store = storeContainter.GetStore()
        use session = store.OpenSession()
        let basket = getBasket session basketId
        let newBasket =
            match basket with
            | null -> { Id = basketId; BasketItems =  [| |] }
            | :? Basket as oldBasket ->
                session.Advanced.Evict oldBasket
                // do nothing if item already exists, could show user an error
                { oldBasket with BasketItems =  oldBasket.BasketItems |> Seq.filter (fun item -> item.Product.Id <> productId) |> Seq.toArray }
            | _ -> failwith "unreashable case"
        session.Store(newBasket)
        session.SaveChanges()
        x.RedirectToAction("Index") :> ActionResult

    // updates the quantities
    member x.Update (postValues: FormCollection) : ActionResult =
        let basketId = getUserId()
        let store = storeContainter.GetStore()
        use session = store.OpenSession()
        let basket = getBasket session basketId
        let newBasket =
            match basket with
            | null -> { Id = basketId; BasketItems =  [| |] }
            | :? Basket as oldBasket ->
                session.Advanced.Evict oldBasket
                let updateBasketItem item =
                    let newQuantString = postValues.["Quantity_" + item.Product.GetShortId()]
                    let result, value = Int32.TryParse newQuantString
                    if result then
                        { item with Quantity = value }
                    else
                        item
                { oldBasket with BasketItems =  oldBasket.BasketItems |> Seq.map updateBasketItem |> Seq.toArray }
            | _ -> failwith "unreashable case"
        session.Store(newBasket)
        session.SaveChanges()
        x.RedirectToAction("Index") :> ActionResult
