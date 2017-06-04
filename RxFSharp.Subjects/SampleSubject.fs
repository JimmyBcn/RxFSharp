namespace RxFSharp.Subjects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.HotCold
open RxFSharp.Sequences.Core

    type SampleSubject() =
        static member Execute =
            let firstSequence = new ColdSequence()
            let firstObservable = firstSequence.GetObservable()          
            let secondSequence = new ColdSequence()
            let secondObservable = secondSequence.GetObservable()
          
            let subject = new Subject<int>()
            // Using the subject as an observer
            let firstSubscription = firstObservable.Subscribe(subject)
            let secondSubscription = secondObservable.DelaySubscription(TimeSpan.FromMilliseconds((float)1000)).Subscribe(subject)
            
            // Using the subject as an observable
            let subjectSubscription = subject.Subscribe((fun i -> Console.WriteLine(i.ToString())), (fun () -> printfn "Sequence Completed!"))

            Console.WriteLine("Press ENTER to complete...")
            Console.ReadLine() |> ignore
            subject.OnCompleted()

            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            firstSubscription.Dispose()
            secondSubscription.Dispose()
            subjectSubscription.Dispose()
            0
