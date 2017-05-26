namespace RxFSharp.Domain

open System

    // This class will create mutable Employee objects, which are objects that can be modified from outside.
    type Employee(firstName: string, lastName: string) = 
        // ** [let-bindings area] --> code to declare private fields or functions **
        // ** Use 'val foo = ...' or 'val mutable foo = ...' to define public field --> this is not really used **
        // ** Use 'let foo = ...' or 'let mutable foo = ...' if you want to define a local value that is visible only within the type **
        let fullName = String.Format("{0} {1}", firstName, lastName)
        let random  = new System.Random()
        let mutable salary : int = 0

        // ** [do-bindings] --> code to be executed upon object construction **
        do printfn "The user %s has been created" fullName

        // ** [member-bindings] ** --> code to declare public members (additional constructors, properties, methos, events... **
        // ** [Properties]
        // ** Use 'member this.Foo = ...' to expose read-only properties from a type
        member this.FirstName = firstName
        member this.LastName = lastName
        // ** Use 'member val Foo = ...' to expose mutable properties from a type
        member val Identifier = 0 with get, set
        member val Position = "Not defined" with get, set
        // ** Use 'member this.Foo = with get() = ... and set(v) = ... <- v' expose mutable properties with custom code for getter and/or setter
        member this.Salary 
            with get() = 
                salary 
            and set(v) = 
                salary <- v
                printfn "The new salary for %s %s is %d" this.FirstName this.LastName this.Salary
        member this.FullName
            with get() =
                fullName
        
        // ** [member-bindings] **
        // ** [Methods]  
        member this.Increase(percentage: int) = salary <- salary + (salary * percentage)
        member this.Display = printfn "%s %s has ha salary of %d" this.FirstName this.LastName this.Salary



