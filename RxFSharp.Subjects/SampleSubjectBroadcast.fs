namespace RxFSharp.Subjects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.HotCold
open RxFSharp.Sequences.Core

    type SampleSubjectBroadcast() =
        static member Execute =
            let firstSequence = new ColdSequence()
            let firstObservable = firstSequence.GetObservable()          
          
            let subject = new Subject<int>()
            // Using the subject as an observer
            let subscription = firstObservable.Subscribe(subject)

            // Using the subject as an observable (broadcast version)
            let firstSubjectSubscription = subject.Subscribe((fun i -> Console.WriteLine(i.ToString())), (fun () -> printfn "Sequence Completed!"))
            let secondSubjectSubscription = subject.DelaySubscription(TimeSpan.FromMilliseconds((float)6000)).Subscribe((fun i -> Console.WriteLine(i.ToString())), (fun () -> printfn "Sequence Completed!"))

            Console.WriteLine("Press ENTER to complete...")
            Console.ReadLine() |> ignore
            subject.OnCompleted()

            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            subscription.Dispose()
            firstSubjectSubscription.Dispose()
            secondSubjectSubscription.Dispose()
            0
