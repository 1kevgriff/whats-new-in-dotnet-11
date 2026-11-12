namespace BeforeUnions;

// Before C# 15: an abstract base type stands in for "one of these results".
public abstract record PaymentResult;
public sealed record Approved(string AuthCode, decimal Amount) : PaymentResult;
public sealed record Declined(string Reason) : PaymentResult;
public sealed record NeedsReview(string CaseId, string Rule) : PaymentResult;

public static class Demo
{
    public static PaymentResult ProcessPayment(decimal amount) => amount switch
    {
        <= 500m => new Approved("AUTH-7F3K", amount),
        <= 5_000m => new NeedsReview("CASE-1042", "High value for a new customer"),
        _ => new Declined("Insufficient funds"),
    };

    public static void Run()
    {
        foreach (decimal amount in new[] { 49.99m, 12_000m, 1_250m })
        {
            string outcome = ProcessPayment(amount) switch
            {
                Approved a => $"Ship the order (auth {a.AuthCode}, {a.Amount:C})",
                Declined d => $"Show the customer: {d.Reason}",
                NeedsReview r => $"Queue {r.CaseId} for fraud review: {r.Rule}",
                // The compiler can't know the hierarchy is closed, so a default is required.
                _ => throw new InvalidOperationException("Unknown payment result"),
            };

            Console.WriteLine($"{amount,10:C}  ->  {outcome}");
        }
    }
}
