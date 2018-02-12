#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <iterator>

#include "dsu.h"

using namespace std;

struct Edge
{
	int from, to;
	int length;
};
using Graph = vector<Edge>;

ostream &operator<<(ostream &outputStream, Edge edge)
{
	outputStream << edge.from << ' ' << edge.to << ' ' << edge.length << endl;

	return outputStream;
}

Graph readGraph(ifstream &inputStream, int vertexCount)
{
	Graph result;

	for (int i = 0; i < vertexCount; ++i)
	{
		for (int j = 0; j < vertexCount; ++j)
		{
			int currentEdge = 0;
			inputStream >> currentEdge;

			if (j > i && currentEdge) // exists
			{
				result.push_back(Edge{i, j, currentEdge});
			}
		}
	}

	return result;
}

void printGraph(const Graph &graph)
{
	cout << "Edges of the spanning tree (<from> <to> <length>): " << endl;

	if (graph.empty())
	{
		cout << "No edges" << endl;
		return;
	}

	// Slow but cool
	copy(graph.begin(), graph.end(), ostream_iterator<Edge>(cout));
}

Graph makeSpanningTree(Graph &graph, int vertexCount)
{
	Graph result;

	sort(graph.begin(), graph.end(), [](const auto &a, const auto &b)
										{
											return (a.length < b.length);
										});

	// Disjoint-set-union
	DSU dsu(vertexCount);
	initDSU(dsu);

	for (auto e : graph)
	{
		if (!inOneSubset(dsu, e.from, e.to))
		{
			unionSets(dsu, e.from, e.to);
			result.push_back(e);
		}
	}

	return result;
}

int main()
{
	ifstream inputFile("input.txt");
	if (!inputFile.is_open())
	{
		cerr << "Unknown file!!!" << endl;
		return 1;
	}

	int vertexCount = 0;
	inputFile >> vertexCount;

	auto graph = readGraph(inputFile, vertexCount);
	inputFile.close();

	auto spanningTree = makeSpanningTree(graph, vertexCount);

	printGraph(spanningTree);

	return 0;
}
