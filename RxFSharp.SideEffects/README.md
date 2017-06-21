# Side effects

A side effect is a **change or action produced by the running process that is not considered part of the main business workflow** , such as modifying the UI or writing to a log. Side effects are the most common way that a program interacts with the outside world.

On the other side, a side effect can also be considered as an **accidental and undesirable change** of a component&#39;s state due to a bad design or hidden mistakes in the implementation.

**In Rx, we consider a side effect when the observable sequence chain changes the state of an external component**. **Inside the sequence chain, the state remains isolated and controlled**.

Rx provide out of the box operators both to facilitate introducing side effects, and also to isolate the sequence internal state, avoiding being mutated from the outside.

## Introducing side effects with Do operator

The **Do** operator allows you to specify various actions to be taken for each item of observable sequence (e.g., print or log the item, etc.). This is especially helpful when you are chaining many operators and you want to know what values are produced at each level.

Thus, **the Do operator brings the ability to listen in on the sequence, without the ability to modify it**. The **Do** method expresses intent of creating a side effect is created explicitly.

The side effect is launched for each new emitted value. This means that, when subscribing to a cold observable, each subscriber is going to create a new side effect. However, using the **Publish** operator, side effects can be shared among all subscribers.

## Preserving internal state with the Scan operator

The **Scan** operator works as an accumulator for live sequences, so that it returns an accumulated value (or _resulting state_) for each new value (_state_) of the source sequence.

Scan takes two parameters: the initial state and a closure accepting two parameters, the last state you had and the value that was just emitted. **Scan** runs that closure each time it gets a new value. That closure, by definition, cannot use any external value, and the closure returns a new state, so **each incoming state remains immutable, the closure keeps the mutable state isolated, and it creates and return a brand new reduced state.**
