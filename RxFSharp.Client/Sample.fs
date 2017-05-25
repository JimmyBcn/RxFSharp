namespace RxFSharp.Client

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.Sequences

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

    type SampleCold<'Titem, 'Tsequence when 'Tsequence :> ISequence<'Titem>>() =
        static member Execute =
            let sequence = Activator.CreateInstance<'Tsequence>()
            let observable = sequence.GetObservable()
          
            let firstObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let secondObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let thirdObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
          
            let firstSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)500)).Subscribe(firstObserver)
            let secondSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1000)).Subscribe(secondObserver)
            let thirdSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1500)).Subscribe(thirdObserver)
            
            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            firstSubscription.Dispose()
            secondSubscription.Dispose()
            thirdSubscription.Dispose()
            0

    type SampleHot<'Titem, 'Tsequence when 'Tsequence :> IConnectableSequence<'Titem>>() =
        static member Execute =
            let sequence = Activator.CreateInstance<'Tsequence>()
            let observable = sequence.GetObservable() 
          
            let firstObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let secondObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let thirdObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
          
            let firstSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)500)).Subscribe(firstObserver)
            let secondSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1000)).Subscribe(secondObserver)
            let thirdSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1500)).Subscribe(thirdObserver)
          
            Console.WriteLine("Press ENTER to connect...")
            Console.ReadLine() |> ignore
            observable.Connect() |> ignore

            Console.WriteLine("Press ENTER to unsubscribe the first...");
            Console.ReadLine() |> ignore
            firstSubscription.Dispose() |> ignore

            Console.WriteLine("Press ENTER to reconnect the first...");
            Console.ReadLine() |> ignore
            let reFirstSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)500)).Subscribe(firstObserver)

            Console.WriteLine("Press ENTER to unsubscribe the rest...")
            Console.ReadLine() |> ignore
            reFirstSubscription.Dispose()
            secondSubscription.Dispose()
            thirdSubscription.Dispose()
            0

    type SampleColdShared<'Titem, 'Tsequence when 'Tsequence :> ISequence<'Titem>>() =
        static member Execute =    
            let sequence = Activator.CreateInstance<'Tsequence>()
            let observable = sequence.GetObservable() 
          
            let firstObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let secondObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let thirdObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
          
            let firstSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)500)).Subscribe(firstObserver)
            let secondSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1000)).Subscribe(secondObserver)
            let thirdSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1500)).Subscribe(thirdObserver)
                    
            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            firstSubscription.Dispose()
            secondSubscription.Dispose()
            thirdSubscription.Dispose()
            0
