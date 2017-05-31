namespace RxFSharp.HotCold

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Threading.Tasks
open RxFSharp.Sequences.Core
    
    // ** An internal producer generates the elements inside the observable boundaries
    // The resulting observable is cold, as the underlying sequence is created and started once the observer is subscribed,
    // and each observer generates an independent sequence.
    type InternalSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Create<int>(
                    fun (o : IObserver<_>) -> 
                        Task.Factory.StartNew(fun () ->
                                        for i in 0 .. 20 do
                                            o.OnNext(i)
                                            Thread.Sleep(1000) |> ignore))                
            

