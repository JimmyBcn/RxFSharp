namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading

    type BlockingSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Create<int>(
                    fun (o : IObserver<_>) -> 
                        for i in 1 .. 10 do
                            try
                                o.OnNext(i)
                                Thread.Sleep(1000)
                            with
                                | :? Exception as ex -> o.OnError(ex)
                        o.OnCompleted()
                        Disposable.Empty)
            
                    
