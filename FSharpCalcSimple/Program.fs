namespace HW2

    open System
    open System.Diagnostics.CodeAnalysis
    open System.Linq.Expressions
    open System.Text
    open System.Threading
    open FSharp.Data       
    module Calculator = 
        let private CreateAnswer(response:HttpResponseWithStream) =                
            match response.StatusCode with
            | 404 -> "Wrong address!"
            | 400 -> "Invalid expression!"
            | 200 -> response.Headers.["calculator_result"]
            | _ -> "Unknown error"


            
        let private ConvertExpression(expression:string) =
            let s = expression.Replace("+", "%2B").Replace("*", "%2A").Replace("/", "%2F")
            s
        let public Calculate(expression:string) =
                
            let a = expression|>ConvertExpression
            let url = "https://localhost:5001/calculate?expression="+a
            let response = Http.RequestStream(url, silentHttpErrors=true)
            let res = CreateAnswer(response)
            res
            



    [<ExcludeFromCodeCoverage>]            
    module Program =
        [<EntryPoint>]
        let main argv =
            printfn "%s" (Calculator.Calculate("2*2+4+6"))
            0
