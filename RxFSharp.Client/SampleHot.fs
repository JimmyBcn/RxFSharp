namespace RxFSharp.Client

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.BasicSequences
open RxFSharp.Sequences.Core

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
