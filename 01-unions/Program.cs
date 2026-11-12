Pet[] pets = [new Dog("Rex"), new Cat("Tom"), new Bird("Tweety")];

foreach (Pet pet in pets)
{
    // Exhaustive: remove an arm and the compiler warns that the switch is not exhaustive.
    string description = pet switch
    {
        Dog d => $"{d.Name} is a dog",
        Cat c => $"{c.Name} is a cat",
        Bird b => $"{b.Name} is a bird",
    };

    Console.WriteLine(description);
}

// Gotcha: union declarations store the value in a single object field, so value-type cases are boxed.
IntOrString[] values = [42, "forty-two"];

foreach (IntOrString value in values)
{
    Console.WriteLine(value switch
    {
        int i => $"int: {i}",
        string s => $"string: {s}",
    });
}

public record class Cat(string Name);
public record class Dog(string Name);
public record class Bird(string Name);

public union Pet(Cat, Dog, Bird);
public union IntOrString(int, string);
