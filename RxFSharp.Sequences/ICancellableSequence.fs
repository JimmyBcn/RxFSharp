namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Threading

    type ICancellableSequence<'Tresult> =
        inherit ISequence<'Tresult>
        abstract member GetCancellableObservable : CancellationTokenSource -> IObservable<'Tresult>