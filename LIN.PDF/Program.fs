namespace LIN.PDF
#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open System.Configuration

module Program =

    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        let key = builder.Configuration["eo:key"]
        builder.Services.AddSwaggerGen()
        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()
        EO.WebBrowser.Runtime.AddLicense(key);
        EO.Pdf.Runtime.AddLicense(key);
        app.UseAuthorization()
        app.UseSwagger()
        app.UseSwaggerUI()
        app.MapControllers()

        app.Run()

        exitCode
