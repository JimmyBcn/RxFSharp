namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Threading

    // ** Regular sequence (Cold) **
    type ISequence<'Tresult> =
        abstract member GetObservable : _ -> IObservable<'Tresult>