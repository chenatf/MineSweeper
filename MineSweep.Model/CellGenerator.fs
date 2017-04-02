module MineSweep.Model.FSharp.CellGenerator

open MineSweep.Model

let createMineField count width height =
    let allIdxs =
        let xs = [0..width - 1]
        let ys = [0..height - 1]
        Helper.cartesianProduct xs ys

    let mineIdx =
        allIdxs
        |>Helper.shuffle
        |>Map.toList
        |>List.unzip
        |>snd
        |>List.take count
        |>Set.ofList

    let proximities =
        let getProximities struct (x, y) =
            [
            struct (x-1, y-1)
            struct (x-1, y)
            struct (x-1, y+1)
            struct (x, y-1)
            struct (x, y+1)
            struct (x+1, y-1)
            struct (x+1, y)
            struct (x+1, y+1)
            ]

        mineIdx
        |>Seq.collect getProximities
        |>Seq.countBy id
        |>Seq.filter (fun (idx, _) -> mineIdx |> Set.contains idx |> not)

    let emptyIdx =
        let proxIdxs =
            proximities
            |>Seq.map fst
        allIdxs
        |>List.except proxIdxs
        |>List.except mineIdx

    let foldEmpty mp idx =
        mp
        |>Map.add idx Empty

    let foldMine mp idx =
        mp
        |>Map.add idx Mine

    let foldProximity mp (idx, count) =
        mp
        |>Map.add idx (Proximity count)

    let a =
        emptyIdx
        |>Seq.fold foldEmpty Map.empty
    let b =
        proximities
        |>Seq.fold foldProximity a
    mineIdx
    |>Seq.fold foldMine b