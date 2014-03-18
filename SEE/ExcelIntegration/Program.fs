
namespace SEE

open Microsoft.Office.Interop.Excel
open System.IO

type TransferData = (string [,]) list

type Importer = 
    class
        static member Import(path : string) : TransferData = 
            let getWorksheet (worksheet : Worksheet) =
                let usedRange = worksheet.UsedRange
                let rows = usedRange.Rows.Count
                let cols = usedRange.Columns.Count
                printfn "WS: %Ax%A" rows cols
                Array2D.init rows cols
                             (fun i j -> string <| (worksheet.Cells.[i + 1, j + 1] :?> Range).Value2)
            let app = new ApplicationClass(Visible = false)
            let workbook = app.Workbooks.Open(path, Editable = true)
            let worksheetsCount = workbook.Worksheets.Count
            printfn "worksheets count = %A" worksheetsCount
            let worksheets = [ for x in workbook.Worksheets -> 
                                    let ws = x :?> Worksheet
                                    getWorksheet ws
                             ]
            app.Quit()
            worksheets
    end

type Exporter =
    class
        static member Export(path : string, data : TransferData) =
            let app = new ApplicationClass(Visible = false)
            let workbook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet)
            List.iter
                (fun table ->
                    let ws = workbook.Worksheets.Add() :?> Worksheet
                    for i in 1..Array2D.length1 table do
                        for j in 1..Array2D.length2 table do
                            (ws.Cells.[i, j] :?> Range).Value2 <- table.[i - 1, j - 1]
                ) data
            workbook.SaveAs(Filename = path)
            app.Quit()
    end