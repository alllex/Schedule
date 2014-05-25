module Tests

open SEE
open ScheduleData

let a = "D:\Desktop\studies\Schedule\3.xls"
let b = Importer.Import(a)


printfn "%A" b.Table