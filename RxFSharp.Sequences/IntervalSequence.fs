namespace RxFSharp.Sequences

open System
open System.Reactive
open System.Reactive.Linq

    type IntervalSequence() =
        inherit Sequence<int>()
        override this.GetObservable(_) =
            Observable
                .Interval(TimeSpan.FromMilliseconds((float)1000))
                .Select(fun i -> (int)i)

                // TIME SHIFTING the sequence
                //.DelaySubscription(TimeSpan.FromSeconds(2))
                //.Sample(TimeSpan.FromMilliseconds((float)1500))

                // FINALIZING the sequence
                .TakeWhile(fun i -> i < 5)

                // REPEATING the sequence (use with TakeWhile)
                .Repeat()
