namespace RxFSharp.Subjects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open RxFSharp.Sequences.Core
    
    //TimeShift those sequences for incoming project samples...

    type BreadSequence() =
        inherit Sequence<BurgerIngredient>()
        let producer = new BreadProducer()
        do producer.Start() |> ignore 
        override this.GetObservable(_) = producer.BreadReady.AsObservable() 

    type CheeseSequence() =
        inherit Sequence<BurgerIngredient>()
        let producer = new CheeseProducer()
        do producer.Start() |> ignore 
        override this.GetObservable(_) = producer.CheeseReady.AsObservable() 

    type VegsSequence() =
        inherit Sequence<BurgerIngredient>()
        let producer = new VegsProducer()
        do producer.Start() |> ignore 
        override this.GetObservable(_) = producer.VegsReady.AsObservable() 

    type MeatSequence() =
        inherit Sequence<BurgerIngredient>()
        let producer = new MeatProducer()
        do producer.Start() |> ignore 
        override this.GetObservable(_) = producer.MeatReady.AsObservable() 

