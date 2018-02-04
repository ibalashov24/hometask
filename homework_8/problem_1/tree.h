#pragma once

#include <string>

namespace treeStuff
{
    struct SplayTree;

    SplayTree *createSplayTree();
    void deleteSplayTree(SplayTree *tree);

    // Inserts element (key; value) into the tree
    // or replaces value if it's already exists
    void insert(SplayTree *tree,
                const std::string &key,
                const std::string &value);
    bool isInTree(const SplayTree *tree,
                  const std::string &key);
    std::string getValue(SplayTree *tree,
                         const std::string &key);
    void deleteElement(SplayTree *tree,
                       const std::string &key);
}
