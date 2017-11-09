namespace listStuff
{
    struct ListVertex;
    struct SortedList
    {
        ListVertex *top = nullptr;
        int size = 0;
    };

    void insert(SortedList &list, int value);
    void deleteList(SortedList &list, int value);
    void printList(const SortedList &list);

    void clearList(SortedList &list);
}
