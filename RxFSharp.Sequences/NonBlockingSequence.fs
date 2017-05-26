namespace RxFSharp.BasicSequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Threading.Tasks
open RxFSharp.Sequences.Core

    // ** A Non Blocking sequence will be executed on a background thread **
    type NonBlockingSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Create<int>(
                    fun (o : IObserver<_>) -> 
                            let task =
                                async {
                                    for i in 1 .. 10 do                                
                                        try
                                            o.OnNext(i)
                                            Thread.Sleep(1000) 
                                        with
                                            | :? Exception as ex -> o.OnError(ex) |> ignore
                                    o.OnCompleted() } 
                            Async.StartAsTask(task) |> ignore
                            Disposable.Empty)
