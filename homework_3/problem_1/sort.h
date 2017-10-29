#include <vector>

namespace Sorting
{
    // if size <= m.t.b.e then insertion sort will be used
    const int MAX_TOP_BORDER_ELEMENT = 10;

    void quicksort(std::vector<int>::iterator begin,
                   std::vector<int>::iterator end);
    void insertionSort(std::vector<int>::iterator begin,
                       std::vector<int>::iterator end);

    std::vector<int>::iterator _partition(std::vector<int>::iterator begin,
                                          std::vector<int>::iterator end);
}
