// Learn more about F# at http://fsharp.org

open System
open System.Collections.Generic
let removeAt index input =
  input 
  |> List.mapi (fun i el -> (i <> index, el)) 
  |> List.filter fst |> List.map snd
let stringFromCharList (cl : char list) = //+
    String.concat "" <| List.map string cl
let ClearString(s:string):string = //+
    let chars = Seq.toList s
    let mutable pure_str = ""
    let pure_chars = List.filter(fun x->Char.IsDigit(x)=true || x='+' || x='-' || x='*' || x='/') chars
    stringFromCharList(pure_chars)
let Split (str:string) (ch:char) = str.Split([|ch|], StringSplitOptions.None) |> Array.toList//+
let GetNumber(s:string):double = //+
    s|>double
let NumberDev(s:string):double = 
    let str = Split s '/'
    let mutable res = GetNumber(str.Head)
    let str = removeAt 0 str
    List.iter(fun x->(res<-res/GetNumber(x))) str
    res
let NumberMult(s:string):double =
    let str = Split s '*'
    let mutable res = NumberDev(str.Head)
    let str = removeAt 0 str
    List.iter(fun x->(res<-res*NumberDev(x))) str
    res    
let NumberMinus(s:string) =
    let str = Split s '-'
    let mutable res = NumberMult(str.Head)
    let str = removeAt 0 str
    List.iter(fun x->(res<-res-NumberMult(x))) str
    res        
let NumberPlus(s_raw:string) =
    let s = ClearString(s_raw)
    let str = Split s '+'
    let mutable res = 0.0
    List.iter(fun x->(res<-res+NumberMinus(x))) str
    res

    
    
[<EntryPoint>]
let main argv =
    printfn "%f" (NumberPlus("3/2"))
    0 // return an integer exit code
