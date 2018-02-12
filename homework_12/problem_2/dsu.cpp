#include "dsu.h"

#include <numeric>

void initDSU(DSU &dsu)
{
	iota(dsu.begin(), dsu.end(), 0);
}

int getParent(DSU &dsu, int vertex)
{
	if (dsu[vertex] == vertex)
	{
		return vertex;
	}

	// Optimization in order to reach amortized O(log(n))
	dsu[vertex] = getParent(dsu, dsu[vertex]);

	return dsu[vertex];
}

bool inOneSubset(DSU &dsu, int firstVertex, int secondVertex)
{
	return (getParent(dsu, firstVertex) == getParent(dsu, secondVertex));
}

void unionSets(DSU &dsu, int firstVertex, int secondVertex)
{
	// TODO: Rang-based optimization (in order to reach amortized O(1))
	dsu[getParent(dsu, firstVertex)] = secondVertex;
}
