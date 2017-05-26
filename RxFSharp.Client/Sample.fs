namespace RxFSharp.Client

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.Sequences
open RxFSharp.Sequences.Core

    // ** The following methods basically get an observable sequence, define an observer and subscribe that observer to the sequence **
    type Sample<'Titem, 'Tsequence when 'Tsequence :> ISequence<'Titem>>() =
        static member Execute =
            let sequence = Activator.CreateInstance<'Tsequence>()
            let observable = sequence.GetObservable()
            let observer = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())), (fun error -> printfn "Error %s" error.Message), (fun () -> printfn "Sequence Completed!"))
            let subscription = observable.Subscribe(observer)
            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            subscription.Dispose();
            Console.WriteLine("Press ENTER to start next sample...")
            Console.ReadLine() |> ignore
            Console.WriteLine()
            0