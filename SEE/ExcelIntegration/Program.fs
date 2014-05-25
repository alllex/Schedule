
namespace SEE

open Microsoft.Office.Interop.Excel
open System.IO
open System.Collections.Generic
open System.Text.RegularExpressions
open ScheduleData

type TransferData = (string [,] * string) []

type Importer = 
    class
    
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
            let data = Importer.ImportToData(path)
            let namesOfSheets = Array.map snd data
            let tables = Array.map fst data
            //let yearsOfStudy = new Dictionary<string, YearOfStudy>()
            //let specializations = new Dictionary<string, Specialization>()
            let subjects = new Dictionary<string, Subject>()
            let classrooms = new Dictionary<string, Classroom>()
            let lecturers = new Dictionary<string, Lecturer>()
            //let groups = new Dictionary<string, Group>()
            //let classesTables = Array.create data.Length null

            let schedule = new Schedule()

            let days = [| for i in Importer.VerticalOffset .. (Array2D.length1 tables.[0] - 1) -> tables.[0].[i,0] |] // Заполняю timeline
            let mutable dayIndex = 0
            let timeLine = Array.create (Array2D.length1 tables.[0] - Importer.VerticalOffset) null
            for i in Importer.VerticalOffset .. Array2D.length1 tables.[0] - 1 do //бежим по таймлайну
                if days.[i - Importer.VerticalOffset] <> "" then dayIndex <- i - Importer.VerticalOffset
                let day = match days.[dayIndex] with
                            | "Monday" -> Weekdays.Monday 
                            | "Tuesday" -> Weekdays.Tuesday 
                            | "Wednesday" -> Weekdays.Wednesday
                            | "Thursday" -> Weekdays.Thursday
                            | "Friday" -> Weekdays.Friday 
                            | "Saturday" -> Weekdays.Saturday
                            //| "Воскресенье" -> Sunday
                            | _ -> failwith "Incorrect weekday"
                let time = new ClassTime(Day = day, Number = (i - Importer.VerticalOffset) % ClassTime.ClassIntervals.Length)
                timeLine.[i - Importer.VerticalOffset] <- time  
                schedule.TimeLine.Add(time) // поменять индексы
           
            for sheet in 0 .. data.Length - 1 do //цикл по всем листам 
                let currentTable = tables.[sheet]
                let currentYear = new YearOfStudy(Name = namesOfSheets.[sheet])
                schedule.YearsOfStudy.Add(currentYear)
                //yearsOfStudy.Add(currentYear.Name, currentYear)

                if (Array2D.length1 currentTable > 2) && (Array2D.length2 currentTable > 2) then
                    let groupsArray = Array.create (Array2D.length2 currentTable - Importer.HorizontalOffset) null 

                    let specs = [| for i in Importer.HorizontalOffset .. Array2D.length2 currentTable - 1 -> currentTable.[0,i] |]
                    let mutable currSpecialization = new Specialization(Name = specs.[0])
                    schedule.Specializations.Add(currSpecialization)
                    for i in Importer.HorizontalOffset .. Array2D.length2 currentTable - 1 do //бежим по группам, добавляем в словарь группы и специализации 
                        if specs.[i - Importer.HorizontalOffset] <> "" then currSpecialization <- new Specialization(Name = specs.[i - Importer.HorizontalOffset]) 
                                                                            schedule.Specializations.Add(currSpecialization)//с учетом необъединенных ячеек
                        
                        let group = new Group(Name = currentTable.[1,i], Specialization = currSpecialization, YearOfStudy = currentYear)
                        //groups.Add(group.Name, group)
                        schedule.Groups.Add(group)
                        groupsArray.[i - Importer.HorizontalOffset] <- group
                        
                    //let table = Array.create (Array2D.length1 currentTable - Importer.VerticalOffset) (Array.create (Array2D.length2 currentTable - Importer.HorizontalOffset) null) //(new sClassRecord(0, new sSubject(0, ""), new sLecturer(0, "", "", ""), new sClassroom(0, "", ""))))
                    let reg = new Regex("^(?<Subject>[a-zA-Zа-яА-Я0-9\s]*)\n(?<Lector>[a-zA-Zа-яА-Я0-9\s]*)\n(?<Room>[a-zA-Zа-яА-Я0-9\s]*)$")
                    
                    for i in Importer.VerticalOffset .. Array2D.length1 currentTable - 1 do
                        for j in Importer.HorizontalOffset .. Array2D.length2 currentTable - 1 do
                            let card = currentTable.[i,j]
                            
                            if (card <> "") then
                                let matches = reg.Match(card)
                                if matches.Success then
                                    let subj = matches.Groups.["Subject"].Value
                                    let lect = matches.Groups.["Lector"].Value
                                    let room = matches.Groups.["Room"].Value //разбили card на предмет, лектора и аудиторию
                        
                                    let subject = if subj = "" then null
                                                  elif subjects.ContainsKey(subj) then 
                                                        subjects.[subj]
                                                  else let subject = new Subject(Name = subj)
                                                       subjects.Add(subject.Name, subject)
                                                       schedule.Subjects.Add(subject)
                                                       subject
                                     
                                    let lecturer = if lect = "" then null
                                                   elif lecturers.ContainsKey(lect) then 
                                                        lecturers.[lect]
                                                   else let lecturer = new Lecturer(Name = lect)
                                                        lecturers.Add(lecturer.Name, lecturer)
                                                        schedule.Lecturers.Add(lecturer)
                                                        lecturer

                                    let classroom = if room = "" then null
                                                    elif classrooms.ContainsKey(room) then 
                                                        classrooms.[room]
                                                    else let classroom = new Classroom(Name = room)
                                                         classrooms.Add(classroom.Name, classroom)
                                                         schedule.Classrooms.Add(classroom)
                                                         classroom

                                    let classRecord = new ClassRecord(Subject = subject, Lecturer = lecturer, Classroom = classroom, Group = groupsArray.[j - Importer.HorizontalOffset], ClassTime = timeLine.[i - Importer.VerticalOffset])
                                    schedule.ClassRecords.Add(classRecord)
                                else let classRecord = new ClassRecord(Subject = new Subject(Name = "Error"), 
                                                                       Lecturer = new Lecturer(Name = "Error"), 
                                                                       Classroom = new Classroom(Name = "Error"), 
                                                                       Group = groupsArray.[j - Importer.HorizontalOffset], 
                                                                       ClassTime = timeLine.[i - Importer.VerticalOffset])
                                     schedule.ClassRecords.Add(classRecord)

                                //table.[i - Importer.VerticalOffset].[j - Importer.HorizontalOffset] <- new sClassRecord(id, subject, lecturer, classroom)
 
            schedule        
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

        static member Export(path : string, data : Schedule) =
            let numberOfSheets = data.YearsOfStudy.Count
            let schedule = Array.create numberOfSheets (Array2D.create 0 0 "", "")
            for sheet in 0 .. numberOfSheets - 1 do
                let getSheet sheetNumber =
                    // Здесь создаем таблицы для групп
                    let currentTable = new GroupClasses(data, data.YearsOfStudy.[sheetNumber])
                    let groups = currentTable.Subjects //[| for i in data.Groups -> i.Value |]
                    let numberOfGroups = groups.Count
 
                    let numberOfColumns = numberOfGroups + Exporter.HorizontalOffset
                    let numberOfRows = data.TimeLine.Count + Exporter.VerticalOffset 
                    let table = Array2D.create numberOfRows numberOfColumns "" //empty table (1 sheet)

                    if data.TimeLine.Count > 0 then
                        let currentDay = ref data.TimeLine.[0].Day
                        let writeTimes i (x : ClassTime) = 
                            table.[i + Exporter.VerticalOffset, 1] <- ClassTime.ClassIntervals.[x.Number]

                            if x.Day <> !currentDay then 
                                table.[i + Exporter.VerticalOffset, 0] <- x.Day.ToString()
                                currentDay.Value <- x.Day
            
                        table.[Exporter.VerticalOffset, 0] <- currentDay.Value.ToString() 
                        Seq.iteri writeTimes data.TimeLine
                   
                    if numberOfGroups > 0 then
                        let currentSpecialization = ref groups.[0].Specialization
                        let writeGroups i (x : Group) = 
                            table.[1, i + Exporter.HorizontalOffset] <- x.Name

                            if x.Specialization <> !currentSpecialization then 
                                table.[0, i + Exporter.HorizontalOffset] <- x.Specialization.Name
                                currentSpecialization.Value <- x.Specialization
            
                        table.[0, Exporter.HorizontalOffset] <- currentSpecialization.Value.Name
                        Seq.iteri writeGroups currentTable.Subjects

                    if currentTable.Subjects.Count > 0 && data.TimeLine.Count > 0 then
                        let writeColumn i j (x : ClassRecord) =
                                table.[i + Exporter.VerticalOffset, j + Exporter.HorizontalOffset] <-
                                    if x <> null then 
                                        x.Subject.Name + "\n" + x.Lecturer.Name + "\n" + x.Classroom.Name
                                    else ""
                        Array2D.iteri writeColumn <| Array2D.init data.TimeLine.Count currentTable.Subjects.Count (fun i j -> currentTable.GetClass(i, j))             
                 
                    table, currentTable.YearOfStudy.Name + " курс" 

                schedule.[numberOfSheets - 1 - sheet] <- getSheet sheet

            Exporter.ExportFromData(path, schedule)
    end