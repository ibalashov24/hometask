namespace treeStuff
{
    struct Tree;
    struct TreeVertex;

    Tree *createTree(TreeVertex *top);

    // Creates subtree with operation
    TreeVertex *constructVertex(TreeVertex *left, TreeVertex *right,
                                char operation);
    // Creates leaf (sons == nullptr)
    TreeVertex *constructVertex(double value);

    double calculateExpression(const Tree *operationTree,
                          bool needRevalidateCache = false);
    void printTree(const Tree *operationTree);

    void deleteTree(Tree *tree);
}
