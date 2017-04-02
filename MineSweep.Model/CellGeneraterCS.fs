module MineSweep.Model.CSharp.CellGenerator

open MineSweep.Model.FSharp.CellGenerator
open System.Collections.Generic
open MineSweep.Model

let CreateMineField (count, width, height) : IDictionary<struct (int*int), Cell> =
    createMineField count width height
    :> IDictionary<struct (int*int), Cell>