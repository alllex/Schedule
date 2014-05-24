
namespace SEE

open Microsoft.Office.Interop.Excel
open System.IO
open System.Collections.Generic
open ScheduleData

type TransferData = (string [,] * string) []

type Importer = 
    class
    (*
        static member VerticalOffset = 2 //2 extra rows for group & subgroup numbers
        static member HorizontalOffset = 2 //2 extra columns for weekday and time

        static member ImportToData(path : string) : TransferData = 
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

        static member Import(path : string) = 
            let mutable id = 0
            let data = Importer.ImportToData(path)
            let namesOfSheets = Array.map snd data
            let tables = Array.map fst data
            let yearsOfStudy = new Dictionary<int, sYearOfStudy>()
            let specializations = new Dictionary<int, sSpecialization>()
            let subjects = new Dictionary<int, sSubject>()
            let classrooms = new Dictionary<int, sClassroom>()
            let lecturers = new Dictionary<int, sLecturer>()
            let groups = new Dictionary<int, sGroup>()
            let classesTables = Array.create data.Length (new sClassesTable(0, new sYearOfStudy(0, ""), [|[|new sClassRecord(0, new sSubject(0, ""), new sLecturer(0, "", "", ""), new sClassroom(0, "", ""))|]|], [|new sGroup(0, "", new sSpecialization(0, ""), new sYearOfStudy(0, ""))|]))

            let days = [| for i in Importer.VerticalOffset .. Array2D.length1 tables.[0] - 1 - Importer.VerticalOffset -> tables.[0].[i,0] |] // Заполняю timeline
            let mutable iday = 0
            let timeLine = Array.create (Array2D.length1 tables.[0] - Importer.VerticalOffset) (new sClassTime(id, Weekdays.Monday, 0))
            for i in 0 .. Array2D.length2 tables.[0] - 1 do //бежим по таймлайну ПОМЕНЯЛА в Length 1 на 2!
                if days.[i] <> "" then iday <- i
                let day = match days.[iday] with
                            | "Monday" -> Weekdays.Monday 
                            | "Tuesday" -> Weekdays.Tuesday 
                            | "Wednesday" -> Weekdays.Wednesday
                            | "Thursday" -> Weekdays.Thursday
                            | "Friday" -> Weekdays.Friday 
                            | "Saturday" -> Weekdays.Saturday
                            //| "Воскресенье" -> Sunday
                            | _ -> failwith "Incorrect weekday"
                timeLine.[i] <- new sClassTime(id, day, i % ClassTime.ClassIntervals.Length)
                id <- id + 1
           
            for sheet in 0 .. data.Length - 1 do //цикл по всем листам 
                let currentTable = tables.[sheet]
                let currentYear = new sYearOfStudy(id, namesOfSheets.[sheet])
                yearsOfStudy.Add(id, currentYear)
                id <- id + 1

                if (Array2D.length1 currentTable > 2) && (Array2D.length2 currentTable > 2) then

                    let groupsArray = Array.create (Array2D.length2 currentTable - Importer.HorizontalOffset) (new sGroup(0, "", new sSpecialization(0, ""), new sYearOfStudy(0, "")))

                    let specs = [| for i in Importer.HorizontalOffset .. Array2D.length2 currentTable - 1 -> currentTable.[0,i] |]
                    let mutable isp = 0
                    for i in 0 .. Array2D.length2 currentTable - 1 - Importer.HorizontalOffset do //ПОМЕНЯЛА В LENGTH 2 НА 1 !!! бежим по группам, добавляем в словарь группы и специализации 
                        if specs.[i] <> "" then isp <- i //с учетом необъединенных ячеек
                        let sp = new sSpecialization(id, specs.[isp])
                        specializations.Add(id, sp)
                        id <- id + 1
                        groups.Add(id, new sGroup(id, currentTable.[1,i + Importer.HorizontalOffset], sp, currentYear))
                        groupsArray.[i] <- new sGroup(id, currentTable.[1,i + Importer.HorizontalOffset], sp, currentYear)
                        id <- id + 1

                    let table = Array.create (Array2D.length1 currentTable - Importer.VerticalOffset) (Array.create (Array2D.length2 currentTable - Importer.HorizontalOffset) (new sClassRecord(0, new sSubject(0, ""), new sLecturer(0, "", "", ""), new sClassroom(0, "", ""))))
                    for i in Importer.HorizontalOffset .. Array2D.length1 currentTable - 1 - Importer.HorizontalOffset do
                        for j in Importer.VerticalOffset .. Array2D.length2 currentTable - 1 - Importer.VerticalOffset do //ПОМЕНЯЛА 1 И 2 МЕСТАМИ!
                            let card = currentTable.[i,j]
                            if card <> "" then
                                let subj = card.Substring(0, card.IndexOf('\n'))
                                let rest = card.Substring(card.IndexOf('\n') + 1, card.Length - card.IndexOf('\n') - 1)
                                let lect = rest.Substring(0, rest.IndexOf('\n'))
                                let room = rest.Substring(rest.IndexOf('\n') + 1, rest.Length - 1 - rest.IndexOf('\n')) //разбили card на предмет, лектора и аудиторию
                        
                                let subject = new sSubject(id, subj)
                                subjects.Add(id, subject)
                                id <- id + 1
                                let lecturer = new sLecturer(id, lect, "", "") //НЕТ СТЕПЕНИ И КАФЕДРЫ!
                                lecturers.Add(id, lecturer)
                                id <- id + 1
                                let classroom = new sClassroom(id, room, "") //НЕТ АДРЕСА!
                                classrooms.Add(id, classroom)
                                id <- id + 1
                                table.[i].[j] <- new sClassRecord(id, subject, lecturer, classroom)
                                id <- id + 1
                            else table.[i].[j] <- null
                                 id <- id + 1 

                    classesTables.[sheet] <- new sClassesTable(id, currentYear, table, groupsArray)
                    id <- id + 1
                
            new sClassesSchedule(id, timeLine, groups, lecturers, classrooms, subjects, specializations, yearsOfStudy, classesTables)
            *)
    end

