#include <iostream>

using namespace std;

void swap(int &firstValue, int &secondValue)
{
    int tempVariable = firstValue;
    firstValue = secondValue;
    secondValue = tempVariable;
}


void reverseArray(int *x, int begin, int end)
{
    for (int i = 0; i < (end - begin) / 2 + 1; i++)
    {
        swap(x[i + begin], x[end - i]);
    }
}

void readArray(int *x, int begin, int end)
{
    for (int i = begin; i < end + 1; i++)
    {
        cout << "Enter x[" << i << "]: ";
        cin >> x[i];
    }
}

void printArray(const int *x, int begin, int end)
{
    for (int i = begin; i < end + 1; i++)
    {
        cout << "x[" << i << "] =  " << x[i] << endl;
    }
}

int main()
{
    int n = 0;
    int m = 0;
    cout << "Enter the sizes of the arrays m and n: ";
    cin >> m >> n;

    if (m == 0 || n == 0)
    {
        cout << "One of the arrays is empty!!!" << endl;
        return 1;
    }

    int *x = new int[m + n + 1]; // + 1, т.к. по условию нумерация с 1
    readArray(x, 1, m + n);

    reverseArray(x, 1, m+n);
    reverseArray(x, 1, n);
    reverseArray(x, n+1, m+n);

    cout << "The answer is: " << endl;
    printArray(x, 1, m+n);

    delete[] x;

    return 0;
}
