namespace RxFSharp.Subjects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Threading.Tasks

    type BurgerIngredient =
    | Bread
    | Cheese
    | Vegs
    | Meat

    type BreadProducer() =
        let breadReady = new Event<BurgerIngredient>()
        member this.BreadReady = breadReady.Publish
        member this.Start() =
            Task.Factory.StartNew(fun () ->
                while true do
                    breadReady.Trigger(Bread)
                    Thread.Sleep(1000)) |> ignore

    type CheeseProducer() =
        let cheeseReady = new Event<BurgerIngredient>()
        member this.CheeseReady = cheeseReady.Publish
        member this.Start() =
            Task.Factory.StartNew(fun () ->
                while true do
                    cheeseReady.Trigger(Cheese)
                    Thread.Sleep(1000)) |> ignore

    type VegsProducer() =
        let vegsReady = new Event<BurgerIngredient>()
        member this.VegsReady = vegsReady.Publish
        member this.Start() =
            Task.Factory.StartNew(fun () ->
                while true do
                    vegsReady.Trigger(Vegs)
                    Thread.Sleep(1000)) |> ignore

    type MeatProducer() =
        let meatReady = new Event<BurgerIngredient>()
        member this.MeatReady = meatReady.Publish
        member this.Start() =
            Task.Factory.StartNew(fun () ->
                while true do
                    meatReady.Trigger(Meat)
                    Thread.Sleep(1000)) |> ignore

