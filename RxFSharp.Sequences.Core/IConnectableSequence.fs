namespace RxFSharp.Sequences.Core

open System
open System.Reactive
open System.Reactive.Subjects
open System.Threading

    // ** Connectable sequence (Hot) **
    type IConnectableSequence<'Tresult> =
        abstract member GetObservable : _ -> IConnectableObservable<'Tresult>