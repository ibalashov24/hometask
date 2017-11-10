namespace listStuff
{
    struct ListVertex;
    struct SortedList;

    // Constructs empty list
    SortedList *makeList();

    // Inserts element into sorted `list`
    void insert(SortedList *list, int value);
    // Deletes element from sorted `list`
    void deleteListElement(SortedList *list, int value);
    // Prints content of `list` to `cout`
    void printList(SortedList const *list);

    // Clears all memory allocated to list
    void deleteList(SortedList *list);
}
