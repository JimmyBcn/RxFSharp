namespace RxFSharp.Client

[<AutoOpen>]
module Main = 

    open System
    open System.Reactive
    open System.Reactive.Linq
    open RxFSharp.BasicSequences
    open RxFSharp.HotCold

    // ** Visit rxmarbles.com !!! **
    // ** Run the samples you are interested in **
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

        Console.WriteLine("** Cold Sequence **")
        SampleCold<int, ColdSequence>.Execute |> ignore

        Console.WriteLine("** Hot Sequence **")
        SampleHot<int, HotSequence>.Execute |> ignore

        Console.WriteLine("** Cold shared Sequence **")
        SampleColdShared<int, ColdSharedSequence>.Execute |> ignore

        Console.WriteLine("** Side effects on observable sequence **")
        SampleObservableSideEffect.Execute |> ignore
        0    