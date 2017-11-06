#include <iostream>
#include <algorithm>
#include <cmath>
#include <vector>
#include <string>

using namespace std;

const int ADDITIONAL_CODE_BASE = sizeof (int) * 8;

vector<bool> reverseCopy(vector<bool>::const_iterator begin,
                         vector<bool>::const_iterator end)
{
    auto temp = vector<bool>(begin, end);
    auto left = temp.begin();
    auto right = temp.end() - 1;
    while (left < right)
    {
        iter_swap(left++, right--);
    }
    return temp;
}

vector<bool> reverseCopy(const std::vector<bool> &outString)
{
    return reverseCopy(outString.begin(), outString.end());
}

vector<bool> decToBin(int number)
{
    vector<bool> result(ADDITIONAL_CODE_BASE);

    for (int i = 0; i < ADDITIONAL_CODE_BASE; ++i)
    {
        result[i] = (number >> i) & 1;
    }

    return result;
}

int binToDec(const vector<bool> &binaryNumber)
{
    int result = 0;
    for (int i = ADDITIONAL_CODE_BASE - 1; i >= 0; --i)
    {
        result = (result << 1) | (binaryNumber[i]);
    }

    return result;
}

vector<bool> sumBinaryNumbers(const vector<bool> &a, const vector<bool> &b)
{
    vector<bool> tempNumber(ADDITIONAL_CODE_BASE, false);

    bool carry = false;
    for (int i = 0; i < ADDITIONAL_CODE_BASE; i++)
    {
        short currentDigit = a[i] + b[i] + carry;

        tempNumber[i] = currentDigit % 2;
        carry = currentDigit / 2;
    }

    return tempNumber;
}

string toString(const vector<bool> &input)
{
    string result = "";
    for (auto e : input)
    {
        result.push_back(e + '0');
    }

    return result;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    int firstNumber = 0;
    int secondNumber = 0;
    cout << "Введите два числа: ";
    cin >> firstNumber >> secondNumber;

    auto firstNumberBin = decToBin(firstNumber);
    auto secondNumberBin = decToBin(secondNumber);

    cout << "Двоичное представление этих чисел в дополнительном коде: " << endl;
    cout << toString(reverseCopy(decToBin(firstNumber))) << ' ';
    cout << toString(decToBin(secondNumber)) << endl;

    bool isNeedReverse = false;
    if (firstNumber < 0 && secondNumber < 0)
    {
        firstNumberBin = decToBin(-firstNumber);
        secondNumberBin = decToBin(-secondNumber);

        isNeedReverse = true;
    }

    vector<bool> sumBinary;
    sumBinary = sumBinaryNumbers(firstNumberBin, secondNumberBin);

    int sumDecimal = binToDec(sumBinary);
    if (isNeedReverse)
    {
        sumDecimal = -sumDecimal;
        sumBinary = decToBin(sumDecimal);
    }

    cout << "Сумма в двоичном формате: ";
    cout << toString(reverseCopy(sumBinary.begin(), sumBinary.end())) << endl;

    cout << "Сумма в десятичном формате: ";
    cout << sumDecimal << endl;

    return 0;
}
