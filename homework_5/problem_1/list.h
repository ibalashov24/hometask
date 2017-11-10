namespace listStuff
{
    struct ListVertex;
    struct SortedList
    {
        ListVertex *top = nullptr;
        int size = 0;
    };

    // Inserts element into sorted `list`
    void insert(SortedList &list, int value);
    // Deletes element from sorted `list`
    void deleteList(SortedList &list, int value);
    // Prints content of `list` to `cout`
    void printList(const SortedList &list);

    // Clears all memory allocated to list
    void clearList(SortedList &list);
}
