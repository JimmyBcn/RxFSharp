# Subjects

In addition to observables and observers, Rx provides **subjects**. **A subject acts both as an observer and as an observable**. As an observer, it can subscribe to one or more observables. As an observable, it can pass through the items it observes by reemitting them, and it can also emit new items. This characteristics allow a subject to **act as a proxy for a group of subscribers and a source** , so that **it can observe one or more sequences, perform a merge operation among them if needed, and broadcast the result to all its observers**.

Rx provides several varieties of subjects, like **ReplaySubject** , **BehaviourSubject** and **AsyncSubject**.

Rx framework internally uses subjects to allow warming cold observables (see hot and cold observables). This is implmented by the **Publish** , **Multicast** or **Replay** operators. When a subject subscribes to a cold observable, it has the effect of making himself a hot observable.

On the face of it, it seems that the Subject class is the one to use from now on wherever reactive is appropriate since it offers both behaviours. The caveat is that Subject exposes more functionality and state than you normally need (has extra responsibilities). Subjects are deemed the ‘Mutable Variables‘ in the reactive paradigm.

**Subjects should be only used to implement a custom hot observable with an own implementation for caching, buffering or time shifting**. Any other need to use an bservable can be achieved by using the already provided Rx operators.
