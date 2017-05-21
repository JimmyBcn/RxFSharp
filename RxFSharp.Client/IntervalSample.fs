namespace RxFSharp.Client

open System
open System.Reactive
open RxFSharp.Sequences

    type IntervalSample() =
        inherit Sample()
        override this.InnerExecute(_) =
            let sequence = new IntervalSequence()
            let observable = sequence.GetObservable()
            let observer = Observer.Create<int>((fun i -> printfn "%d" i), (fun error -> printfn "Error %s" error.Message), (fun () -> printfn "Sequence Completed!"))
            let subscription = observable.Subscribe(observer)
            subscription