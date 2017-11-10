namespace listStuff
{
    struct ListVertex
    {
        ListVertex *next;
        int value;
    };

    struct CyclicList
    {
        ListVertex *top = nullptr;
        int size = 0;
    };

    void insertToCyclicList(CyclicList &list, int value);
    void deleteFromCyclicList(CyclicList &list, ListVertex *position);
    void clearCyclicList(CyclicList &list);
}
