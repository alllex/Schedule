

open Microsoft.Office.Interop.Excel
open System.IO

// Run Excel as a visible application
let app = new ApplicationClass(Visible = false) 

// Create new file and get the first worksheet
let workbook = app.Workbooks.Open(@"M:\projects\Experiments\FSharp\Schedule\SEE\ExcelIntegration\bin\Debug\b.xlsx",
                                   Editable = true)
// Note that worksheets are indexed from one instead of zero
let worksheet = (workbook.Worksheets.[1] :?> Worksheet)

let xlRange = worksheet.UsedRange
let rowCount = xlRange.Rows.Count
let colCount = xlRange.Columns.Count

printfn "rowCount %A" rowCount
printfn "colCount %A" colCount

let data = Array2D.create rowCount colCount "*"

for i in 1..rowCount do
    for j in 1..colCount do
        data.[i - 1, j - 1] <- string <| (worksheet.Cells.[i, j] :?> Range).Value2

let fileName = "output.txt"

let out = new StreamWriter(fileName)

for i in 0..rowCount - 1 do
    for j in 0..colCount - 1 do
        out.WriteLine(sprintf "[%A, %A] == %A" i j data.[i, j])

out.Close()

let ws2 = (workbook.Worksheets.[2] :?> Worksheet)

open System

// Store data in arrays of strings or floats
let rnd = new Random()
let titles = [| "No"; "Maybe"; "Yes" |]
let names = Array2D.init 10 1 (fun i _ -> string('A' + char(i)))
let data' = Array2D.init 10 3 (fun _ _ -> rnd.NextDouble())

// Populate data into Excel worksheet
ws2.Range("C2", "E2").Value2  <- titles
ws2.Range("B3", "B12").Value2 <- names
ws2.Range("C3", "E12").Value2 <- data'

workbook.Save()
app.Quit()