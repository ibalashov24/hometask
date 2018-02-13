#pragma once

#include <string>

namespace treeStuff
{
    struct SplayTree;

    // Constructs new empty splay tree
    SplayTree *createSplayTree();

    // Fully deletes splay tree
    void deleteSplayTree(SplayTree *tree);

    // Inserts element (key; value) into the tree
    // or replaces value if it's already exists
    void insert(SplayTree *tree,
                const std::string &key,
                const std::string &value);

    // Check if element with given key is already in tree
    bool isInTree(SplayTree *tree,
                  const std::string &key);

    // Returns value which corresponds to key (or empty string if not exists)
    std::string getValue(SplayTree *tree,
                         const std::string &key);

    // Erases element with given key
    void deleteElement(SplayTree *tree,
                       const std::string &key);
}
