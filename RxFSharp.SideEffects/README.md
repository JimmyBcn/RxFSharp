# Subjects

In addition to observables and observers, Rx provides **subjects**. **A subject acts both as an observer and as an observable**. As an observer, it can subscribe to one or more observables. As an observable, it can pass through the items it observes by reemitting them, and it can also emit new items. This characteristics allow a subject to **act as a proxy for a group of subscribers and a source** , so that **it can observe one or more sequences, perform a merge operation among them if needed, and broadcast the result to all its observers**.

Rx provides several varieties of subjects, like **ReplaySubject** , **BehaviourSubject** and **AsyncSubject**.

Rx framework internally uses subjects to allow warming cold observables (see hot and cold observables). This is implmented by the **Publish** , **Multicast** or **Replay** operators. When a subject subscribes to a cold observable, it has the effect of making himself a hot observable.

 ![](data:image/*;base64,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)

**Subjects should be only used to implement a custom hot observable with an own implementation for caching, buffering or time shifting**. Any other need to use a hot observable can be achieved by warming a cold observable.