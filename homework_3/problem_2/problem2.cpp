#include <iostream>
#include <vector>
#include <utility>

using namespace std;

const int TOP_BORDER_INSERTION_SORT = 10;
const int MAX_ELEMENT = 1000000001;

void insertionSort(vector<int>::iterator begin,
                   vector<int>::iterator end)
{
    for (auto pos_i = begin + 1; pos_i != end; ++pos_i)
    {
        for (auto pos_j = pos_i; pos_j >= begin + 1; --pos_j)
        {
            if (*pos_j < *(pos_j - 1))
            {
                iter_swap(pos_j, pos_j - 1);
            }
        }
    }
}

vector<int>::iterator partition(vector<int>::iterator begin,
                                vector<int>::iterator end)
{
    int median = *(begin + (end - begin) / 2);

    auto insertionPos = begin;
    for (auto pos = begin; pos != end; ++pos)
    {
        if (*pos <= median)
        {
            iter_swap(pos, insertionPos);
            ++insertionPos;
        }
    }

    return insertionPos;
}

void quicksort(vector<int>::iterator begin,
               vector<int>::iterator end)
{
    if (end - begin <= 1)
    {
        return;
    }

    if (end - begin <= TOP_BORDER_INSERTION_SORT)
    {
        insertionSort(begin, end);
        return;
    }

    auto median = partition(begin, end);

    quicksort(begin, median - 1);
    quicksort(median, end);
}

bool isIncludes(vector<int>::const_iterator begin,
                vector<int>::const_iterator end,
                int element)
{
    while (end - begin > 1)
    {
        auto median = begin + (end - begin) / 2;

        if (*median > element)
        {
            end = median;
        }
        else
        {
            begin = median;
        }
    }

    return (*begin == element || *end == element);
}

void printVector(const vector<int> &vectorToPrint, char delimiter = ' ')
{
    for (auto e : vectorToPrint)
    {
        cout << e << delimiter;
    }
    cout << endl;
}

void generateRandom(vector<int> &container)
{
    for (int &e : container)
    {
        e = rand() % MAX_ELEMENT;
    }
}

int main()
{
   // srand(time(nullptr));

    int n = 0;
    int k = 0;
    cout << "Enter n and k: ";
    cin >> n >> k;

    vector<int> elements(n);
    generateRandom(elements);

    vector<int> checkList(k);
    generateRandom(checkList);

    cout << "Array of elements: ";
    printVector(elements);

    cout << "Checklist: ";
    printVector(checkList);

    quicksort(elements.begin(), elements.end());

    cout << "First array includes: ";
    bool isEmpty = true;
    for (int e : checkList)
    {
        if (isIncludes(elements.begin(), elements.end(), e))
        {
            cout << e << ' ';
            isEmpty = false;
        }
    }
    if (isEmpty)
    {
        cout << "nothing" << endl;
    }

    return 0;
}

