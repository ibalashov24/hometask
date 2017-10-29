#include <iostream>

#include "sort.h"

int main()
{
    std::cout << "Enter size (> 0): ";
    int size = 0;
    std::cin >> size;

    std::cout << "Enter non-empty array with this size: ";
    int *array = new int[size];
    for (int i = 0; i < size; i++)
    {
        std::cin >> array[i];
    }

    std::cout << "Enter 1 if Bubble Sort, 0 if Counting sort: " << std::endl;
    bool isBubbleSort = false;
    std::cin >> isBubbleSort;

    if (isBubbleSort)
        Sorting::bubbleSort(array, size);
    else
        Sorting::countingSort(array, size);

    std::cout << "Result: ";
    for (int i = 0; i < size; i++)
        std::cout << array[i] << ' ';

    delete [] array;
}
