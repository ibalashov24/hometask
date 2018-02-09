#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <array>

using namespace std;

/* First line: state count
 * Configuration file format:
 * a[i][j] describes transition from the state i to the state a[i][j]
 * j (columns): <slash> <star> <non-comment-part>
 * a[i][j] == -1 means transition is not permitted
 */

enum struct Transition
{
	SLASH,
	STAR,
	NON_COMMENT_PART
};

using TransitionTable = vector<array<int, 3>>;

string readString()
{
	ifstream inputFile("input.txt");
	string input;
	getline(inputFile, input);
	inputFile.close();

	return input;
}

TransitionTable readTransitionTable()
{
	ifstream stateTable("stateTable.config");

	int stateCount = 0;
	stateTable >> stateCount;

	TransitionTable result(stateCount);

	for (auto &line : result)
	{
		for (auto &transition : line)
		{
			stateTable >> transition;
		}
	}

	return result;
}

int getNextState(const TransitionTable &transition,
				 const int currentState,
				 const char currentSymbol)
{
	Transition currentSituation;

	switch (currentSymbol)
	{
	case '/':
	{
		currentSituation = Transition::SLASH;
		break;
	}
	case '*':
	{
		currentSituation = Transition::STAR;
		break;
	}
	default:
	{
		currentSituation = Transition::NON_COMMENT_PART;
		break;
	}
	}

	return transition[currentState][static_cast<int>(currentSituation)];
}

void printAllComments(const TransitionTable &transitionTable,
					  const string &inputString)
{
	auto commentBegin = inputString.begin();
	int currentState = 0;

	bool commentFound = false;
	for (auto pos = inputString.begin(); pos != inputString.end(); ++pos)
	{
		const int oldState = currentState;
		currentState = getNextState(transitionTable, currentState, *pos);

		if (currentState == 1)
		{
			commentBegin = pos;
		}

		if (oldState == 3 && currentState == 0)
		{
			cout << string(commentBegin, pos + 1) << endl;
			commentFound = true;
		}
	}

	if (!commentFound)
	{
		cout << "There are no C++ comments" << endl;
	}
}

int main()
{
	auto transitionTable = readTransitionTable();
	auto input = readString();

	cout << "All comments: " << endl;
	printAllComments(transitionTable, input);

	return 0;
}
