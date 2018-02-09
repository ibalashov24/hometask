#include <iostream>
#include <cmath>

using namespace std;

int MAX_VALUE = 100000;
int MIN_VALUE = -100000;

bool isInRange(int r, int b)
{
	return (r >= 0 && r < abs(b));
}

int main()
{
    int a = 0;
    int b = 0;
    cout << "Введите целые a и b: ";
    cin >> a >> b;

    if (b == 0 && a != 0)
    {
    	cout << "Нет ответа" << endl;
    	return 0;
    }

    int left = MIN_VALUE;
    int right = MAX_VALUE;
    int r_left = a - left * b;
    int r_right = a - right * b;

    while (!isInRange(r_left, b) && !isInRange(r_right, b))
    {
    	const int q = (left + right) / 2;
    	const int r = a - q * b;

    	if ((b > 0 && r >= b) || (b < 0 && r < 0))
    	{
    		left = q;
    	}
    	else
    	{
    		right = q;
    	}

    	r_left = a - left * b;
    	r_right = a - right * b;
    }

    cout << "Остаток от деления: ";
    if (isInRange(r_left, b))
    {
    	cout << r_left << endl;
    }
    else
    {
    	cout << r_right << endl;
    }

    return 0;
}
