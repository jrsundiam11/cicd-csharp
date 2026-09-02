namespace SampleApp;

/// <summary>
/// A deliberately simple class so the CI/CD demo stays focused on the
/// pipeline mechanics rather than the business logic.
/// </summary>
public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }

        return a / b;
    }

    public bool IsPrime(int number)
    {
        if (number < 2) return false;

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false;
        }

        return true;
    }
}
