# Concurrency

Concurrent computing is a form of computing in which **several computations are executed during overlapping time periods** (concurrently) instead of sequentially.

There are several ways to enable concurrency in a .NET application:

- **Multithreading** : consists on dividing a task into multiple smaller tasks and let multiple threads execute them concurrently in the same single CPU. The .NET introduced the **Task** type representing an abstraction for easiest multithreading coding.
- **Parallel processing** : consists on applying multithreading through more than one processor. The .NET introduced the **TPL** to support parallel concurrency.
- **Ashyncronous programming** : consists on using promises or callbacks to allow releaising the calling thread until the background job is finished. .NET introduced the **async/await** keywords to enable an easiest way to implement asynchronous programming.
- **Reactive programming** : consists on working with asynchronous data streams. .Net provided the **Rx.NET** library as an abstraction layer that enables an easier way to provide concurrency to the incoming data streams.

If a single CPU has multiple tasks (such as threads) on it, they are executing concurrently, each thread gets a portion of CPU time before yielding to another thread, even if a thread has not yet finished. Time slicing ensures that all software threads make some progress, but t his equitable distribution of hardware threads incurs overhead that can severely degrade performance. This is managed by the operative system at the low level and currently served to the programmer through several abstractions (schedulers).

Parallel execution is concurrent by definition, but concurrency is not necessarily parallelism. Parallelism only occurs if those threads are being scheduled and executed on different CPUs at the exact same time.

## Concurrency issues

The common problem that concurrency introduces is **unpredictable timing**.

In multi-threaded code, multiple independent threads may concurrently access the same state (so called **shared state** ). **Unsynchronized access to mutable shared state can introduce race conditions** , and race conditions may lead to **unpredictable behaviour** , in the form of **deadlocks** (situation where no progress can be made due to threads that are waiting each other) or **corrupted state**.

Race conditions have a reputation of being difficult to reproduce and debug, since the end result is non-deterministic and depends on the relative timing between interfering threads.  Good practices pray for **avoiding write non-deterministic code** , this is code which result depend on race conditions, **by careful software design**.

Race condition can be addressed in various ways. first of all, **if your application is not noticeably improved by adding a layer of concurrency, then you should avoid doing so**. Concurrent applications can exhibit maintenance problems with symptoms surfacing in the areas of debugging, testing and refactoring. Therefore, partitioning a fixed amount of work among too many threads gives each thread too little work, so that **the overhead of starting and terminating threads swamps the useful work**.

In case you need to write concurrent software, **modifying shared state should be avoided when possible**.

Eventually, if we cannot get rid of the mutated state, we need mechanisms to guard critical sections and enforce synchronized access. **A common used mechanism is locking** , which basically serializes access to critical sections (by means of high level abstractions such as semaphores or monitors).

**Another mechanism is scheduling**. By means of scheduling techniques we can control the exact timing of when tasks in a concurrent system are executed based on a specified priority. It also denotes where the task will be executed. The scheduler has a clock which provides a notion of time for itself.Scheduling is also useful if we want fine-tune the performance of existing code.

It is realkly difficult to write high-performance, efficient, scalable, and correct concurrent software, and when jumping to distributed systems, concurrency and parallelism are even taken to another level.

# Concurrency in Rx

One of the most interesting aspects of the **Observable** is how it simplifies the interaction between the producer and subscriber, even abstracting away the details of concurrency. **The concurrency implementation details remain encapsulated in the producer without leaking into the subscriber code**.

**Rx is single-threaded by default,** that means that the observable and observer code are carried on the same single thread. But **Rx is also a free-threaded model** , which means that you are not restricted to which thread you choose to do your work. If you do not (somehow) introduce any scheduling, callbacks will be invoked on the same thread that the OnNext/OnError/OnCompleted methods are invoked from.

In Rx, it is no allowed to invoke onNext concurrently, so **a single observable cannot generate items concurrently from more than one thread**. The onNext cannot be invoked concurrently for several reasons. Primarily because onNext() is meant for us humans to use, and concurrency is difficult. Second reason is because some operators such as **scan** and **reduce** require sequential event propagation so that state can be accumulated on streams of events that are not both associative and commutative. A third reason is that performance is affected by synchronization overhead because all observers and operators would need to be thread-safe, even if most of the time data arrives sequentially.

## Schedulers

If some concurrency must be introduced, Rx provides **Schedulers**, which provide a rich platform for processing work concurrently without the need to be exposed directly to threading primitives. An IScheduler in Rx is used to schedule some action to be performed, either as soon as possible or at a given point in the future. They also help with common troublesome areas of concurrency such as cancellation, passing state and recursion.

The Rx schedulers are somehow doing work for you when you’re writing a query and when you subscribe to an observable sequence. **Whenever Rx needs to introduce concurrency to get a job done, it relies on a scheduler to do so**.

The **Rx library adds a bunch of useful operators that internally will handle concurrency issues for us**, providing the desired abstraction layers for us to avoid having to deal with race conditions and save threading. When you browse the methods on the Observable class, you’ll notice a lot of them to have an overload that takes in a scheduler. This means the operator has to schedule work, maybe based on time, to get its job done. Luckily, in most cases you don’t even need to worry about this happening under the covers, thanks to the overloads that pass in a meaningful default value.

