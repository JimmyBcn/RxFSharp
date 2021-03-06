﻿namespace RxFSharp.Sequences.Core

open System
open System.Reactive
open System.Threading

    // ** Cancelable sequence (Cold) **
    [<AbstractClass>]
    type CancellableSequence<'Tresult>() as this =
        abstract member GetCancellableObservable: CancellationTokenSource -> IObservable<'Tresult> 
        interface ICancellableSequence<'Tresult> with
            member x.GetObservable(_) = this.GetCancellableObservable(null)
            member x.GetCancellableObservable(cts : CancellationTokenSource) = this.GetCancellableObservable(cts)
