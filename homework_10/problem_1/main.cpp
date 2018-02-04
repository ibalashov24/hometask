#include <iostream>
#include <vector>
#include <fstream>
#include <algorithm>

using namespace std;

struct Road
{
	int destinationCity;
	int length;
};

struct RoadsFromCity
{
    // Iterator points to the first road to non-used city
    vector<Road>::iterator firstNonUsed;
    // Destination / Length
    vector<Road> roads;
    int country = -1;    // Neutral
};
using AdjacencyMatrix = vector<RoadsFromCity>;

class BadFileException : public exception
{
};

// Return pair <adjacency matrix / city count>
pair<AdjacencyMatrix, int> initializeMatrix()
{
    ifstream inputFile("input.txt");
    if (!inputFile.is_open())
    {
        throw BadFileException();
    }

    int citiesCount = 0;
    int roadsCount = 0;
    inputFile >> citiesCount >> roadsCount;
    AdjacencyMatrix matrix(citiesCount);

    for (int i = 0; i < roadsCount; i++)
    {
        int firstCity = 0;
        int secondCity = 0;
        int length = 0;
        inputFile >> firstCity >> secondCity >> length;

        matrix[firstCity - 1].roads.push_back(Road{secondCity - 1, length});
        matrix[secondCity - 1].roads.push_back(Road{firstCity - 1, length});
    }

    for (auto &city : matrix)
    {
        // TODO: Implement self-written QuickSort
        sort(city.roads.begin(), city.roads.end(), [](auto a, auto b)
                                                     {
                                                         // Sort by the length of way
                                                         return (a.length < b.length);
                                                     });
    }

    int countryCount = 0;
    inputFile >> countryCount;
    for (int i = 0; i < countryCount; ++i)
    {
        int capital = 0;
        inputFile >> capital;

        matrix[capital - 1].country = i;
    }

    return make_pair(matrix, countryCount);
}

void printCountries(const AdjacencyMatrix &matrix)
{
    cout << "Countries:" << endl;
    for (unsigned int i = 0; i < matrix.size(); ++i)
    {
        cout << "City " << i + 1 << " belongs to country ";
        cout << matrix[i].country + 1 << endl;
    }
}

void markBorders(AdjacencyMatrix &matrix, int countryCount)
{
    for (auto &city : matrix)
    {
        city.firstNonUsed = city.roads.begin();
    }

    int neutralCities = matrix.size() - countryCount;
    vector<vector<int> > countries(countryCount);
    for (unsigned int i = 0; i < matrix.size(); ++i)
    {
        if (matrix[i].country != -1)
        {
            countries[matrix[i].country].push_back(i);
        }
    }

    while (neutralCities != 0)
    {
        for (auto &country : countries)
        {
            // The city with neighbor who is the nearest one
            int nearestNeighbourCity = -1;
            for (unsigned int i = 0; i < country.size(); ++i)
            {
            	auto currentCity = matrix[country[i]];

            	auto targetCity = currentCity.firstNonUsed->destinationCity;
                // Choose first non-used (it can be invalidated after last iter.)
                while (currentCity.firstNonUsed != currentCity.roads.end() &&
                       matrix[targetCity].country != -1)
                {
                    ++currentCity.firstNonUsed;
                }

                if (currentCity.firstNonUsed != currentCity.roads.end() &&
                    (nearestNeighbourCity == -1 ||
                     currentCity.firstNonUsed->length <
                     matrix[nearestNeighbourCity].firstNonUsed->length))
                {
                    nearestNeighbourCity = country[i];
                }
            }

            if (nearestNeighbourCity != -1)
            {
            	auto targetCity = matrix[nearestNeighbourCity].firstNonUsed->destinationCity;

                country.push_back(targetCity);

                matrix[targetCity].country = matrix[nearestNeighbourCity].country;

                ++matrix[nearestNeighbourCity].firstNonUsed;
                --neutralCities;
            }
        }
    }
}

int main()
{
	// matrix / country count
    pair<AdjacencyMatrix, int> countryMap;
    try
    {
        countryMap = initializeMatrix();
    }
    catch (BadFileException const &e)
    {
        cout << "Unknown File!!!" << endl;
        return -1;
    }

    markBorders(countryMap.first, countryMap.second);

    printCountries(countryMap.first);

    return 0;
}

