﻿namespace HW2

    open System
    open System.Diagnostics.CodeAnalysis
    open System.Linq.Expressions
    open System.Text
    open System.Threading
    open FSharp.Data
    

    type AsyncMaybeBuilder () =
        member this.Bind(x, f) =
            async {
                let! _x = x
                match _x with
                | Some s -> return! f s
                | None -> return None
                }

        member this.Return(x:'a option) =
            async{return x}
    module Calculator =
        let maybe_async = new AsyncMaybeBuilder()
        let private CreateAnswerAsync(response) =
            async{
                return
                    match response.StatusCode with
                    | 404 -> None
                    | 400 -> "Invalid expression!"|>Some
                    | 200 -> response.Headers.["calculator_result"]|>Some
                    | _ -> None
            }


            
        let private ConvertExpression(expression:string) =
            let s = expression.Replace("+", "%2B").Replace("*", "%2A").Replace("/", "%2F")
            s
        let private GetRequest(url) =
            async{
                let! res = Http.AsyncRequestStream(url, silentHttpErrors=true)
                let res = res|>Some
                return res
                
            }
        let public Calculate(expression:string) =
                maybe_async{
                    let a = expression|>ConvertExpression
                    let url = "https://localhost:5001/calculate?expression="+a
                    let! response = GetRequest(url)
                    let! res = CreateAnswerAsync(response)
                    return res|>Some
                }
        


    module Solving=
        let public Solve expression proxy =
            let res = expression|>proxy|>Async.RunSynchronously 
            match res with
            |Some s -> s
            |None -> "Unknown error"
    
    [<ExcludeFromCodeCoverage>]            
    module Program =
        [<EntryPoint>]
        let main argv =
            let proxy_Func = Solving.Solve "2+3" 
            printfn "%s" (proxy_Func Calculator.Calculate)
            0
