#include <iostream>

using namespace std;

long long calcPowerIterative(long long number, int power)
{
    if (number == 0)
    {
        return 0;
    }

    long long result = 1;
    for (int i = 0; i < power; i++)
    {
        result *= number;
    }

    return result;
}

long long calcPowerFast(long long number, int power)
{
    if (number == 0)
    {
        return 0;
    }
    if (power == 0 || number == 1)
    {
        return 1;
    }

    if (power % 2 == 0)
    {
        const long long squareRoot = calcPowerFast(number, power / 2);
        return squareRoot * squareRoot;
    }
    else
    {
        return number * calcPowerFast(number, power - 1);
    }
}

int main()
{
    cout << "Enter a, n: " << endl;
    int exponent = 0;
    long long number = 0;
    cin >> number >> exponent;

    cout << "a^n = " << endl;
    cout << "Fast (O(log(n))): " << calcPowerFast(number, exponent) << endl;
    cout << "Iterative (O(n)): " << calcPowerIterative(number, exponent) << endl;

    return 0;
}
