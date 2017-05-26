namespace RxFSharp.Sequences.Core

open System
open System.Reactive
open System.Threading

    // ** Cancelable sequence (Cold) **
    type ICancellableSequence<'Tresult> =
        inherit ISequence<'Tresult>
        abstract member GetCancellableObservable : CancellationTokenSource -> IObservable<'Tresult>