#pragma once

#include "list.h"

#include <string>

namespace sortingStuff
{
    void mergeSort(listStuff::PairStringList *list,
                   bool (*cmp)(const std::pair<std::string, std::string> &,
                               const std::pair<std::string, std::string> &));
}
