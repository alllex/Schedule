
#r "Microsoft.Office.Interop.Excel.dll"
open Microsoft.Office.Interop.Excel

// Run Excel as a visible application
let app = new ApplicationClass(Visible = false) 

// Create new file and get the first worksheet
let workbook = app.Workbooks.Open(@"C:\a.xlsx")
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

open System.IO

let out = new StreamWriter(fileName)

for i in 0..rowCount - 1 do
    for j in 0..colCount - 1 do
        out.Write(data.[i, j])
    out.WriteLine()

out.Close()