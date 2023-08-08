namespace LIN.PDF.Controllers
open System.Drawing
open EO.Pdf

module Conversors =

    
    // Obtiene el tamaño en MB de un array de bytes
    let sizeOf (array: byte array) = decimal(array.Length) / 1048576m


    // Imagen a Byte Array
    let imageToByteArray (image: System.Drawing.Image) =
        use stream = new System.IO.MemoryStream()
        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        stream.ToArray()


    // Convertir un HTML a PDF
    let convertir (html: string) =
        try
            // Opciones para el motor
            let options = 
                HtmlToPdfOptions(GeneratePageImages = true, SSLVerificationMode = SSLVerificationMode.None, NoScript = false, NoCache = true)

            let leftMargin = 0f
            let topMargin = 0f
            let width = 0f
            let height = 0f
            options.OutputArea <- RectangleF(leftMargin, topMargin, width, height)
            
            // Stream de guardar
            let stream = new System.IO.MemoryStream()

            // Resultado
            let result = HtmlToPdf.ConvertHtml(html, stream, options)

            // Guarda los cambios
            result.PdfDocument.Save(stream)

            // Convierte el pdf a array de bytes
            let bytes = stream.ToArray()

            // Retorna
            (bytes)

        with
        | _ ->
            ([||])
        
    