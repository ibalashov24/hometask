#include <iostream>
#include <string>
#include <cctype>

using namespace std;

enum struct State
{
	NOT_CORRECT,
	BEGIN_POINT,
	INTEGER_PART,
	DECIMAL_POINT,
	EXPONENT_SIGN,
	PLUS_MINUS_SIGN,
	EXPONENT_PART,
	REAL_PART
};

bool isRealNumber(const string &example)
{
	State currentState = State::BEGIN_POINT;

	for (auto ch : example)
	{
		switch (currentState)
		{
		case State::BEGIN_POINT:
		{
			currentState = isdigit(ch) ? State::INTEGER_PART : State::NOT_CORRECT;
			break;
		}
		case State::INTEGER_PART:
		{
			if (ch == 'E')
			{
				currentState = State::EXPONENT_SIGN;
			}
			else if (ch == '.')
			{
				currentState = State::DECIMAL_POINT;
			} /*else if (isdigit(ch))
			{
				currentState = State::EXPONENT_PART;
			}*/
			else if (!isdigit(ch))
			{
				currentState = State::NOT_CORRECT;
			}
			break;
		}
		case State::DECIMAL_POINT:
		{
			currentState = isdigit(ch) ? State::REAL_PART : State::NOT_CORRECT;
			break;
		}
		case State::EXPONENT_SIGN:
		{
			if (ch == '+' || ch == '-')
			{
				currentState = State::PLUS_MINUS_SIGN;
			}
			else if (isdigit(ch))
			{
				currentState = State::EXPONENT_PART;
			}
			else
			{
				currentState = State::NOT_CORRECT;
			}
			break;
		}
		case State::PLUS_MINUS_SIGN:
		{
			currentState = isdigit(ch) ? State::EXPONENT_PART
							: State::NOT_CORRECT;
			break;
		}
		case State::EXPONENT_PART:
		{
			if (!isdigit(ch))
			{
				currentState = State::NOT_CORRECT;
			}
			break;
		}
		case State::REAL_PART:
		{
			if (ch == 'E')
			{
				currentState = State::EXPONENT_SIGN;
			}
			else if (!isdigit(ch))
			{
				currentState = State::NOT_CORRECT;
			}
			break;
		}
		default:
		{
			currentState = State::NOT_CORRECT;
		}
		}

		if (currentState == State::NOT_CORRECT)
		{
			break;
		}
	}

	bool isReal = true;
	if (currentState != State::EXPONENT_PART
			&& currentState != State::REAL_PART
			&& currentState != State::INTEGER_PART)
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
