namespace Mandelbrot_set_ETO.Wpf
module Program =

    open System
    open Mandelbrot_set_ETO

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platforms.Wpf)
        app.Run(new MainForm())
        0