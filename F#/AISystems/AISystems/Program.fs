namespace AISystems

open AISystems.Mea;
open AISystems.Mea.Operators;
open AISystems.Mea.Mea;

module Program =
    
    [<EntryPoint>]
    let main argv = 
    
        printfn "test"

        let operators =
            set [{ Operator.Name = "D3 (A -> B)";
                   Operator.Pre = set [{Value = "in ralf cafe"}];
                   Operator.Add = set [{Value = "in ralf kitchen"}];
                   Operator.Del = set [{Value = "in ralf cafe"}]; };
                 { Operator.Name = "D3 (A -> C)";
                   Operator.Pre = set [{Value = "in ralf kitchen"}];
                   Operator.Add = set [{Value = "in ralf cafe"}];
                   Operator.Del = set [{Value = "in ralf kitchen"}]; };
                 { Operator.Name = "large-disk-to-C";
                   Operator.Pre = set [{Value = "in ralf cafe"}];
                   Operator.Add = set [{Value = "in ralf street"}];
                   Operator.Del = set [{Value = "in ralf cafe"}]; };
                 { Operator.Name = "pickup-rat";
                   Operator.Pre = set [{Value = "in ralf kitchen"}; {Value = "in rat kitchen"}; {Value = "holds ralf nil"}];
                   Operator.Add = set [{Value = "holds ralf rat"}];
                   Operator.Del = set [{Value = "in rat kitchen"}; {Value = "holds ralf nil"}]; };
                 { Operator.Name = "drop-rat";
                   Operator.Pre = set [{Value = "in ralf street"}; {Value = "holds ralf rat"}];
                   Operator.Add = set [{Value = "holds ralf nil"}; {Value = "in rat street"}];
                   Operator.Del = set [{Value = "holds ralf rat"}]; };
                ];

        let state = set [{Value = "in ralf cafe"}; {Value = "in rat kitchen"}; {Value = "holds ralf nil"}]

        let goal = set [{Value = "in rat street"}]

//        let operators =
//            set [{ Operator.Name = "move-to-kitchen";
//                   Operator.Pre = set [{Value = "in ralf cafe"}];
//                   Operator.Add = set [{Value = "in ralf kitchen"}];
//                   Operator.Del = set [{Value = "in ralf cafe"}]; };
//                 { Operator.Name = "move-to-cafe";
//                   Operator.Pre = set [{Value = "in ralf kitchen"}];
//                   Operator.Add = set [{Value = "in ralf cafe"}];
//                   Operator.Del = set [{Value = "in ralf kitchen"}]; };
//                 { Operator.Name = "move-to-street";
//                   Operator.Pre = set [{Value = "in ralf cafe"}];
//                   Operator.Add = set [{Value = "in ralf street"}];
//                   Operator.Del = set [{Value = "in ralf cafe"}]; };
//                 { Operator.Name = "pickup-rat";
//                   Operator.Pre = set [{Value = "in ralf kitchen"}; {Value = "in rat kitchen"}; {Value = "holds ralf nil"}];
//                   Operator.Add = set [{Value = "holds ralf rat"}];
//                   Operator.Del = set [{Value = "in rat kitchen"}; {Value = "holds ralf nil"}]; };
//                 { Operator.Name = "drop-rat";
//                   Operator.Pre = set [{Value = "in ralf street"}; {Value = "holds ralf rat"}];
//                   Operator.Add = set [{Value = "holds ralf nil"}; {Value = "in rat street"}];
//                   Operator.Del = set [{Value = "holds ralf rat"}]; };
//                ];
//
//        let state = set [{Value = "in ralf cafe"}; {Value = "in rat kitchen"}; {Value = "holds ralf nil"}]
//
//        let goal = set [{Value = "in rat street"}]

//        let operators =
//            set [
//                 { Operator.Name = "move-to-street";
//                   Operator.Pre = set [{Value = "in ralf cafe"}];
//                   Operator.Add = set [{Value = "in ralf street"}];
//                   Operator.Del = set [{Value = "in ralf cafe"}]; };
//                 { Operator.Name = "pickup-rat";
//                   Operator.Pre = set [{Value = "in ralf cafe"}; {Value = "in rat cafe"}; {Value = "holds ralf nil"}];
//                   Operator.Add = set [{Value = "holds ralf rat"}];
//                   Operator.Del = set [{Value = "in rat cafe"}; {Value = "holds ralf nil"}]; };
//                 { Operator.Name = "drop-rat";
//                   Operator.Pre = set [{Value = "in ralf street"}; {Value = "holds ralf rat"}];
//                   Operator.Add = set [{Value = "holds ralf nil"}; {Value = "in rat street"}];
//                   Operator.Del = set [{Value = "holds ralf rat"}]; };
//                ];
//
//        let state = set [{Value = "in ralf cafe"}; {Value = "in rat cafe"}; {Value = "holds ralf nil"}]
//
//        let goal = set [{Value = "in rat street"}]

        let result = search state goal operators

        let rec printList list =
            match list with
            | [] -> ()
            | hd :: tl ->
                printfn "%O" hd.Name
                printList tl

        let list =
            match result with
            | Success x -> x
            | Failure x -> x 

        printList list

        0
