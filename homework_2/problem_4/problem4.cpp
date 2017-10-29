#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

const int MAX_SIZE = 100;
const int MAX_ELEMENT = 100;

void swap(int &a, int &b)
{
    int t = a;
    a = b;
    b = t;
}

void partition(int *array, int size, int index = 0)
{
    int posBegin = 0;
    int posEnd = size - 1;
    int median = array[index];

    while (posBegin < posEnd)
    {
        if (array[posBegin] > median && array[posEnd] <= median)
        {
            swap(array[posBegin++], array[posEnd--]);
        }
        else if (array[posBegin] > median)
        {
            posEnd--;
        }
        else if (array[posEnd] <= median)
        {
            posBegin++;
        }
        else
        {
            posBegin++;
            posEnd--;
        }
    }

    if (array[posBegin] > median)
    {
        posBegin--;
    }

    int equalMedianPos = 0;
    while (equalMedianPos < posBegin)
    {
        while (equalMedianPos < posBegin && array[posBegin] == median)
        {
            posBegin--;
        }

        if (equalMedianPos < posBegin && array[equalMedianPos] == median)
        {
            swap(array[equalMedianPos], array[posBegin]);
        }

        equalMedianPos++;
    }
}

int main()
{
    srand(time(nullptr));

    int size = rand() % MAX_SIZE + 1;
    int *array = new int[size];
    for (int i = 0; i < size; i++)
        array[i] = rand() % MAX_ELEMENT;

    cout << "Before: ";
    for (int i = 0; i < size; i++)
        cout << array[i] << ' ';
    cout << endl;
    cout << endl;

    partition(array, size);

    cout << "After: ";
    for (int i = 0; i < size; i++)
    {
        cout << array[i] << ' ';
    }

    delete[] array;

    return 0;
}
