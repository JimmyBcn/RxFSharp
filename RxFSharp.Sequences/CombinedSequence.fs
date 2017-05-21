namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq

    type CombinedSequence() =
        inherit Sequence<int>()
        let FirstSequence = 
            Observable
                .Interval(TimeSpan.FromMilliseconds((float)1000))
                .Select(fun i -> (int)i)
                .TakeWhile(fun i -> i < 5)
        let SecondSequence = 
            Observable
                .Interval(TimeSpan.FromMilliseconds((float)1600))
                .Select(fun i -> (int)i * 10)
                .TakeWhile(fun i -> i < 50)
        override this.GetObservable(_) =
            //Uncomment for Sample - Concat
            //let sequence = FirstSequence.Concat(SecondSequence)
            //sequence
            //    .Timeout(TimeSpan.FromMilliseconds((float)1200))
            //    .Throttle(TimeSpan.FromMilliseconds((float)1200)) // While events are received within 1200 ms, they are discarded
            
            //Uncomment for Sample - Merge
            //let sequence = FirstSequence.Merge(SecondSequence)
            //sequence
                //.Distinct()

            //Uncomment for Sample - Zip
            let sequence = FirstSequence.Zip(SecondSequence, fun o1 o2 -> o1 + o2)
            sequence

            //Uncomment for Sample - CombineLatest
            //let sequence = FirstSequence.CombineLatest(SecondSequence, fun o1 o2 -> o1 + o2)
            //sequence
