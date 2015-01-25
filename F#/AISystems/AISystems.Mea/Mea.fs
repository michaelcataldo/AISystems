namespace AISystems.Mea

module Mea = 
    open System
    open System.Collections.Generic
    
    type Result<'a> = 
        | Success of 'a
        | Failure of 'a
    
    let tryFindOperators fact operators =
        Set.filter (fun op -> Set.contains fact op.Add) operators
    
    let tryApplyOperator operator state = 
        let pre = operator.Pre
        match state with
        | state when pre.IsSubsetOf state -> 
            operator.Del
            |> Set.difference state
            |> Set.union operator.Add
            |> Some
        | _ -> None
    
    let traverseOperators action continuation operators = 
        let rec loop ops = 
            match ops with
            | hd :: tl -> 
                let result = action hd
                match result with
                | Success x -> result
                | Failure x -> loop tl
            | [] -> continuation
        loop operators
    
    let prioritise state = state

    let rec printList list =
            match list with
            | [] -> ()
            | hd :: tl ->
                printfn "%O" hd.Value
                printList tl
    
    let search state goal ops = 
        let rec searchHelper state stack path cache = 
            match stack : obj list with
            | [] ->
                match state with
                | state when Set.isSubset goal state -> Success path
                | _ -> Failure path
            | next :: tail -> 
                match next with
                | :? Operator as op ->
                    match path with
                    | path when List.exists ((=) op) path -> searchHelper state tail path cache
                    | _ ->
                        match tryApplyOperator op state with
                        | Some state ->
                            let path = op :: path
                            searchHelper state tail path Set.empty
                        | _ -> searchHelper state tail path cache
                | :? Fact as fact when state.Contains fact -> searchHelper state tail path cache
                | :? Fact as fact -> 
                    let ops = tryFindOperators fact ops
                    let action op = 
                        let pre = 
                            op.Pre
                            |> prioritise
                            |> Seq.cast<obj>
                            |> Seq.toList
                        
                        let stack = List.append (op :> obj :: tail) pre
                        let cache = Set.add op cache
                        searchHelper state stack path cache
                    let continuation = searchHelper state tail path cache
                    ops - cache
                    |> Set.toList
                    |> traverseOperators action continuation
                | _ -> failwith ""
        
        let stack = 
            goal - state
            |> prioritise
            |> Seq.cast<obj>
            |> Seq.toList
        
        searchHelper state stack List.empty Set.empty
