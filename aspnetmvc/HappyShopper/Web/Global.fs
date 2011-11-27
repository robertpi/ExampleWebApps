namespace HappyShopper.Core

open System
open System.Web
open System.Web.Mvc
open System.Web.Routing
open Raven.Client.Indexes
open Raven.Abstractions.Indexing
open Raven.Client.Document
open Raven.Abstractions.Indexing
open Raven.Abstractions.Data
open Strangelights.FsRavenDbTools.DocumentStoreExt

type Route = { 
    controller : string
    action : string
    id : UrlParameter }

type Global() =
    inherit System.Web.HttpApplication() 



    static member RegisterRoutes(routes:RouteCollection) =
        // registers the routes
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            { controller = "Home"; action = "Index"; id = UrlParameter.Optional } // Parameter defaults
            )

    member x.Start() =
        // initalize ravendb's indexes
        let assem = System.Reflection.Assembly.Load("WebHost")
        let store = DocumentStore.OpenInitializedStore()
        IndexCreation.CreateIndexes(assem, store)

        // initalize unity
        UnityBootstrapper.Initialise store

        // regiser the routes
        AreaRegistration.RegisterAllAreas()
        Global.RegisterRoutes(RouteTable.Routes)

