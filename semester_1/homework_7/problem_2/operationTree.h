namespace treeStuff
{
	// Binary tree service structures
    struct Tree;
    struct TreeVertex;

    // Creates new binary tree
    Tree *createTree(TreeVertex *top);

    // Creates subtree with operation
    TreeVertex *constructVertex(TreeVertex *left, TreeVertex *right,
                                char operation);
    // Creates leaf (sons == nullptr)
    TreeVertex *constructVertex(double value);

    // Calculates expression by given operation tree
    double calculateExpression(const Tree *operationTree);

    // Prints tree in form (<operator> <operand1> <operand2>)
    void printTree(const Tree *operationTree);

    // Fully deletes tree
    void deleteTree(Tree *tree);
}
