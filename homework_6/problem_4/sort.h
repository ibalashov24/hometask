#pragma once

#include "list.h"

#include <string>
#include <functional>

namespace sortingStuff
{
    void mergeSort(listStuff::PairStringList *list,
    		std::function<bool(const std::pair<std::string, std::string> &,
    		                   const std::pair<std::string, std::string> &)> cmp);
}
