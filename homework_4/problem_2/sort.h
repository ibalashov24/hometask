#include <vector>

namespace sorting
{
    // if size <= m.t.b.e then insertion sort will be used
    const int MAX_TOP_BORDER_ELEMENT = 10;

    // Performing Quick Sort
    void quicksort(std::vector<int>::iterator begin,
                   std::vector<int>::iterator end);
    // Performing Insertion Sort
    void insertionSort(std::vector<int>::iterator begin,
                       std::vector<int>::iterator end);
}
