using System.Globalization;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

string mode = args.Length > 0 ? args[0].ToLowerInvariant() : "both";

if (mode is "both" or "before")
{
    Console.WriteLine("=== Before unions: abstract record + `_ => throw` ===");
    BeforeUnions.Demo.Run();
    Console.WriteLine();
}

if (mode is "both" or "after")
{
    Console.WriteLine("=== With unions: `union PaymentResult(...)`, exhaustive switch ===");
    AfterUnions.Demo.Run();
}
