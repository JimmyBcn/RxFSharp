namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open RxFSharp.Sequences.Core

    type HotSequence() =
        inherit ConnectableSequence<int>()
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

                // ** Warms up the previous [COLD OBSERVABLE SEQUENCE or Observable] making it a [HOT OBSERVABLE SEQUENCE or ConnectableObservable]. 
                // Observers can subscribe now to the sequence (creating a disposable subscription) but the sequence does not start emiting items until one of them call the Connect() action. 
                // Subscriptors can dispose the subscription independently, but the ConnectableObservable will remain alive if any of the subscriptions is not disposed --> Be careful with memory leaks and dispose all subscriptions.
                // The [HOT OBSERVABLE SEQUENCE or ConnectableObservable] is SHARED among the subscribers, so all subscriptions are listening to the same sequence. **
                .Publish()