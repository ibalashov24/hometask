#include "sort.h"

void Sorting::_swap(int &a, int &b)
{
    int t = a;
    a = b;
    b = t;
}

void Sorting::bubbleSort(int *array, int size)
{
    for (int i = 1; i < size; i++)
        for (int j = i; j > 0; j--)
            if (array[j] < array[j-1])
            {
                Sorting::_swap(array[j], array[j-1]);
            }
}

void Sorting::countingSort(int *array, int size)
{
    int minElement = array[0];
    int maxElement = array[0];
    for (int i = 1; i < size; i++)
    {
        if (array[i] < minElement)
        {
            minElement = array[i];
        }
        if (array[i] > maxElement)
        {
            maxElement = array[i];
        }
    }

    int *repeats = new int[maxElement - minElement + 1];
    for (int i = 0; i < size; i++)
    {
        repeats[array[i] - minElement]++;
    }

    int insertPosition = 0;
    for (int i = 0; i <= maxElement - minElement; i++)
        for (int j = 0; j < repeats[i]; j++)
        {
            array[insertPosition++] = i + minElement;
        }

    delete [] repeats;
}
