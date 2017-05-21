namespace RxFSharp.Client

[<AutoOpen>]
module Main = 

    open System
    open System.Reactive
    open System.Reactive.Linq
    open RxFSharp.Sequences

    [<EntryPoint>]
    let main argv = 
        Console.WriteLine("** Simple Range Sequence **")
        Sample<int, RangeSequence>.Execute |> ignore

        Console.WriteLine("** Simple Interval Sequence **")
        Sample<int, IntervalSequence>.Execute |> ignore

        Console.WriteLine("** Combined Sequence **")
        Sample<int, CombinedSequence>.Execute |> ignore

        Console.WriteLine("** Blocking Sequence **")
        Sample<int, BlockingSequence>.Execute |> ignore

        Console.WriteLine("** Non Blocking Sequence **")
        Sample<int, NonBlockingSequence>.Execute |> ignore

        Console.WriteLine("** Cancellable Sequence **")
        SampleCancellable<int, NonBlockingCancellableSequence>.Execute |> ignore

        // Coming soon
        //Console.WriteLine("** Hot and Cold Sequences **")
        //Sample<int, HotColdSequence>.Execute |> ignore
        0    