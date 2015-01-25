namespace AISystems.Mea

[<AutoOpen>]
module Operators =

    [<CustomComparison; CustomEquality>]
    type Fact =
        { Value : string }
        interface System.IComparable<Fact> with
            member this.CompareTo { Value = value } =
                compare this.Value value
        interface System.IComparable with
            member this.CompareTo obj =
                match box obj with
                    | :? Fact as other -> (this :> System.IComparable<_>).CompareTo other
                    | _ -> invalidArg "obj" "not a Fact"
        interface System.IEquatable<Fact> with
            member this.Equals { Value = value } =
                this.Value = value
        override this.Equals obj =
            match box obj with
            | :? Fact as other -> (this :> System.IEquatable<_>).Equals other
            | _ -> invalidArg "obj" "not a Fact"
        override this.GetHashCode () =
            hash this.Value

    type Operator =
        { Name : string
          Pre : Set<Fact>
          Add : Set<Fact>
          Del : Set<Fact> }