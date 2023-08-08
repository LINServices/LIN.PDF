namespace LIN.PDF.Controllers
#nowarn "20"
open LIN.PDF.Models
open Microsoft.AspNetCore.Mvc

[<Route("[controller]")>]
type ConvertController()=
    inherit Controller()

    [<HttpPost>]
    member this.Convert([<FromBody>] html : string) : IActionResult =
        if html = null then
            this.StatusCode(400, { sizeMB = 0m; file = [||]})
        else
            let mutable response: byte array = [||]
            response <- (LIN.PDF.Controllers.Conversors.convertir html)
            let size = response |> LIN.PDF.Controllers.Conversors.sizeOf 
            this.StatusCode(200, { sizeMB = size; file = response } )
        