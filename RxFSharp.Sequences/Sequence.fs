﻿namespace RxFSharp.Sequences

open System
open System.Reactive

    [<AbstractClass>]
    type Sequence<'Tresult>() as this =
        abstract member GetObservable: _ -> IObservable<'Tresult> 
        interface ISequence<'Tresult> with 
            member x.GetObservable(_) = this.GetObservable()
