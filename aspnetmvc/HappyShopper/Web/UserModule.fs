namespace HappyShopper.Core

open System
open System.Web

/// used to give each user a cookie with thier user-id, cookie is non-persistant but we could easly change this
type UserModule() =
    // code than checks if the user has a cookie and assigns them one if they don't
    let checkUserCookie _ =
        let userId =
            if HttpContext.Current.Request.Cookies.AllKeys |> Seq.exists (fun x -> x = "user-id") then
                HttpContext.Current.Request.Cookies.["user-id"].Value
            else 
                let newId = Guid.NewGuid().ToString()
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("user-id", newId))
                newId
        HttpContext.Current.Items.Add("user-id", userId)

    // the IHttpModule interface implementation
    interface IHttpModule with
        member x.Init(application: HttpApplication) =
            application.BeginRequest.Add(checkUserCookie)
        member x.Dispose() = ()