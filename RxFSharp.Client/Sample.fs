namespace RxFSharp.Client

open System
open System.Reactive
open System.Threading
open RxFSharp.Sequences

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

    //Comming soon !!!
    //type SampleHotCold<'Titem, 'Tsequence when 'Tsequence :> ISequence<'Titem>>() =
    //   static member Execute =
    //       let sequence = Activator.CreateInstance<'Tsequence>()
    //       let cts = new CancellationTokenSource()
    //       let observable = sequence.GetCancellableObservable(cts)
    //       let observer = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())), (fun error -> printfn "Error %s" error.Message), (fun () -> printfn "Sequence Completed!"))
    //       let subscription = observable.Subscribe(observer)
    //       Console.WriteLine("Press ENTER to unsubscribe, or any key to cancel and terminate the sequence")
    //       let keyStroke = Console.ReadKey() 
    //       if keyStroke.Key.Equals(ConsoleKey.Enter) then
    //           subscription.Dispose();
    //       else
    //           cts.Cancel();
    //       Console.WriteLine("Press ENTER to start next sample...")
    //       Console.ReadLine() |> ignore
    //       Console.WriteLine()
    //       0
