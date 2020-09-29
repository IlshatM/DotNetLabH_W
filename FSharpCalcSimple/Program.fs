open System

type MaybeBuilder() =

    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = 
        Some x
   
let maybe = new MaybeBuilder()
let Print(a:double option):unit =
     if a = None then printfn "Invalid input"
     else printfn "%f" (a.Value)
let stringFromCharList (cl : char list) = 
    String.concat "" <| List.map string cl
let ClearString(s:string):string =
    let chars = Seq.toList s
    let pure_chars = List.filter(fun x->Char.IsDigit(x)=true || x='+' || x='-' || x='*' || x='/') chars
    stringFromCharList(pure_chars)

let Split (str:string, ch:char) =
    str.Split([|ch|], StringSplitOptions.None) |> Array.toList
    
let CheckValid(str:string) =
    let a = str.Split([|'+';'-';'*';'/'|], StringSplitOptions.None) |> Array.toList
    if a.Length>2 || a.Length<=1 || a.[0]="" || a.[1]=""
    then
        None
    else
        Some(str)
let GetAction (s:string) =
    if s.Contains "+" then Some('+')
    else if s.Contains "-" then Some('-')
    else if s.Contains "*" then Some('*')
    else if s.Contains "/" then Some('/')
    else None
    
let GetNumber(s:string):double = 
    s|>double
let Dev(a:double, b:double) =
    if b<>0.0 then
        Some(a/b)
    else None

let calc(mas:double list, act:char) =
    match act with
    |'+' -> List.reduce(+) mas |> Some
    |'-' ->List.reduce(-) mas |> Some
    |'*' ->List.reduce(*) mas |> Some
    |'/' ->Dev(mas.[0], mas.[1])
    |_->None
let Calculate(s:string) =
    maybe
        {
        let! pure_str = s |> ClearString |> CheckValid
        let! act = pure_str |> GetAction
        let! double_mas = Split(pure_str,act)|>List.map(fun x->GetNumber(x))|>Some
        let! res = calc(double_mas, act)
        return res
        }

[<EntryPoint>]
let main argv =
    Print(Calculate("2/3"))
    0
