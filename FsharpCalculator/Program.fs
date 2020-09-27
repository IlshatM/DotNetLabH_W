open System
open System.Collections.Generic
let stringFromCharList (cl : char list) = 
    String.concat "" <| List.map string cl
let ClearString(s:string):string = 
    let chars = Seq.toList s
    let pure_chars = List.filter(fun x->Char.IsDigit(x)=true || x='+' || x='-' || x='*' || x='/') chars
    stringFromCharList(pure_chars)
let Split (str:string,ch:char) = str.Split([|ch|], StringSplitOptions.None) |> Array.toList
let GetNumber(s:string):double = 
    s|>double
let bindPlus(s:string) =
    (s, '+')
let Calculate(expression:string) =
    expression
            |> ClearString
            |> bindPlus
            |> Split 
            |> List.map(fun t->Split(t, '-'))
            |> List.map(fun t -> List.map(fun t1->Split(t1,'*'))t) 
            |> List.map(fun t->List.map(fun t1 -> List.map(fun t2 -> Split(t2, '/'))t1)t)
            |> List.map(fun t->List.map(fun t1->List.map(fun t2->List.map(fun t3->GetNumber(t3))t2)t1)t)
            |> List.map(fun t->List.map(fun t1->List.map(fun t2->List.reduce(/) t2)t1)t)
            |> List.map(fun t1->List.map(fun t2->List.reduce(*) t2)t1)
            |> List.map(fun t2->List.reduce(-) t2)
            |> List.reduce(+)
    
            
        
    


[<EntryPoint>]
let main argv =
    printfn "%f" (Calculate("2+3*2 - 11/11 "))
    
    

    0 // return an integer exit code
