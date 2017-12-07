#include "tree.h"

#include <iostream>

struct SplayTreeElement
{
    SplayTreeElement *leftSon = nullptr;
    SplayTreeElement *rightSon = nullptr;

    unsigned int keyHash;
    std::string value;
};

struct treeStuff::SplayTree
{
    SplayTreeElement *top = nullptr;
};

treeStuff::SplayTree *treeStuff::createSplayTree()
{
    auto newTree = new SplayTree;
    return newTree;
}

void recursiveDelete(SplayTreeElement *element)
{
    if (element == nullptr)
    {
        return;
    }

    recursiveDelete(element->rightSon);
    recursiveDelete(element->leftSon);

    delete element;
}

void treeStuff::deleteSplayTree(SplayTree *tree)
{
    if (tree != nullptr)
    {
        recursiveDelete(tree->top);
        delete tree;
    }
}

unsigned int getHash(const std::string &input)
{
    const unsigned int MODULO = 1190494759; // Prime number #60000000
    const unsigned int PRIME = 541; // Prime number #100

    unsigned int result = 0;
    unsigned int power = 1;
    for (auto e : input)
    {
        result = (result + ((power * e) % MODULO)) % MODULO;
        power = (power * PRIME) % MODULO;
    }

    return result;
}

void makeZig(SplayTreeElement *a, SplayTreeElement *b)
{
    if (a->rightSon == b)
    {
        a->rightSon = b->leftSon;
        b->leftSon = a;
    }
    else
    {
        a->leftSon = b->rightSon;
        b->rightSon = a;
    }
}

void makeZigZag(SplayTreeElement *a, SplayTreeElement *b, SplayTreeElement *c)
{

    if (a->rightSon == b)
    {
        std::swap(a, b);
    }

    b->rightSon = c->leftSon;
    a->leftSon = c->rightSon;
    c->leftSon = b;
    c->rightSon = a;
}

void makeZigZig(SplayTreeElement *a, SplayTreeElement *b, SplayTreeElement *c)
{
    if (a->leftSon == b)
    {
        a->leftSon = b->rightSon;
        b->leftSon = c->rightSon;
        c->rightSon = b;
        b->rightSon = a;
    }
    else
    {
        a->rightSon = b->leftSon;
        b->rightSon = c->leftSon;
        b->leftSon = a;
        c->leftSon = b;
    }
}

bool isZigZig(SplayTreeElement *a, SplayTreeElement *b, SplayTreeElement *c)
{
    return ((b == a->leftSon && c == b->leftSon) ||
            (b == a->rightSon && c == b->rightSon));
}

std::pair<SplayTreeElement *, SplayTreeElement *>
performRotates(treeStuff::SplayTree *tree,
               SplayTreeElement *currentElement,
               SplayTreeElement *endElement)
{
    if (currentElement == endElement)
    {
        return std::make_pair(endElement, nullptr);
    }

    auto nextElement = (endElement->keyHash > currentElement->keyHash ?
                        currentElement->rightSon : currentElement->leftSon);

    auto nextTwo = performRotates(tree, nextElement, endElement);

    // It means that some rotate was performed at last step
    if (nextTwo.second == nullptr)
    {
        if (endElement->keyHash > currentElement->keyHash)
        {
            currentElement->rightSon = endElement;
        }
        else
        {
            currentElement->leftSon = endElement;
        }
    }

    if (currentElement == tree->top && nextTwo.first == endElement)
    {
        makeZig(currentElement, nextTwo.first);

        return std::make_pair(endElement, nullptr);
    }

    if (nextTwo.second == endElement)
    {
        if (isZigZig(currentElement, nextTwo.first, nextTwo.second))
        {
            makeZigZig(currentElement, nextTwo.first, nextTwo.second);
        }
        else
        {
            makeZigZag(currentElement, nextTwo.first, nextTwo.second);
        }

        return std::make_pair(endElement, nullptr);
    }

    return std::make_pair(currentElement, endElement);
}

void splay(treeStuff::SplayTree *tree,
           SplayTreeElement *element)
{
    if (element == nullptr)
    {
        return;
    }

    performRotates(tree, tree->top, element);
    tree->top = element;
}

// Returns parent of the element
// (`nullptr` if tree is empty or element == top)
SplayTreeElement *findParent(const treeStuff::SplayTree *tree,
                             unsigned int hash)
{
    auto nextElement = tree->top;
    decltype(nextElement) currentElement = nullptr;
    while (nextElement != nullptr && nextElement->keyHash != hash)
    {
        currentElement = nextElement;

        if (hash > nextElement->keyHash)
        {
            nextElement = nextElement->rightSon;
        }
        else if (hash < nextElement->keyHash)
        {
            nextElement = nextElement->leftSon;
        }
    }

    return currentElement;
}

