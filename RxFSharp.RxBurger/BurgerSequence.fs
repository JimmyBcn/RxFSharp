namespace RxFSharp.Subjects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open System.Reactive.Subjects
open RxFSharp.Sequences.Core
    
    type Burger(bread, cheese, vegs, meat) =
        member this.Bread = bread
        member this.Cheese = cheese
        member this.Vegs = vegs
        member this.Meat = meat

    type BurgerSequence() =
        inherit Sequence<Burger>()
        let burgerSubject = new Subject<BurgerIngredient>()

        let breadSequence = new BreadSequence()
        let cheeseSequence = new BreadSequence()
        let vegsSequence = new BreadSequence()
        let meatSequence = new BreadSequence()
        
        // ** We use the And-Then-When pattern to merge all sequences
        let pattern = breadSequence.GetObservable()
                        .And(cheeseSequence.GetObservable())
                        .And(vegsSequence.GetObservable())
                        .And(meatSequence.GetObservable())
        let plan = pattern.Then(fun b c v m -> new Burger(b, c, v, m))

        override this.GetObservable(_) =
            Observable.When(plan)


