#include <vector>

using namespace std;

// Disjoint-set-union
using DSU = vector<int>;

// Initializes set
void initDSU(DSU &dsu);

// Checks if vertexes belongs to one subset
bool inOneSubset(DSU &dsu, int firstVertex, int secondVertex);

// Unites sets where given vertexes located
void unionSets(DSU &dsu, int firstVertex, int secondVertex);
