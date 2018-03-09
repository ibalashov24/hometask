namespace Tree
{
    using System;

    /// <summary>
    /// Implements the parse tree
    /// </summary>
    public class ExpressionTree : IExpressionTree
    {
        /// <summary>
        /// Reference to the root of the tree
        /// </summary>
        private Vertex.ExpressionTreeVertex root;

        /// <summary>
        /// Service variable containing value of the tree
        /// </summary>
        private double? calculatedValue;

        /// <summary>
        /// Initializes new instance of ExpressionTree
        /// </summary>
        /// <param name="expression">Expression to parse</param>
        public ExpressionTree(string expression)
        {
            var parser = new ExpressionStringParser(expression);
            this.root = parser.Root;
            this.EvaluateTree();
        }

        /// <summary>
        /// Returns result of the expression tree calculation
        /// </summary>
        public double Value
        {
            get
            {
                return (double)this.calculatedValue;
            }
        }

        /// <summary>
        /// Returns string representation in tree form
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return this.root.ToString();
        }

        /// <summary>
        /// Returns infix (1 + 2 * 3) representation of the tree
        /// </summary>
        /// <returns>Infix representaion</returns>
        public string GetInfixRepresentation()
        {
            return this.root.GetInfixRepresentation();
        }

        /// <summary>
        /// Calculates value of the expression parse tree
        /// </summary>
        private void EvaluateTree()
        {
            this.calculatedValue = this.root.Handle();
        }

        /// <summary>
        /// Parses expression string into the tree
        /// </summary>
        private class ExpressionStringParser
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
}