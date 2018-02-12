namespace listStuff
{
	// Some service list structures
    struct ListVertex;
    struct CyclicList;

    // Constructs empty list
    CyclicList *makeCyclicList();

    // Fully deletes the list
    void clearCyclicList(CyclicList *list);

    // Inserts new value to the list
    void insertToCyclicList(CyclicList *list, int value);

    // Deletes element with given position from the list
    void deleteFromCyclicList(CyclicList *list, ListVertex *position);

    // Returns last element from the list
    ListVertex *getListTop(CyclicList *list);

    // Returns count of elements in the list
    int getListSize(const CyclicList *list);

    // Returns next element after `vertex` in same list (or nullptr if last)
    ListVertex *getNextVertex(ListVertex *vertex);

    // Returns value contained in `vertex`
    int getVertexValue(const ListVertex *vertex);
}
