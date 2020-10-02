module Tests

open System
open System.Diagnostics.CodeAnalysis
open HW2
open HW2.Calculator
open Xunit



[<Fact>]
let ``2+sometext3 -> 2+3`` () =
    Assert.Equal("2+3",ClearString("2+sometext3"))
[<Fact>]
let ``2+3=5`` () =
    Assert.Equal(5.0,Calculator.Calculate("2+3").Value)
[<Fact>]    
let ``2-3=-1`` () =
    Assert.Equal(-1.0,Calculator.Calculate("2-3").Value)
    
[<Fact>]    
let ``2*3=6`` () =
    Assert.Equal(6.0,Calculator.Calculate("2*3").Value)
[<Fact>]    
let ``6/3=2`` () =
    Assert.Equal(2.0,Calculator.Calculate("6/3").Value)
[<Fact>]    
let ``NoneWhenNoOperator`` () =
    Assert.Equal(None,Calculator.GetAction("63"))
[<Fact>]    
let ``NoneWhenCharIsNotValid`` () =
    Assert.Equal(None,Calculator.calc([6.0;3.0],'%'))
[<Fact>]    
let ``NoneWhenNoSecondNumber`` () =
    Assert.Equal(None,Calculator.CheckValid("2+"))
[<Fact>]
let ``NoneWhenDevideBy0`` () =
    Assert.Equal(None,Calculator.Dev(2.0,0.0))
    
[<Fact>]
let ``Convert string to number`` () =
    Assert.Equal(2.0,Calculator.GetNumber("2"))
    
[<Fact>]
let ``Convert invalid string to number`` () =
    Assert.Throws<FormatException>(fun()->GetNumber("A")|>ignore)|>ignore

