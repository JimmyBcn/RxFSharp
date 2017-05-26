namespace RxFSharp.Domain

open System

    type LastNameGenerator() = 
        static let rnd = new Random()
        static let names = ["Mia"; "Vincent"; "Jules"; "Winston"; "butch"; "Honey"]

        static member GetRandomName() = 
            let r = rnd.Next(5)
            let name = names.[r]
            name




