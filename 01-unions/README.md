# 01: C# unions

A union is a value that is one of a fixed set of types. The case types don't need a shared base class or interface, values convert implicitly, and the compiler checks that a `switch` handles every case.

This demo models a payment authorization result: approved, declined, or held for fraud review.

```csharp
public record class Approved(string AuthCode, decimal Amount);
public record class Declined(string Reason);
public record class NeedsReview(string CaseId, string Rule);
public union PaymentResult(Approved, Declined, NeedsReview);

string outcome = ProcessPayment(amount) switch
{
    Approved a => $"Ship the order (auth {a.AuthCode})",
    Declined d => $"Show the customer: {d.Reason}",
    NeedsReview r => $"Queue {r.CaseId} for fraud review",
};
```

## Run

```bash
dotnet run --project 01-unions          # Before, then After
dotnet run --project 01-unions before   # only the pre-union version
dotnet run --project 01-unions after    # only the union version
```

- `Before.cs`: the pre-C# 15 way, an abstract record with three subclasses.
- `After.cs`: the same three results as a `union`, plus a short `union CustomerId(int, string)` example.

## Before unions vs with unions

| Before unions (abstract base) | With unions |
| --- | --- |
| Anyone can add a subclass of `PaymentResult`. | The case list is closed. |
| No exhaustiveness check, so the `switch` needs `_ => throw`. | The `switch` is exhaustive with no default arm. |
| The cases must inherit the base, so they can't be existing types. | Cases can be any type, even `int` and `string`. |

A `_` default arm also hides new cases: add a fourth result to the base class and the compiler stays silent.

## Gotcha

Remove one arm from the `switch` in `After.cs` and the build warns:

```text
warning CS8509: The switch expression does not handle all possible values of its input type (it is not exhaustive). For example, the pattern 'AfterUnions.NeedsReview' is not covered.
```

It is a warning, not an error. Turn on `TreatWarningsAsErrors` (or `<WarningsAsErrors>CS8509</WarningsAsErrors>`) if you want the build to fail.

A union declaration also stores its value in a single `object` field, so value-type cases, like `int` in `CustomerId`, are boxed. The docs show how to write a custom union that avoids boxing.

## Docs

- [Union types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/union)
- [What's new in C# 15](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-15)
- [Tutorial: Explore union types](https://learn.microsoft.com/dotnet/csharp/whats-new/tutorials/unions)
