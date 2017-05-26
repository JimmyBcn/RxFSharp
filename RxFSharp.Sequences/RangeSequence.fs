namespace RxFSharp.BasicSequences

open System
open System.Reactive
open System.Reactive.Linq
open RxFSharp.Sequences.Core

    // ** The simpliest sequence snippet you can use to get used to LINQ syntax and some functional features using Rx **
    type RangeSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Range(0, 10)
                // ** FILTERING the sequence **
                //.Skip(5)
                //.Take(3)
                //.Where(fun i -> i % 2 = 0)
                // ** MAPPING (PROJECTING) the sequence **
                //.Select(fun i -> i*10) 
                // ** REDUCING the sequence **
                //.Sum()
                //.Max()
                //.Average().Select(fun a -> (int)a)
                // ** INSPECTING the sequence **
                //.ElementAt(5)