SplayTreeElement *createNewElement(unsigned int key,
                                   const std::string &value)
{
    auto newElement = new SplayTreeElement{nullptr, nullptr, key, value};
    return newElement;
}

void treeStuff::insert(SplayTree *tree,
                       const std::string &key,
                       const std::string &value)
{
    auto hash = getHash(key);
    auto parentPosition = findParent(tree, hash);

    if (parentPosition == nullptr)
    {
        if (tree->top == nullptr)
        {
            tree->top = createNewElement(hash, value);
        }
        else
        {
            tree->top->value = value;
        }

        return;
    }

    auto &insertPosition = (hash > parentPosition->keyHash ?
                            parentPosition->rightSon : parentPosition->leftSon);

    if (insertPosition == nullptr)
    {
        insertPosition = createNewElement(hash, value);
    }
    else
    {
        insertPosition->value = value;
    }

    splay(tree, insertPosition);
}

bool treeStuff::isInTree(const SplayTree *tree,
                         const std::string &key)
{
    auto hash = getHash(key);
    auto parentPosition = findParent(tree, hash);

    bool isTop =    parentPosition == nullptr && tree->top != nullptr &&
                    tree->top->keyHash == hash;

    bool isSon = false;
    if (parentPosition != nullptr)
    {
        auto &checkSon =    (hash > parentPosition->keyHash ?
                            parentPosition->rightSon : parentPosition->leftSon);

        isSon = checkSon != nullptr && checkSon->keyHash == hash;
    }

    return isTop || isSon;
}

std::string treeStuff::getValue(const SplayTree *tree,
                                const std::string &key)
{
    auto hash = getHash(key);
    auto parentPosition = findParent(tree, hash);

    std::string result = "";

    if (parentPosition == nullptr && tree->top != nullptr)
    {
        result = tree->top->value;
    }

    if (parentPosition != nullptr)
    {
        auto &resultSon = (hash > parentPosition->keyHash ?
                           parentPosition->rightSon : parentPosition->leftSon);

        if (resultSon != nullptr)
        {
            result = resultSon->value;
        }
    }

    return result;
}

/**
    Prepare a vertex that will be in the tree at the site of the removed vertex
*/
SplayTreeElement *prepareSubstituteVertex(SplayTreeElement *erasedElement)
{
    auto currentElement = erasedElement;
    auto lastElement = currentElement;

    if (currentElement->leftSon != nullptr)
    {
        currentElement = currentElement->leftSon;
        while (currentElement->rightSon != nullptr)
        {
            lastElement = currentElement;
            currentElement = currentElement->rightSon;
        }

        if (erasedElement == lastElement)
        {
            lastElement->leftSon = currentElement->leftSon;
        }
        else
        {
            lastElement->rightSon = currentElement->leftSon;
        }
    }
    else if (currentElement->rightSon != nullptr)
    {
        currentElement = currentElement->rightSon;
        while (currentElement->leftSon != nullptr)
        {
            lastElement = currentElement;
            currentElement = currentElement->leftSon;
        }

        if (erasedElement == lastElement)
        {
            lastElement->rightSon = currentElement->rightSon;
        }
        else
        {
            lastElement->leftSon = currentElement->rightSon;
        }
    }

    return currentElement;
}

void treeStuff::deleteElement(SplayTree *tree,
                              const std::string &key)
{
    auto hash = getHash(key);
    auto erasedElementParent = findParent(tree, hash);
    auto erasedElement = erasedElementParent;
    if (erasedElementParent == nullptr)
    {
        erasedElement = tree->top;
    }
    else
    {
        erasedElement = (hash > erasedElementParent->keyHash) ?
                                        erasedElementParent->rightSon:
                                        erasedElementParent->leftSon;
    }

    if (erasedElement == nullptr || erasedElement->keyHash != hash)
    {
        return;
    }

    auto currentElement = prepareSubstituteVertex(erasedElement);

    if (currentElement == erasedElement)
    {
        currentElement = nullptr;
    }
    else
    {
        currentElement->rightSon = erasedElement->rightSon;
        currentElement->leftSon = erasedElement->leftSon;
    }

    if (erasedElementParent == nullptr)
    {
        tree->top = currentElement;
    }
    else if (erasedElement->keyHash > erasedElementParent->keyHash)
    {
        erasedElementParent->rightSon = currentElement;
    }
    else
    {
        erasedElementParent->leftSon = currentElement;
    }

    delete erasedElement;

    splay(tree, erasedElementParent);
}
