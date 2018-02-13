include <iostream>

using namespace std;

int main()
{
    double x = 0.0;

    cout << "Введите переменную x: ";
    cin >> x;

    double squareX = x * x;
    double polynomialRoot = 1 + (x + squareX) * (1 + squareX);

    cout << "Значение многочлена: " << polynomialRoot << endl;

    return 0;
}
