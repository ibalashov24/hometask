#include <iostream>
#include <cmath>

using namespace std;

int main()
{
	int a = 0;
	int b = 0;
	cout << "Enter operands: ";
	cin >> a >> b;

	if (b == 0)
	{
		cout << (a == 0 ? "a div b == 0" : "No answer") << endl;
		return 0;
	}

	// a == q * b + r
	int q = 0;
	while ((q + 1) * abs(b) <= abs(a))
	{
		++q;
	}

	if (a < 0 && abs(a) != q * abs(b))
	{
		++q;
	}

	if ((a < 0 && b > 0) || (a > 0 && b < 0))
	{
		q = -q;
	}

	cout << "a div b == " << q << endl;

	return 0;
}
