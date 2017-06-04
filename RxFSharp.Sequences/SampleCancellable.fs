namespace RxFSharp.BasicSequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.Sequences.Core

    type SampleCancellable<'Titem, 'Tsequence when 'Tsequence :> ICancellableSequence<'Titem>>() =
        static member Execute =
            let sequence = Activator.CreateInstance<'Tsequence>()
            let cts = new CancellationTokenSource()
            let observable = sequence.GetCancellableObservable(cts)
            let observer = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())), (fun error -> printfn "Error %s" error.Message), (fun () -> printfn "Sequence Completed!"))
            let subscription = observable.Subscribe(observer)
            Console.WriteLine("Press ENTER to unsubscribe, or any key to cancel and terminate the sequence")
            let keyStroke = Console.ReadKey() 
            if keyStroke.Key.Equals(ConsoleKey.Enter) then
                subscription.Dispose();
            else
                cts.Cancel();
            Console.WriteLine("Press ENTER to start next sample...")
            Console.ReadLine() |> ignore
            Console.WriteLine()
            0

