namespace Problem1
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private Calculator.ICalculator calculator;

        private readonly string defaultPlaceholderValue = "0";
        private bool isZeroPlaceholderSet = true;
        private bool isDecorativePlaceholderSet = false;

        private const char commaSign = ',';
        private bool needPrintCommaWhenNextNumberEntered = false;   

        public Form1(Calculator.ICalculator calculator)
        {
            this.calculator = calculator;

            InitializeComponent();

            this.PrintZeroPlaceholder();
        }

        /// <summary>
        /// Handles situation when one of 0-9 is pressed
        /// </summary>
        public void NumberButtonClicked(object sender, EventArgs args)
        {
            var senderButton = sender as System.Windows.Forms.Button;
            var pressedNumber = senderButton.Text;

            var newOperandValueString = this.calculator.LastOperand.ToString();
            if (this.needPrintCommaWhenNextNumberEntered)
            {
                newOperandValueString += commaSign;
                this.needPrintCommaWhenNextNumberEntered = false;
            }
            newOperandValueString += pressedNumber;

            var newOperandValue = double.Parse(newOperandValueString);

            if (this.IsAnyPlaceholderSet())
            {
                this.CurrentOperandBox.Text = "";
                this.isZeroPlaceholderSet = false;
                this.isDecorativePlaceholderSet = false;
            }

            this.calculator.SetLastOperandValue(newOperandValue);
            this.PlaceLastOperandToOperandBox();
        }

        public void CommaButtonClicked(object sender, EventArgs args)
        {
            var lastOperandString = this.calculator.LastOperand.ToString();

            if (lastOperandString.IndexOf(commaSign) == -1
                && !this.needPrintCommaWhenNextNumberEntered)
            {
                this.needPrintCommaWhenNextNumberEntered = true;
                this.CurrentOperandBox.Text += commaSign;
            }
        }

        public void OperationButtonClicked(object sender, EventArgs args)
        {
            var senderButton = sender as System.Windows.Forms.Button;

            if (this.isZeroPlaceholderSet)
            {
                this.calculator.SetLastOperandValue(
                    double.Parse(this.defaultPlaceholderValue));
                this.isZeroPlaceholderSet = false;
            }

            if (!IsAnyPlaceholderSet())
            {
                this.calculator.FlushOperand();
            }

            switch (senderButton.Name)
            {
                case "DivisionButton":
                    this.calculator.SetNextOperator(
                        Calculator.OperatorType.Division);
                    break;
                case "MultiplicationButton":
                    this.calculator.SetNextOperator(
                        Calculator.OperatorType.Multiplication);
                    break;
                case "PlusButton":
                    this.calculator.SetNextOperator(
                        Calculator.OperatorType.Plus);
                    break;
                case "MinusButton":
                    this.calculator.SetNextOperator(
                        Calculator.OperatorType.Minus);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"Invalid operation {senderButton.Name}");
            }

            this.PlaceExpressionToExpressionBox();

            this.CurrentOperandBox.Text = 
                this.calculator.ExpressionValue.ToString();
            this.isDecorativePlaceholderSet = true;
        }

        public void BackspaceButtonClicked(object sender, EventArgs args)
        {
            var senderButton = sender as System.Windows.Forms.Button;

            if (this.IsAnyPlaceholderSet())
            {
                return;
            }

            if (this.needPrintCommaWhenNextNumberEntered)
            {
                this.PlaceLastOperandToOperandBox();
                this.needPrintCommaWhenNextNumberEntered = false;
                return;
            }
             
            var lastOperandString = 
                this.calculator.LastOperand.ToString();
            lastOperandString = lastOperandString.Remove(
                lastOperandString.Length - 1);
            
            if (lastOperandString.Length == 0)
            {
                this.PrintZeroPlaceholder();
                this.calculator.SetLastOperandValue(0);
            }
            else
            {
                this.CurrentOperandBox.Text = lastOperandString;
                this.calculator.SetLastOperandValue(
                    double.Parse(lastOperandString));

                if (lastOperandString[lastOperandString.Length - 1]
                    == commaSign)
                {
                    this.needPrintCommaWhenNextNumberEntered = true;
                }
            }
        }

        public void EqualityButtonClicked(object sender, EventArgs args)
        {
            this.calculator.FlushOperand();
            this.calculator.ReinitializeCalculatorWithResult();
            this.PlaceLastOperandToOperandBox();
            this.PlaceExpressionToExpressionBox();
        }

        public void CEButtonClicked(object sender, EventArgs args)
        {
            this.calculator.SetLastOperandValue(0);
            this.PrintZeroPlaceholder();
        }

        public void CButtonClicked(object sender, EventArgs args)
        {
            this.calculator.ClearMemory();
            this.PrintZeroPlaceholder();
            this.PlaceExpressionToExpressionBox();
        }

        public void NegateButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.NegateCurrentOperand();
            this.PlaceLastOperandToOperandBox();
        }

        public void FractionButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.PutOperandIntoDenominator();
            this.PlaceLastOperandToOperandBox();
        }

        public void SqrtButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.ApplySqrtToOperand();
            this.PlaceLastOperandToOperandBox();
        }

        public void SqrButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.ApplySqrToOperand();
            this.PlaceLastOperandToOperandBox();
        }

        private void PrintZeroPlaceholder()
        {
            this.CurrentOperandBox.Text = this.defaultPlaceholderValue;
            this.isZeroPlaceholderSet = true;
        }

        private bool IsAnyPlaceholderSet()
        {
            return this.isZeroPlaceholderSet || this.isDecorativePlaceholderSet;
        }

        private void PlaceLastOperandToOperandBox()
        {
            this.CurrentOperandBox.Text =
                this.calculator.LastOperand.ToString();
        }

        private void PlaceExpressionToExpressionBox()
        {
            this.ExpressionBox.Text = "\r\n" + this.calculator.Expression;
        }

        private void SetPlaceholderWithResultAsOperand()
        {
            if (this.IsAnyPlaceholderSet())
            {
                this.calculator.SetLastOperandValue(
                    this.calculator.ExpressionValue);
            }
        }
    }
}
