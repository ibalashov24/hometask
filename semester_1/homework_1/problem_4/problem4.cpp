#include <iostream>

using namespace std;

const int maxTicket = 999999;

/**
    Summarizes the digits (the numbering of digits from the younger one)
**/
int sumRanks(int number, int begin, int end)
{
    int powerBegin = 1;
    int powerEnd = 1;
    for (int i = 0; i < end - 1; i++)
    {
        powerBegin *= 10;
        if (i < begin - 1)
            powerEnd *= 10;
    }

    int sum = 0;
    for (int powerTen = powerBegin; powerTen >= powerEnd; powerTen /= 10)
        sum += (number / powerTen) % 10;

    return sum;
}

int main()
{
    int luckyTicketCount = 0;
    for (int ticket = 0; ticket <= maxTicket; ticket++)
    {
        if (sumRanks(ticket, 4, 6) == sumRanks(ticket, 1, 3))
            luckyTicketCount++;
    }

    cout << "Number of lucky numbers: " << luckyTicketCount << endl;
}
