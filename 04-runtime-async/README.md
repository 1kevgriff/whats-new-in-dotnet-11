# 04: Runtime async (PREVIEW)

> **Preview.** Runtime async is opt-in in .NET 11, and the .NET 11 announcement still calls it a preview feature. This demo shows behavior only. It makes no performance claims.

Same `async` and `await` source. With runtime async, the runtime handles suspension and resumption instead of a compiler-generated state machine. Turn it on in the project file:

```xml
<Features>runtime-async=on</Features>
```

You don't need `EnablePreviewFeatures`.

## Run

```bash
dotnet run --project 04-runtime-async
```

The app prints the live call stack from inside `InnerAsync`, then the stack trace of an exception thrown deep in an async chain.

To compare with the compiler state machine, turn runtime async off and rebuild:

```bash
dotnet build 04-runtime-async -p:RuntimeAsync=false --no-incremental
dotnet run --project 04-runtime-async --no-build
```

On SDK 11.0.100 the live call stack has 5 frames with runtime async on and 13 frames with it off. The off version shows each method twice, around `AsyncMethodBuilderCore.Start`. The exception stack trace is the same in both modes.

## Gotcha

The visible change is the live call stack (debugger, profiler, `new StackTrace()`). Exception stack traces were already cleaned up before .NET 11. It's opt-in, so test your dependencies and diagnostics before you turn it on.

## Docs

- [What's new in the .NET 11 runtime: Runtime async](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-11/runtime#runtime-async)
