namespace RxFSharp.HotCold

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Threading.Tasks
open RxFSharp.Sequences.Core

    type Producer() =
        let numberSent = new Event<int>()
        member this.NumberSent = numberSent.Publish
        member this.Start() =
            Task.Factory.StartNew(fun () ->
                for i in 0 .. 12 do
                    numberSent.Trigger(i)
                    Thread.Sleep(1000)
                ) |> ignore

