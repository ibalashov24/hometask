#include <iostream>

using namespace std;

int main()
{
    int a = 0;
    int b = 0;
    cout << "Введите целые a и b: ";
    cin >> a >> b;

    bool isNegative = false;
    if (a < 0) 
    {
        a = -a;
        isNegative = true;
    }
    if (b < 0)
    {
        b = -b;
    }

    int result = 0;
    while (a >= b)
    {
        a -= b;
        result++;
    }

    cout << "Неполное частное от деления a на b: ";
    cout << (isNegative ? result - b : result) << endl;

    return 0;
}
