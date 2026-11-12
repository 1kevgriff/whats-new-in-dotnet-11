JobStatus[] statuses =
[
    new Queued(),
    new Running(42),
    new Completed(TimeSpan.FromSeconds(3.5)),
    new Failed("disk full"),
    new RetryableFailed("timeout", 2),
];

foreach (JobStatus status in statuses)
{
    Console.WriteLine($"{status.GetType().Name,-16} {Describe(status)}");
}

// No discard arm: the compiler knows every direct subclass of a closed class.
static string Describe(JobStatus status) => status switch
{
    Queued => "waiting to start",
    Running(var percent) => $"{percent}% complete",
    Completed(var elapsed) => $"finished in {elapsed.TotalSeconds:F1}s",
    Failed(var error) => $"failed: {error}",
};

public closed record class JobStatus;
public record class Queued : JobStatus;
public record class Running(int PercentComplete) : JobStatus;
public record class Completed(TimeSpan Elapsed) : JobStatus;
public record class Failed(string Error) : JobStatus;

// Gotcha: closed applies to direct subclasses only. Failed is not closed or sealed,
// so code in another assembly could still derive from it, like this type does.
public record class RetryableFailed(string Error, int Attempts) : Failed(Error);
