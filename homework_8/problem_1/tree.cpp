#include "tree.h"

#include <iostream>

struct SplayTreeElement
{
    SplayTreeElement *leftSon = nullptr;
    SplayTreeElement *rightSon = nullptr;

    const std::string key;
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

    auto nextElement = (endElement->key > currentElement->key ?
                        currentElement->rightSon : currentElement->leftSon);
    auto nextTwo = performRotates(tree, nextElement, endElement);

    // It means that some rotate was performed at last step
    if (nextTwo.second == nullptr)
    {
    	auto &newSon = (endElement->key > currentElement->key ?
    						currentElement->rightSon :
							currentElement->leftSon);
        newSon = endElement;
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
                             const std::string &key)
{
    auto nextElement = tree->top;
    decltype(nextElement) currentElement = nullptr;
    while (nextElement != nullptr && nextElement->key != key)
    {
        currentElement = nextElement;

        if (key > nextElement->key)
        {
            nextElement = nextElement->rightSon;
        }
        else if (key < nextElement->key)
        {
            nextElement = nextElement->leftSon;
        }
    }

    return currentElement;
}

SplayTreeElement *createNewElement(const std::string &key,
                                   const std::string &value)
{
    auto newElement = new SplayTreeElement{nullptr, nullptr, key, value};
    return newElement;
}

void treeStuff::insert(SplayTree *tree,
                       const std::string &key,
                       const std::string &value)
{
    auto parentPosition = findParent(tree, key);

    if (parentPosition == nullptr)
    {
        if (tree->top == nullptr)
        {
            tree->top = createNewElement(key, value);
        }
        else
        {
            tree->top->value = value;
        }

        return;
    }

    auto &insertPosition = (key > parentPosition->key ?
                            parentPosition->rightSon : parentPosition->leftSon);

    if (insertPosition == nullptr)
    {
        insertPosition = createNewElement(key, value);
    }
    else
    {
        insertPosition->value = value;
    }

    splay(tree, insertPosition);
}

bool treeStuff::isInTree(SplayTree *tree,
                         const std::string &key)
{
    auto parentPosition = findParent(tree, key);

    bool isTop = (parentPosition == nullptr &&
    			  tree->top != nullptr &&
                  tree->top->key == key);

    bool isSon = false;
    if (parentPosition != nullptr)
    {
        auto &checkSon = (key > parentPosition->key ?
                         parentPosition->rightSon : parentPosition->leftSon);

        isSon = checkSon != nullptr && checkSon->key == key;

        if (isSon)
        {
        	splay(tree, checkSon);
        }
    }

    return (isSon || isTop);
}

std::string treeStuff::getValue(SplayTree *tree,
                                const std::string &key)
{
    auto parentPosition = findParent(tree, key);

    SplayTreeElement *result = nullptr;

    if (parentPosition == nullptr && tree->top != nullptr)
    {
        result = tree->top;
    }

    if (parentPosition != nullptr)
    {
        result = (key > parentPosition->key ?
                  parentPosition->rightSon : parentPosition->leftSon);
    }

    if (result)
    {
    	splay(tree, result);
    	return result->value;
    }
    else
    {
    	// In order to speed up next "bad" search
    	splay(tree, parentPosition);
    	return "";
    }
}

/**
    Prepare a vertex that will be in the tree at the site of the removed vertex
*/
SplayTreeElement *prepareSubstituteVertex(SplayTreeElement *erasedElement)
{
	auto currentElement = erasedElement;

	if (currentElement->leftSon == nullptr &&
		currentElement->rightSon == nullptr)
	{
		return nullptr;
	}

	if (currentElement->leftSon == nullptr)
	{
		return currentElement->rightSon;
	}
	else if (currentElement->rightSon == nullptr)
	{
		return currentElement->leftSon;
	}
	else
	{
		auto lastElement = currentElement;
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

		currentElement->leftSon = erasedElement->leftSon;
		currentElement->rightSon = erasedElement->rightSon;

		return currentElement;
	}
}

void treeStuff::deleteElement(SplayTree *tree,
                              const std::string &key)
{
    auto erasedElementParent = findParent(tree, key);
    auto erasedElement = erasedElementParent;
    if (erasedElementParent == nullptr)
    {
        erasedElement = tree->top;
    }
    else
    {
        erasedElement = (key > erasedElementParent->key) ?
                                        erasedElementParent->rightSon:
                                        erasedElementParent->leftSon;
    }

    if (erasedElement == nullptr || erasedElement->key != key)
    {
        return;
    }

    auto currentElement = prepareSubstituteVertex(erasedElement);

    if (erasedElementParent == nullptr)
	{
    	tree->top = currentElement;
	}
	else if (erasedElement->value > erasedElementParent->value)
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
