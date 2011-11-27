namespace HappyShopper.Core
open System
open Raven.Client

/// interface to hold a reference to the RavenDb IDocumentStore
type IRavenContainer =
    abstract GetStore: unit -> IDocumentStore 

/// the container that will hold the IDocumentStore 
type RavenContainer(store) =
    interface IRavenContainer with
        member x. GetStore() = store
    interface IDisposable with
        member x.Dispose() = store.Dispose()
