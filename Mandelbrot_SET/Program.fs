

open System
open Eto.Drawing
open Eto.Forms
type MyForm() as this =
    inherit Form()
    do
        this.Title      <- "My Cross-Platform App"
        this.ClientSize <- Size (200, 200)
        this.Content    <- new Label(Text = "Hello F# World!")

let app = new Application()
let form = new MyForm()
form.Show()
[<EntryPoint>]
let main argv =
    form.Show()
    0 // return an integer exit code
