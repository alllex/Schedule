
namespace SEE

open Microsoft.Office.Interop.Excel
open System.IO
open ScheduleData

type TransferData = (string [,] * string) []

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
            let worksheets = [| for x in workbook.Worksheets -> 
                                    let ws = x :?> Worksheet
                                    getWorksheet ws, ws.Name
                             |]
            app.Quit()
            worksheets
    end

type Exporter =
    class
        static member VerticalOffset = 2 //2 extra rows for group & subgroup numbers
        static member HorizontalOffset = 2 //2 extra columns for weekday and time

        static member ExportFromData(path : string, data : TransferData) =
            let app = new ApplicationClass(Visible = false)
            let workbook = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet)
            Array.iter
                (fun (table, name) ->
                    let ws = workbook.Worksheets.Add() :?> Worksheet
                    ws.Name <- name
                    for i in 1..Array2D.length1 table do
                        for j in 1..Array2D.length2 table do
                            (ws.Cells.[i, j] :?> Range).Value2 <- table.[i - 1, j - 1]
                ) data
            workbook.SaveAs(Filename = path)
            app.Quit()

        static member Export(path : string, data : sClassesSchedule) =
            let numberOfSheets = data.Tables.Length
            let currentTable = data.Tables.[0]
            let numberOfDays = 
                let mutable counter = 1
                let mutable day = data.TimeLine.[0].Day
                for i in 1 .. data.TimeLine.Length - 1 do
                    if day <> data.TimeLine.[i].Day then counter <- counter + 1
                                                         day <- data.TimeLine.[i].Day
                counter
            //не учтены специализации: непонятно, откуда взять подгруппы
            let groups = [|for i in data.Groups -> i.Value |]
            let numberOfGroups = groups.Length
            let numberOfColumns = numberOfGroups + Exporter.HorizontalOffset
            let numberOfRows = data.TimeLine.Length + Exporter.VerticalOffset 
            let list = Array2D.create numberOfRows numberOfColumns "" //empty table (1 sheet)

            if data.TimeLine.Length > 0 then
                let currentDay = ref data.TimeLine.[0].Day
                let writeTimes i (x : sClassTime) = 
                    list.[i + Exporter.VerticalOffset, 1] <- ClassTime.ClassIntervals.[x.Number]

                    if x.Day <> !currentDay then 
                        list.[i + Exporter.VerticalOffset, 0] <- x.Day.ToString()
                        currentDay.Value <- x.Day
            
                list.[Exporter.VerticalOffset, 0] <- currentDay.Value.ToString() 
                Array.iteri writeTimes data.TimeLine

            if currentTable.Groups.Length > 0 then
                let currentSpecialization = ref currentTable.Groups.[0].Specialization
                let writeGroups i (x : sGroup) = 
                    list.[1, i + Exporter.HorizontalOffset] <- currentTable.Groups.[i].Name

                    if x.Specialization <> !currentSpecialization then 
                        list.[0, i + Exporter.HorizontalOffset] <- x.Specialization.Name
                        currentSpecialization.Value <- x.Specialization
            
                list.[0, Exporter.HorizontalOffset] <- currentSpecialization.Value.Name
                Array.iteri writeGroups currentTable.Groups

            if currentTable.Table.Length > 0 then
                let writeColumn i (x : sClassRecord[]) =
                    let writeClasses j (x : sClassRecord) =
                        list.[i + Exporter.VerticalOffset, j + Exporter.HorizontalOffset] <-
                            if x <> null then 
                                x.Subject.Name + "\n" + x.Lecturer.Name + "\n" + x.Classroom.Name
                            else ""
                    Array.iteri writeClasses x

                Array.iteri writeColumn currentTable.Table
                         
        
            Exporter.ExportFromData(path, [|list, currentTable.YearOfStudy.ToString()|])

    end