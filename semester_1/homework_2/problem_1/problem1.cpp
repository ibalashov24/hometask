#include <iostream>

using namespace std;

long long fibonacciIterative(long long number)
{
    long long lastNumber = 0;
    long long current = 1;

    for (long long i = 2; i <= number; i++)
    {
        const long long temp = current;
        current = lastNumber + current;
        lastNumber = temp;
    }

    return current;
}

long long fibonacciRecursive(long long number)
{
    if (number == 1 || number == 2)
        return 1;

    return fibonacciRecursive(number - 1) + fibonacciRecursive(number - 2);
}

long long getFibonacci(int number, bool isUseIterative = true)
{
    if (isUseIterative)
        return fibonacciIterative(number);
    else
        return fibonacciRecursive(number);
}

int main()
{
    cout << "Enter n: ";
    int n = 0;
    cin >> n;

    cout << "F(n) = " << endl;
    const long long fibonacciFirst = getFibonacci(n, true);
    cout << "Iterative algorithm: " << fibonacciFirst << endl;

    const long long fibonacciSecond = getFibonacci(n, false);
    cout << "Recursive algorithm: " << fibonacciSecond << endl;

    // Feeling the difference here

    return 0;
}
