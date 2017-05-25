namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading

    type ColdSharedSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Create<int>(
                    fun (o : IObserver<_>) -> 
                        let task =
                            async {
                                let rnd = new Random()
                                o.OnNext(rnd.Next())
                                Thread.Sleep(2500)
                                o.OnCompleted()}  
                        Async.StartAsTask(task) |> ignore
                        Disposable.Empty)                   
                .Repeat()                
                .Publish() 

                // ** Cold down a [HOT OBSERVABLE SEQUENCE or ConnectableObservable] to a [COLD OBSERVABLE SEQUENCE or Observable] 
                // but now the sequence is shared among all the subscribers.
                // This observable uses the first observer subscription to perform the Connect() action (which starts emiting items) **
                .RefCount()