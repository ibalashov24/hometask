namespace listStuff
{
    struct ListVertex
    {
        ListVertex *next;
        int value;
    };
    struct CyclicList;

    CyclicList *makeCyclicList();
    void clearCyclicList(CyclicList *list);

    void insertToCyclicList(CyclicList *list, int value);
    void deleteFromCyclicList(CyclicList *list, ListVertex *position);

    ListVertex *getListTop(CyclicList *list);
    int getListSize(const CyclicList *list);

    ListVertex *getNextVertex(ListVertex *vertex);
    int getVertexValue(const ListVertex *vertex);
}
