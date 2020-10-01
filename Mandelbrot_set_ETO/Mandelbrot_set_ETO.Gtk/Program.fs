namespace Mandelbrot_set_ETO.Gtk
module Program =

    open System
    open Mandelbrot_set_ETO

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) = 
        let app = new Eto.Forms.Application(Eto.Platforms.Gtk)
        app.Run(new MainForm())
        0