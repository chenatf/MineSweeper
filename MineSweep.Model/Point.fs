namespace MineSweep.Model

[<Struct>]
type Point ={
    X: int
    Y: int
}
with
    static member op_Implicit(struct (x, y)) =
        {
            X = x
            Y = y
        }
