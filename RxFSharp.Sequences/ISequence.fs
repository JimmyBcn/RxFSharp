namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Threading

    type ISequence<'Tresult> =
        abstract member GetObservable : _ -> IObservable<'Tresult>