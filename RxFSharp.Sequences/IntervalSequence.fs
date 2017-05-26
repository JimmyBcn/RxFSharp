namespace RxFSharp.BasicSequences

open System
open System.Reactive
open System.Reactive.Linq
open RxFSharp.Sequences.Core

    // ** Use this sequence snippet to start simulating an asynchronous sequence of events or playing with Rx time shifting facilities **
    type IntervalSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            let rnd = new Random()
            Observable
                .Interval(TimeSpan.FromMilliseconds((float)(rnd.Next(1000))))
                .Select(fun i -> (int)i) //this simultes an asynchronous sequence of events

                // ** TIME SHIFTING the sequence **
                //.DelaySubscription(TimeSpan.FromSeconds(2))
                //.Sample(TimeSpan.FromMilliseconds((float)(rnd.Next(1000))))

                // ** FINALIZING the sequence **
                .TakeWhile(fun i -> i < 5)

                // ** REPEATING the sequence (use with TakeWhile) **
                .Repeat()
