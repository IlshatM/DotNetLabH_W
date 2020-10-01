namespace Mandelbrot_set_ETO.Mac
module Program =

    open System
    open Mandelbrot_set_ETO

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platforms.Mac64)
        app.Run(new MainForm())
        0