using SampleApp;

var calculator = new Calculator();

Console.WriteLine("=== SampleApp Calculator Demo ===");
Console.WriteLine();

Console.WriteLine($"Add(2, 3)       = {calculator.Add(2, 3)}");
Console.WriteLine($"Subtract(5, 3)  = {calculator.Subtract(5, 3)}");
Console.WriteLine($"Multiply(4, 5)  = {calculator.Multiply(4, 5)}");
Console.WriteLine($"Divide(10, 2)   = {calculator.Divide(10, 2)}");

try
{
    calculator.Divide(10, 0);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Divide(10, 0)   -> threw {nameof(DivideByZeroException)}: \"{ex.Message}\"");
}

Console.WriteLine();
Console.WriteLine("Prime check (2 through 20):");
for (int i = 2; i <= 20; i++)
{
    if (calculator.IsPrime(i))
    {
        Console.Write($"{i} ");
    }
}
Console.WriteLine();

Console.WriteLine();
Console.WriteLine("Done. Run 'dotnet test' from the solution root to run the automated test suite.");
