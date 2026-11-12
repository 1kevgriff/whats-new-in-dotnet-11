# 02: Closed classes

A `closed` class is a public base class that only types in the same assembly can derive from directly. The compiler knows every direct subclass, so a `switch` can be exhaustive without a discard (`_`) arm.

```csharp
public closed record class JobStatus;
public record class Queued : JobStatus;
public record class Running(int PercentComplete) : JobStatus;
public record class Completed(TimeSpan Elapsed) : JobStatus;
public record class Failed(string Error) : JobStatus;
```

A closed class is implicitly abstract. You can't also mark it `sealed`, `static`, or `abstract`.

## Run

```bash
dotnet run --project 02-closed-classes
```

## Gotcha

`closed` applies to direct subclasses only. `Failed` is not closed or sealed, so another assembly can still derive from it (see `RetryableFailed`). To lock the whole tree, mark intermediate types `closed` or seal the leaves.

Use a union when the types aren't related. Use `closed` when they already share a base class.

## Docs

- [closed modifier](https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/closed)
- [What's new in C# 15: Closed hierarchies](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-15#closed-hierarchies)
