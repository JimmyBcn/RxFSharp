namespace RxFSharp.HotCold

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open RxFSharp.Sequences.Core
    
    // ** An external producer generates the sequence outside the observable boundaries
    // The resulting observable is hot, as the underlying sequence is always running,
    // and each observer receive the same sequence
    type ExternalSequence() =
        inherit Sequence<int>()
        let producer = new Producer()
        do producer.Start() |> ignore // this starts generating the sequence
        // Events in F# are already IObservable<T>, so you don't need to convert them.
        override this.GetObservable(_) = producer.NumberSent.AsObservable()

