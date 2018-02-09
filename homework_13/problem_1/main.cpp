#include <iostream>
#include <string>
#include <cctype>

using namespace std;

const int NOT_CORRECT = -1;
const int END_STATE = -2;

bool isRealNumber(const string &example)
{
	int currentState = 0;

	for (auto ch : example)
	{
		switch (currentState)
		{
		case 0:
		{
			currentState = isdigit(ch) ? 1 : NOT_CORRECT;
			break;
		}
		case 1:
		{
			if (ch == 'E')
			{
				currentState = 3;
			}
			else if (ch == '.')
			{
				currentState = 2;
			} else if (isdigit(ch))
			{
				currentState = 5;
			}
			else
			{
				currentState = NOT_CORRECT;
			}
			break;
		}
		case 2:
		{
			currentState = isdigit(ch) ? 6 : NOT_CORRECT;
			break;
		}
		case 3:
		{
			if (ch == '+' || ch == '-')
			{
				currentState = 4;
			}
			else if (isdigit(ch))
			{
				currentState = 5;
			}
			else
			{
				currentState = NOT_CORRECT;
			}
			break;
		}
		case 4:
		{
			currentState = isdigit(ch) ? 5 : NOT_CORRECT;
			break;
		}
		case 5:
		{
			if (!isdigit(ch))
			{
				currentState = NOT_CORRECT;
			}
			break;
		}
		case 6:
		{
			if (ch == 'E')
			{
				currentState = 3;
			}
			else if (!isdigit(ch))
			{
				currentState = NOT_CORRECT;
			}
			break;
		}
		default:
		{
			currentState = NOT_CORRECT;
		}
		}

		if (currentState == NOT_CORRECT)
		{
			break;
		}
	}

	bool isReal = true;
	if (currentState != 5 && currentState != 6)
	{
		isReal = false;
	}

	return isReal;
}

int main()
{
	cout << "Enter string (w/o spaces): " << endl;
	string inputString;
	cin >> inputString;

	if (isRealNumber(inputString))
	{
		cout << "Real number" << endl;
	}
	else
	{
		cout << "Not a real number!" << endl;
	}

	return 0;
}
