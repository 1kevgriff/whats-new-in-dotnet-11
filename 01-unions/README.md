# 01: C# unions

A union is a value that is one of a fixed set of types. The case types don't need a shared base class or interface, values convert implicitly, and the compiler checks that a `switch` handles every case.

```csharp
public union Pet(Cat, Dog, Bird);

Pet pet = new Dog("Rex");
string name = pet switch
{
    Dog d => d.Name,
    Cat c => c.Name,
    Bird b => b.Name,
};
```

## Run

```bash
dotnet run --project 01-unions
```

Remove one arm from the `switch` in `Program.cs` to see the exhaustiveness warning.

## Gotcha

A union declaration is a struct that stores its value in a single `object` field. Value-type cases, like `int` in `IntOrString`, are boxed. The docs show how to write a custom union that avoids boxing.

## Docs

- [Union types](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/union)
- [What's new in C# 15](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-15)
- [Tutorial: Explore union types](https://learn.microsoft.com/dotnet/csharp/whats-new/tutorials/unions)
