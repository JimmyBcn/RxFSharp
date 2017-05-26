namespace RxFSharp.Domain

open System

    type FirstNameGenerator() = 
        static let rnd = new Random()
        static let names = ["Wallace"; "Vega"; "Winnfield"; "Wolfe"; "Coolidge"; "Bunny"]

        static member GetRandomName() = 
            let r = rnd.Next(5)
            let name = names.[r]
            name