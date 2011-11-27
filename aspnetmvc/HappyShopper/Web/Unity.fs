module HappyShopper.Core.UnityBootstrapper
open Unity.Mvc3
open Microsoft.Practices.Unity
open System.Web.Mvc;
open HappyShopper.Core

let private BuildUnityContainer store =
    // create the container
    let container = new UnityContainer()

    // register the services
    container.RegisterInstance<IRavenContainer>(new RavenContainer(store)) |> ignore

    // register the controllers
    container.RegisterControllers()

    // return the container
    container


let Initialise(store) =
    let container = BuildUnityContainer store
    DependencyResolver.SetResolver(new UnityDependencyResolver(container));