type Exporter =
    class
    (*
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
            let schedule = Array.create numberOfSheets (Array2D.create 0 0 "", "")
            for sheet in 0 .. numberOfSheets - 1 do
                let getSheet sheetNumber =
                    let currentTable = data.Tables.[sheetNumber]
                    let groups = [| for i in data.Groups -> i.Value |]
                    let numberOfGroups = groups.Length
                    let numberOfColumns = numberOfGroups + Exporter.HorizontalOffset
                    let numberOfRows = data.TimeLine.Length + Exporter.VerticalOffset 
                    let table = Array2D.create numberOfRows numberOfColumns "" //empty table (1 sheet)

                    if data.TimeLine.Length > 0 then
                        let currentDay = ref data.TimeLine.[0].Day
                        let writeTimes i (x : sClassTime) = 
                            table.[i + Exporter.VerticalOffset, 1] <- ClassTime.ClassIntervals.[x.Number]

                            if x.Day <> !currentDay then 
                                table.[i + Exporter.VerticalOffset, 0] <- x.Day.ToString()
                                currentDay.Value <- x.Day
            
                        table.[Exporter.VerticalOffset, 0] <- currentDay.Value.ToString() 
                        Array.iteri writeTimes data.TimeLine

                    if currentTable.Groups.Length > 0 then
                        let currentSpecialization = ref currentTable.Groups.[0].Specialization
                        let writeGroups i (x : sGroup) = 
                            table.[1, i + Exporter.HorizontalOffset] <- currentTable.Groups.[i].Name

                            if x.Specialization <> !currentSpecialization then 
                                table.[0, i + Exporter.HorizontalOffset] <- x.Specialization.Name
                                currentSpecialization.Value <- x.Specialization
            
                        table.[0, Exporter.HorizontalOffset] <- currentSpecialization.Value.Name
                        Array.iteri writeGroups currentTable.Groups

                    if currentTable.Table.Length > 0 then
                        let writeColumn i (x : sClassRecord[]) =
                            let writeClasses j (x : sClassRecord) =
                                table.[i + Exporter.VerticalOffset, j + Exporter.HorizontalOffset] <-
                                    if x <> null then 
                                        x.Subject.Name + "\n" + x.Lecturer.Name + "\n" + x.Classroom.Name
                                    else ""
                            Array.iteri writeClasses x
                        Array.iteri writeColumn currentTable.Table
                         
                    table, currentTable.YearOfStudy.Name + " курс"            

                schedule.[numberOfSheets - 1 - sheet] <- getSheet sheet

            Exporter.ExportFromData(path, schedule)
                         *)
    end