This is achieved by combining patterns commonly used in event-driven applications with constructs from functional programming that eliminate shared mutable state. The way to think about schedulers is as a single abstraction over all the different ways that exist in the underlying platform to get work done. Typical implementations of IScheduler forward work directly to the underlying infrastructure (such as the .NET task pool or the WinRT thread pool, to name just a few), or maintain a queue of work ordered by the time the work is due.

**Rx never adds concurrency unless it is asked to do so.** A synchronous observable would be subscribed to, emit all data **using the subscriber&#39;s thread** , and complete (if finite).However, in those cases where an scheduler is needed (for instance, when using the Buffer operator) it uses an scheduler following the **principle of least concurrency.**

The available types of Rx schedulers are:
- _ImmediateScheduler_: The specified action will start immediately.
- _CurrentThreadScheduler_: The specified action will be scheduled (queued) on the thread that made the original call.
- _DispatcherScheduler_: The specified action will be scheduled on the current Dispatcher e.g. WPF app dispatcher.
- _NewThreadScheduler_: The specified action will be scheduled on a newly created thread. Requesting the creation of a thread is expensive. Ideal for long operations e.g. responsive UI.
- _ThreadPoolScheduler_: The specified action will be scheduled on a thread pool thread. Ideal for short operations.
- _TaskPoolScheduler_: The specified action will be scheduled using TaskFactory from TPL. Ideal for short operations.
- _VirtualScheduler_: Useful for testing and debugging by emulating real time.

Each of the overloads to Schedule returns an _**IDisposable**_; this way, a consumer can cancel the scheduled work.

**Some Schedule overloads accepts passing the state as argument**. The passed version of the state is used by the scheduler&#39;s internal workings, so what is executed by the scheduler regarding that state is not being affected   by outside changes on it.

## ObserveOn / SubscribeOn
Rx uses scheduling to implement an easy mechanism for introducing concurrency and multithreading, these are exposed via two extension methods to IObservable&lt;T&gt; called **SubscribeOn** and **ObserveOn,** so that **it is possible to subscribe and observe on different threads**. Both methods have an overload that take an **IScheduler** (or SynchronizationContext) and return an IObservable&lt;T&gt; so you can chain methods together.By using these appropriately, we can simplify our code base, increase responsiveness and reduce the surface area of our concurrency concerns.

The **ObserveOn** operator enables specifying the context in which notifications must be pushed  to observers. By default, the ObserveOn operator ensures that OnNext will be called as many times as possible on the current thread. You can use its overloads and **redirect the onNext outputs to a different context**. it is best to place ObserveOn later in the query. This is because a query will potentially filter out a lot of messages, and placing the ObserveOn operator earlier in the query would do extra work on messages that would be filtered out anyway. Calling the ObserveOn operator at the end of the query will create the least performance impact. Another advantage of specifying a scheduler type explicitly is that you can introduce concurrency for performance purposes

Using the ObserveOn operator, an action is scheduled for each message that comes through the original observable sequence. This potentially changes timing information as well as puts additional stress on the system. 

Instead of using the ObserveOn, we can create concurrency just passing the right scheduler to the observable, using the **SubscribeOn** operator.For example, instead of observe on the UI thread (which is going to redirect each new item of the sequence to that thread) we can subscribe to the UI thread directly.Doing this, all values pushed out from the observable sequence will be originated right on the UI thread.

Best practices using **ObserveOn** and **SubscribeOn** operators can be defined in most cases for some scenarios.

In UI **Applications** the final subscriber is normally the presentation layer and should control the scheduling. **Observe on the DispatcherScheduler** to allow updating of ViewModels. **Subscribe on a background thread** ( **ThreadPoolScheduler** or **NewThreadScheduler** if takes more than 50ms) to prevent the UI from becoming unresponsive

In a **service layer** , iIf your service is reading data from a queue of some sort, consider using a dedicated **EventLoopScheduler**. This way, you can preserve order of events. If processing an item is expensive (&gt;50ms or requires I/O), then consider using a **NewThreadScheduler.**

If you just need the scheduler for a **timer** , e.g. for Observable.Interval or Observable.Timer, then favor the **TaskPool**. Use the ThreadPool if the TaskPool is not available for your platform.

Rx provides a set of commonly used SynchronizationContexts helpers for easy access to some contexts:
- _CurrentDispatcher_: wraps the WPF Dispatcher, to post on the WPF UI Thread for use in WPF and Silverlight applications
- _WindowsForms_: will use the current Windows Forms form to post back on the Windows Forms UI Thread
- _ReactiveEventLoop_: will use a single background thread to post messages in a synchronized matter for applications that don’t have UI
- _FromControl(Control control)_: will use control to post back on the Windows Forms UI Thread (if WinForms controls are embedded in a native UI where there is no current Form).
- _CreateEventLoop(string name)_: will create a new background thread to post messages to for the live time of the SynchronizationContext.
