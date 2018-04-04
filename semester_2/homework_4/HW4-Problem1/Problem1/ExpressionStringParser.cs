using System;

namespace Tree
{
    /// <summary>
    /// Parses expression string into the tree
    /// </summary>
    public class ExpressionStringParser
    {
        /// <summary>
        /// Information about the tree in string format
        /// </summary>
        private readonly string expressionString;

        /// <summary>
        /// Initializes new instance of ExpressionStringParser
        /// </summary>
        /// <param name="stringToParse"></param>
        public ExpressionStringParser(string stringToParse)
        {
            this.expressionString = stringToParse;
            this.InitializeRoot();
        }

        /// <summary>
        /// Returns root of the tree
        /// </summary>
        public Vertex.ExpressionTreeVertex Root
        {
            get;
            private set;
        }

        /// <summary>
        /// Detects the root of the tree
        /// </summary>
        private void InitializeRoot()
        {
            if (this.Root == null)
            {
                int nextPosition = 0;
                (this.Root, nextPosition) = this.Parse();

                if (nextPosition < this.expressionString.Length)
                {
                    throw new Exception.InvalidExpressionException(
                        "There is some trash at the end of expression string");
                }
            }
        }

        /// <summary>
        /// Performs parsing
        /// </summary>
        /// <param name="currentExpressionStringPosition">Position of parse process beginning</param>
        /// <returns>Root of the resulting tree</returns>
        private (Vertex.ExpressionTreeVertex root, int nextSymbolToRead)
            Parse(int currentExpressionStringPosition = 0)
        {
            Vertex.ExpressionTreeVertex currentVertex;
            int nextPosition;

            if (this.expressionString[currentExpressionStringPosition] == '(')
            {
                Vertex.ExpressionTreeVertex leftSon;
                Vertex.ExpressionTreeVertex rightSon;

                // Distance between opening bracket and the first operand
                const int firstOperandShift = 3;
                (leftSon, nextPosition) = this.Parse(currentExpressionStringPosition +
                    firstOperandShift);
                (rightSon, nextPosition) = this.Parse(nextPosition);

                switch (this.expressionString[currentExpressionStringPosition + 1])
                {
                    case '+':
                        currentVertex = new Vertex.OperationPlus(leftSon, rightSon);
                        break;
                    case '-':
                        currentVertex = new Vertex.OperationMinus(leftSon, rightSon);
                        break;
                    case '*':
                        currentVertex = new Vertex.OperationMultiply(leftSon, rightSon);
                        break;
                    case '/':
                        currentVertex = new Vertex.OperationDivide(leftSon, rightSon);
                        break;
                    default:
                        throw new Exception.InvalidExpressionException(
                            "Invalid input string! Operation expected",
                            currentExpressionStringPosition + 1);
                }
            }
            else
            {
                var numberLength = this.expressionString.IndexOfAny(" )".ToCharArray(), currentExpressionStringPosition) -
                    currentExpressionStringPosition;
                if (numberLength < 0)
                {
                    throw new Exception.InvalidExpressionException(
                        "Invalid input string! Operation's end not found",
                        currentExpressionStringPosition);
                }

                var isCorrectOperand = int.TryParse(
                    this.expressionString.Substring(currentExpressionStringPosition, numberLength),
                    out int result);

                if (!isCorrectOperand)
                {
                    throw new Exception.InvalidExpressionException(
                        "Invalid input string! Invalid operand!",
                        currentExpressionStringPosition);
                }

                currentVertex = new Vertex.RealOperand(
                    int.Parse(this.expressionString.Substring(currentExpressionStringPosition, numberLength)));

                nextPosition = currentExpressionStringPosition + numberLength - 1;
            }

            ++nextPosition;
            while (nextPosition < this.expressionString.Length &&
                this.expressionString[nextPosition] == ' ')
            {
                ++nextPosition;
            }

            return (currentVertex, nextPosition);
        }
    }
}