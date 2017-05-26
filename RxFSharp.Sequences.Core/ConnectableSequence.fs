namespace RxFSharp.Sequences.Core

open System
open System.Reactive
open System.Reactive.Subjects

    // ** Connectable sequence (Hot) **
    [<AbstractClass>]
    type ConnectableSequence<'Tresult>() as this =
        abstract member GetObservable: _ -> IConnectableObservable<'Tresult> 
        interface IConnectableSequence<'Tresult> with 
            member x.GetObservable(_) = this.GetObservable()

