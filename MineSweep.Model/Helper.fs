module MineSweep.Model.FSharp.Helper

open System

let shuffle lst =
    let rng = Random()

    let addElement dict i j newElm =
        match dict |> Map.tryFind j with
        |None -> dict |> Map.add j newElm
        |Some oldElm ->
            dict
            |>Map.add i oldElm
            |>Map.remove j
            |>Map.add j newElm

    let elementFolder dict (i, newElm) =
        let j = rng.Next(i + 1)
        addElement dict i j newElm

    lst
    |>Seq.indexed
    |>Seq.fold elementFolder Map.empty

let cartesianProduct xs ys =
    xs
    |>List.collect
        (fun x -> ys |> List.map (fun y -> struct (x, y)))