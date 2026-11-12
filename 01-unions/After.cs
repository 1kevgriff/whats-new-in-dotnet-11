namespace AfterUnions;

// C# 15: the case types need no shared base, and the set is closed.
public record class Approved(string AuthCode, decimal Amount);
public record class Declined(string Reason);
public record class NeedsReview(string CaseId, string Rule);
public union PaymentResult(Approved, Declined, NeedsReview);

// Cases can be existing types. Value-type cases like int are boxed.
public union CustomerId(int, string);

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
            // Exhaustive: no default arm. Remove an arm and the compiler warns.
            string outcome = ProcessPayment(amount) switch
            {
                Approved a => $"Ship the order (auth {a.AuthCode}, {a.Amount:C})",
                Declined d => $"Show the customer: {d.Reason}",
                NeedsReview r => $"Queue {r.CaseId} for fraud review: {r.Rule}",
            };

            Console.WriteLine($"{amount,10:C}  ->  {outcome}");
        }

        Console.WriteLine();
        CustomerId[] ids = [1042, "cus_9XkT2"];
        foreach (CustomerId id in ids)
        {
            Console.WriteLine(id switch
            {
                int legacy => $"Legacy customer #{legacy}",
                string stripe => $"Stripe customer {stripe}",
            });
        }
    }
}
