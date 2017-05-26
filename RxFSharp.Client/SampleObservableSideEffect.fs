namespace RxFSharp.Client

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Subjects
open System.Threading
open RxFSharp.Domain
open RxFSharp.BasicSequences
open RxFSharp.SideEffects
open RxFSharp.Sequences.Core

    type SampleObservableSideEffect() =
        static member Execute =
            let sequence = new EmployeeSequence()
            let observable = sequence.GetObservable()
            let observer = Observer.Create<Employee>(
                (fun i -> Console.WriteLine(i.Display)))//, (fun () -> printfn "Sequence Completed! Press ENTER to finish..."))
            let subscription = observable.Subscribe(observer)
            Console.WriteLine("Press ENTER to unsubscribe...")
            Console.ReadLine() |> ignore
            subscription.Dispose()
            0



