namespace RxFSharp.SideEffects

open System
open System.Reactive
open System.Reactive.Linq
open System.Reactive.Disposables
open System.Threading
open RxFSharp.Sequences.Core
open RxFSharp.Domain

    type EmployeeSequence() =
        inherit Sequence<Employee>()
        let mutable index = 0
        override this.GetObservable(_) =
            Observable
                .Interval(TimeSpan.FromMilliseconds((float)1000))
                .Select(
                    fun i -> 
                        let firstName = FirstNameGenerator.GetRandomName()
                        let lastName = LastNameGenerator.GetRandomName()
                        let employee = new Employee(firstName, lastName)
                        employee)             
                .Take(5)