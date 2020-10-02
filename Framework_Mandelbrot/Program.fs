open System.Numerics
open System.Windows.Forms
open System.Windows.Forms
open System.Windows.Forms
open Microsoft.FSharp.Math;;
open System
open System.Drawing
open System.Windows.Forms
open System.Threading

module Mandelbrot =
    let max = complex 1.0 1.0
    let min = complex -1.0 -1.0
    let rec IsInSet(z:complex, c:complex, current_iter, iter) =
        if current_iter<iter && Complex.Abs(z)<=2.0 then
            IsInSet(((z*z)+c), c, current_iter+1, iter)
        else
            current_iter
            

    
    let convertMeasure(x:int, y:int, height_of_window:float, width_of_window:float, scale:float, mx:float,my:float) =
        let toX p =
            (p-(width_of_window/2.0))/(width_of_window/4.0*scale)
        let toY p =
            -(p-(height_of_window/2.0))/(height_of_window/4.0*scale)
        (toX(float x) + mx, toY(float y) + my)
    let colorize c =
        let r = (4 * c) % 255
        let g = (6 * c) % 255
        let b = (8 * c) % 255
        Color.FromArgb(r,g,b)
    let PointToComplex(x,y) =
        complex x y
    let createImage(height, width, accuracy, scale,mx,my) =
        let img = new Bitmap(400,400)
        for x = 0 to width-1 do
            for y = 0 to height-1 do
                let iteration_passed = IsInSet(Complex.zero,PointToComplex(convertMeasure(x,y,float(height), float(width),scale,mx,my)), 0, accuracy)
                if iteration_passed = accuracy then
                    img.SetPixel(x,y,Color.Black)
                else
                    img.SetPixel(x,y,colorize iteration_passed)
        img





[<STAThread>]
let mutable image = Mandelbrot.createImage(400,400,20,1.0,-0.7,0.28)
let temp = new Form()
temp.Size<-Size(400,400)
let picBox = new PictureBox()
picBox.Image <- image
picBox.Size <- (Size(400,400))
temp.Controls.Add(picBox)
let mutable scale = 0.0
let mutable speed = 1.0
let mutable accuracy_scale = 1.0
let timer = new System.Windows.Forms.Timer(Interval = 10)
timer.Tick.Add <| fun _ ->
    image <- Mandelbrot.createImage(400,400,20+int accuracy_scale,(1.0+scale),-0.7,0.28)
    picBox.Image<-image
    scale<-scale+speed
    if int scale % 10 = 0 then speed<-speed*2.0
    accuracy_scale<-accuracy_scale+3.0
    if scale >= 100.0 then
        timer.Stop()
        image <- Mandelbrot.createImage(400,400,1000,100.0,-0.7,0.28)
        picBox.Image<-image


timer.Start()
do Application.Run(temp)

