namespace RxFSharp.BasicSequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Threading.Tasks
open RxFSharp.Sequences.Core

    // ** Using a CancellationTokenSource, the sequence can be cancelled (and then completed) from outside **
    type NonBlockingCancellableSequence() =
        inherit CancellableSequence<int>()
        override this.GetCancellableObservable(cts: CancellationTokenSource) =
            Observable
                .Create<int>(
                    fun (o : IObserver<_>) -> 
                            let task =
                                async {
                                    for i in 1 .. 10 do                                
                                        if cts = null || not cts.Token.IsCancellationRequested then
                                            o.OnNext(i)
                                            Thread.Sleep(1000) 
                                        else 
                                            Console.WriteLine("Sequence has been canceled!")
                                            o.OnCompleted() 
                                    o.OnCompleted() } 
                            Async.StartAsTask(task) |> ignore
                            Disposable.Create(fun () -> cts.Cancel()))
                    
