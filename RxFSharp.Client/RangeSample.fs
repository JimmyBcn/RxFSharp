namespace RxFSharp.Client

open System
open System.Reactive
open RxFSharp.Sequences

    type RangeSample() =
        inherit Sample()
        override this.InnerExecute(_) =
            let sequence = new RangeSequence()
            let observable = sequence.GetObservable()
            let observer = Observer.Create<int>(fun i -> printfn "%d" i)
            let subscription = observable.Subscribe(observer);
            subscription 

