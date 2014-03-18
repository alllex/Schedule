
#r "Microsoft.Office.Interop.Excel.dll"
#load "Program.fs"
open Microsoft.Office.Interop.Excel
open SEE
open System.IO

let data = Importer.Import(@"M:\projects\Experiments\FSharp\Schedule\SEE\SheduleSample.xlsx")
Exporter.Export(@"M:\projects\Experiments\FSharp\Schedule\SEE\SheduleSampleExported.xlsx", data)