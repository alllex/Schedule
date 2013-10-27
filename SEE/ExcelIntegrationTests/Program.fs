open ExcelDna.Integration

[<ExcelFunction(Description="Increment")>]
let dnaInc x = x + 1

[<ExcelFunction(Description="Change string in starnge way")>]
let dnaChangeStr s = "[prefix<" + s + ">postfix]"

[<ExcelFunction(Description="Multiply numbers")>]
let dnaMult x y = x * y

// take an array as argument
[<ExcelFunction(Description="Sum array elements")>]
let dnaSumArr (xs : double[]) = Array.fold (+) 0.0 xs   