#include <iostream>
#include <vector>
#include <fstream>
#include <algorithm>

using namespace std;

struct RoadsFromCity
{
    // Iterator points to the first road to non-used city
    vector<pair<int, int> >::iterator firstNonUsed;
    // Destination / Length
    vector<pair<int, int> > roads;
    int country = -1;    // Neutral
};
using AdjacencyMatrix = vector<RoadsFromCity>;

class BadFileException
{
};

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

        matrix[firstCity - 1].roads.push_back(make_pair(secondCity - 1, length));
        matrix[secondCity - 1].roads.push_back(make_pair(firstCity - 1, length));
    }

    for (auto &city : matrix)
    {
        // TODO: Implement self-written QuickSort
        sort(city.roads.begin(), city.roads.end(), [](auto a, auto b)
                                                     {
                                                         // Sort by the length of way
                                                         return (a.second < b.second);
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
                // Choose first non-used (it can be invalidated after last iter.)
                while (matrix[country[i]].firstNonUsed != matrix[country[i]].roads.end() &&
                       matrix[matrix[country[i]].firstNonUsed->first].country != -1)
                {
                    ++matrix[country[i]].firstNonUsed;
                }

                if (matrix[country[i]].firstNonUsed != matrix[country[i]].roads.end() &&
                    (nearestNeighbourCity == -1 ||
                     matrix[country[i]].firstNonUsed->second <
                     matrix[nearestNeighbourCity].firstNonUsed->second))
                {
                    nearestNeighbourCity = country[i];
                }
            }

            if (nearestNeighbourCity != -1)
            {
                country.push_back(matrix[nearestNeighbourCity].firstNonUsed->first);
                matrix[matrix[nearestNeighbourCity].firstNonUsed->first].country =
                                            matrix[nearestNeighbourCity].country;

                ++matrix[nearestNeighbourCity].firstNonUsed;
                --neutralCities;
            }
        }
    }
}

int main()
{
    pair<AdjacencyMatrix, int> countryMap;
    try
    {
        countryMap = initializeMatrix();
    }
    catch (BadFileException)
    {
        cout << "Unknown File!!!" << endl;
        return -1;
    }

    markBorders(countryMap.first, countryMap.second);

    printCountries(countryMap.first);

    return 0;
}

