using System.Diagnostics;

// PREVIEW: runtime async is opt-in in .NET 11 (<Features>runtime-async=on</Features> in the csproj).
// This demo shows behavior only: what the call stack looks like from deep inside an async chain.

Console.WriteLine("1) Live call stack from inside InnerAsync:");
await OuterAsync();

Console.WriteLine();
Console.WriteLine("2) Exception stack trace thrown from inside FailingInnerAsync:");
try
{
    await FailingOuterAsync();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.StackTrace);
}

static async Task OuterAsync()
{
    await Task.CompletedTask;
    await MiddleAsync();
}

static async Task MiddleAsync()
{
    await Task.CompletedTask;
    await InnerAsync();
}

static async Task InnerAsync()
{
    await Task.CompletedTask;
    var trace = new StackTrace(fNeedFileInfo: false);
    Console.WriteLine($"{trace.FrameCount} frames");
    Console.Write(trace);
}

static async Task FailingOuterAsync()
{
    await Task.CompletedTask;
    await FailingInnerAsync();
}

static async Task FailingInnerAsync()
{
    await Task.Yield();
    throw new InvalidOperationException("boom");
}
