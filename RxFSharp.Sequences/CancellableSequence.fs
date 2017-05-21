namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Threading

    [<AbstractClass>]
    type CancellableSequence<'Tresult>() as this =
        abstract member GetCancellableObservable: CancellationTokenSource -> IObservable<'Tresult> 
        interface ICancellableSequence<'Tresult> with
            member x.GetObservable(_) = this.GetCancellableObservable(null)
            member x.GetCancellableObservable(cts : CancellationTokenSource) = this.GetCancellableObservable(cts)
