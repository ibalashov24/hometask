#include <iostream>

using namespace std;

/**
    Sieve of Eratosthenes
**/
void splitPrime (bool *isPrimeNumber, const int maxBorder)
{
    // Mark they are all prime at the beginning
    for (int i = 0; i <= maxBorder; i++)
        isPrimeNumber[i] = true;

    isPrimeNumber[1] = isPrimeNumber[0] = false;
    for (int i = 2; i <= maxBorder; i++)
        if (isPrimeNumber[i])
            for (int j = 2 * i; j <= maxBorder; j += i)
                isPrimeNumber[j] = false;
}

int main()
{
    int maxBorder = 0;
    cout << "Enter the number: ";
    cin >> maxBorder;

    bool *isPrimeNumber = new bool[maxBorder + 1];
    splitPrime(isPrimeNumber, maxBorder);

    cout << "The prime numbers <= " << maxBorder << " are: " << endl;
    for (int i = 1; i <= maxBorder; i++)
        if (isPrimeNumber[i])
            cout << i << ' ';

    delete [] isPrimeNumber;

    return 0;
}
