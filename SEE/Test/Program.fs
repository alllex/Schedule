open Editor.Models
open SEE

let a = sClassesSchedule.Load("C:\Users\Lada\Documents\Alex\Schedule.sch")

Exporter.Exp("D:\Desktop\studies\Schedule\1.xlsx", a)