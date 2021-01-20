open System.IO

let closest v = if v < 127uy then 0uy else 255uy //explicit bytes

let oneBitize =
    let rec errorDiffuse error acc audio = 
        match audio with
        | [] -> acc |> List.rev
        | x::xrest -> 
            let close = closest (x + error)
            let error = close - x
            errorDiffuse error (close::acc) xrest
    errorDiffuse 0uy []

[<EntryPoint>]
let main argv = 
    if File.Exists argv.[0] then
        let file = File.ReadAllBytes argv.[0]
        printfn "Doing..."
        let newFile = file |> Array.toList |> oneBitize
        File.WriteAllBytes(argv.[0] + ".new", newFile |> List.toArray)
        printf "Done!"
        0
    else
        printf "File doesn't exist"
        0