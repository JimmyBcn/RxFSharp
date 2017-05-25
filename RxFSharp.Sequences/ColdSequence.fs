namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading

    // ** Creates a [COLD OBSERVABLE SEQUENCE or Observable] which starts emitting items when an observer is subscribed to (creating a disposable subscription). 
    // A subscription is not terminated unless either it is disposed nor the sequence finishes (OnCompleted) or returns an error (OnError).
    // The observable object remains alive while any of the subscriptions is not disposed. --> Be careful with memory leaks.
    // The values sent by the observable are NOT SHARED among the subscribers, so every single subscription is listening to a different sequence. **
    type ColdSequence() =
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