open System.Text.RegularExpressions

let FormatFunction (s : string) =
    let reg1 = new Regex("\r\n") // находит /r/n в строке
    let reg2 = new Regex("\s{2,}") // находит пробел 2 и более раз
    let reg3 = new Regex("_{2,}") // находит нижнее подчёркивание 2 и более раз
    let reg4 = new Regex("_") // находит одно нижнее подчёркивание
     
    let insert (c : char) (reg : Regex) s = 
        let arr = reg.Split(s)
        let s = Array.fold (fun acc x -> acc + c.ToString() + x) "" arr
        s.Substring(1, (s.Length - 1))

    let s1 = s.ToUpper()
        
    let s2 = insert ' ' reg1 s1 // замена переноса на пробел
    let s3 = insert ' ' reg2 s2 // замена кучи пробелов на пробел
    let s4 = insert '_' reg3 s3 // замена кучи подчёрков на подчёрк
    Array.toList(reg4.Split(s4))
    
let test = "Алгоритмы и анализ              сложности (пр. з.)
проф. Косовская Т.М.
_____________
      Математический анализ (лекция) доц. Додонов Н.Ю.
2522"
printfn "%A" (FormatFunction test)