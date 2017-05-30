namespace RxFSharp.Client

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.HotCold
open RxFSharp.Sequences.Core

    type SampleProducer<'Titem, 'Tsequence when 'Tsequence :> ISequence<'Titem>>() =
        static member Execute =
            let sequence = Activator.CreateInstance<'Tsequence>()
            let observable = sequence.GetObservable()
          
            let firstObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let secondObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
            let thirdObserver = Observer.Create<'Titem>((fun i -> Console.WriteLine(i.ToString())))
          
            let firstSubscription = observable.Subscribe(firstObserver)
            let secondSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)1200)).Subscribe(secondObserver)
            let thirdSubscription = observable.DelaySubscription(TimeSpan.FromMilliseconds((float)2200)).Subscribe(thirdObserver)
            
            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            firstSubscription.Dispose()
            secondSubscription.Dispose()
            thirdSubscription.Dispose()
            0
