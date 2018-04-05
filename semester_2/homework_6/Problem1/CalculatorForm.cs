namespace Problem1
{
    using System;
    using System.Windows.Forms;

    public partial class CalculatorForm : Form
    {
        /// <summary>
        /// Calculator's logic
        /// </summary>
        private Calculator.ICalculator calculator;

        /// <summary>
        /// Shows if CurrentOperandBox contains temporary placeholder 
        /// with the value of the expression
        /// </summary>
        private bool isResultPrintedInsteadOfOperand = false;

        /// <summary>
        /// Default sign of comma 
        /// (can be different in different locales?)
        /// </summary>
        private const char commaSign = ',';

        /// <summary>
        /// True if the last symbol of the last operand must == ','
        /// </summary>
        private bool needPrintCommaWhenNextNumberEntered = false;   

        /// <summary>
        /// Initializes new instance of Form1
        /// </summary>
        /// <param name="calculator">Calculator's logic</param>
        public CalculatorForm(Calculator.ICalculator calculator)
        {
            this.calculator = calculator;

            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += this.SomeKeyPressed;

            this.PlaceLastOperandToOperandBox();
            this.PlaceExpressionToExpressionBox();
        }

        /// <summary>
        /// Handles Enter key press
        /// </summary>
        /// <seealso cref="Control.ProcessCmdKey(ref Message, Keys)s"/>
        protected override bool ProcessCmdKey(
            ref Message message, 
            Keys key)
        {
            const int WM_KEYDOWN = 0x100;

            if (message.Msg == WM_KEYDOWN 
                && key == Keys.Enter)
            {
                this.EqualityButton.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref message, key);
        }

        /// <summary>
        /// Handles pressed key
        /// </summary>
        public void SomeKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == 0 &&
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 ||
                e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                var numericValue = Math.Min(
                    Math.Abs(e.KeyCode - Keys.D0), 
                    Math.Abs(e.KeyCode - Keys.NumPad0));

                this.NumberButtonClicked(
                        new Button()
                        {
                            Text = numericValue.ToString()
                        }, e);

                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Back:
                    this.BackspaceButton.PerformClick();
                    break;
                case Keys.Multiply:
                    this.MultiplicationButton.PerformClick();
                    break;
                case Keys.Divide:
                    this.DivisionButton.PerformClick();
                    break;
                case Keys.Subtract:
                    this.MinusButton.PerformClick();
                    break;
                case Keys.Add:
                    this.PlusButton.PerformClick();
                    break;
                case Keys.Escape:
                    this.CButton.PerformClick();
                    break;
                case Keys.Delete:
                    this.CEButton.PerformClick();
                    break;
                case Keys.R:
                    this.FractionButton.PerformClick();
                    break;
                case Keys.Q:
                    this.SqrButton.PerformClick();
                    break;
                case Keys.Enter:
                    this.EqualityButton.PerformClick();
                    break;
                case Keys.Decimal:
                    this.DecimalDotButton.PerformClick();
                    break;
                case Keys.F9:
                    this.NegateButton.PerformClick();
                    break;
                case Keys.D2 when (e.Shift):
                    this.SqrtButton.PerformClick();
                    break;
            } 
        }

        /// <summary>
        /// Handles situation when one of 0-9 is pressed
        /// </summary>
        public void NumberButtonClicked(object sender, EventArgs args)
        {
            // In order to prevent overflow
            const int maxSymbolsInNumber = 12;

            var senderButton = sender as System.Windows.Forms.Button;
            var pressedNumber = senderButton.Text;

            var newOperandValueString = this.calculator.LastOperand.ToString();
            if (newOperandValueString.Length >= maxSymbolsInNumber)
            {
                return;
            }

            if (this.needPrintCommaWhenNextNumberEntered)
            {
                newOperandValueString += commaSign;
                this.needPrintCommaWhenNextNumberEntered = false;
            }
            newOperandValueString += pressedNumber;

            var newOperandValue = double.Parse(newOperandValueString);

            if (this.IsAnyPlaceholderSet())
            {
                this.CurrentOperandBox.Text = string.Empty;
                this.isResultPrintedInsteadOfOperand = false;
            }

            this.calculator.SetLastOperandValue(newOperandValue);
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when comma button is pressed
        /// </summary>
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

        /// <summary>
        /// Handles situation when one of +-*/ was clicked
        /// </summary>
        public void OperationButtonClicked(object sender, EventArgs args)
        {
            var senderButton = sender as System.Windows.Forms.Button;

            if (!IsAnyPlaceholderSet())
            {
                this.ProceedStoredCalculations();
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

            // Adding placeholder
            this.CurrentOperandBox.Text = 
                this.calculator.ExpressionValue.ToString();
            this.isResultPrintedInsteadOfOperand = true;
        }

        /// <summary>
        /// Handles situation when backspace button was pressed
        /// </summary>
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
             
            var lastOperandString = this.calculator.LastOperand.ToString();
            lastOperandString = lastOperandString.Remove(
                lastOperandString.Length - 1);
            
            if (lastOperandString.Length == 0)
            {
                this.calculator.SetLastOperandValue(0);
                this.PlaceLastOperandToOperandBox();
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

        /// <summary>
        /// Handles situation when "=" button was pressed
        /// </summary>
        public void EqualityButtonClicked(object sender, EventArgs args)
        {
            this.ProceedStoredCalculations();
            this.calculator.ReinitializeCalculatorWithResult();
            this.PlaceLastOperandToOperandBox();
            this.PlaceExpressionToExpressionBox();
        }

        /// <summary>
        /// Handles situation when CE button was pressed
        /// </summary>
        public void CEButtonClicked(object sender, EventArgs args)
        {
            this.calculator.SetLastOperandValue(0);
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when C button was pressed
        /// </summary>
        public void CButtonClicked(object sender, EventArgs args)
        {
            this.calculator.ClearMemory();
            this.PlaceLastOperandToOperandBox();
            this.PlaceExpressionToExpressionBox();
        }

        /// <summary>
        /// Handles situation when "+-" button was pressed
        /// </summary>
        public void NegateButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.NegateCurrentOperand();
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when "1/x" button was pressed
        /// </summary>
        public void FractionButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.PutOperandIntoDenominator();
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when square root button was pressed
        /// </summary>
        public void SqrtButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.ApplySqrtToOperand();
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when x^2 button was pressed
        /// </summary>
        public void SqrButtonClicked(object sender, EventArgs args)
        {
            this.SetPlaceholderWithResultAsOperand();
            this.calculator.ApplySqrToOperand();
            this.PlaceLastOperandToOperandBox();
        }

        /// <summary>
        /// Handles situation when % button was pressed
        /// </summary>
        public void PercentButtonClicked(object sender, EventArgs args)
        {
            MessageBox.Show(
                "This function is not implemented yet. Sorry.", 
                "Message box test", 
                MessageBoxButtons.OK);
        }

        /// <summary>
        /// Checks if any placeholder is installed
        /// </summary>
        /// <returns>True if installed</returns>
        private bool IsAnyPlaceholderSet()
        {
            return this.isResultPrintedInsteadOfOperand;
        }

        /// <summary>
        /// Updates data in CurrentOperandBox
        /// </summary>
        private void PlaceLastOperandToOperandBox()
        {
            this.CurrentOperandBox.Text =
                this.calculator.LastOperand.ToString();
        }

        /// <summary>
        /// Updates data in ExpressionBox with given string
        /// </summary>
        /// <param name="value">String to place</param>
        private void PlaceValueToExpressionBox(string value)
        {
            this.ExpressionBox.Text = "\r\n" + value;
        }

        /// <summary>
        /// Updates data in ExpressionBox with 
        /// expression from calculator
        /// </summary>
        private void PlaceExpressionToExpressionBox()
        {
            this.PlaceValueToExpressionBox(this.calculator.Expression);
        }

        /// <summary>
        /// Expression value --> Box with opeand
        /// </summary>
        private void SetPlaceholderWithResultAsOperand()
        {
            if (this.isResultPrintedInsteadOfOperand)
            {
                this.calculator.SetLastOperandValue(
                    this.calculator.ExpressionValue);
            }
        }

        private void ProceedStoredCalculations()
        {
            try
            {
                this.calculator.FlushOperand();
            }
            catch (OverflowException)
            {
                this.calculator.ClearMemory();
                this.PlaceValueToExpressionBox(
                    "Very big number");
                this.isResultPrintedInsteadOfOperand = true;
            }
        }
    }
}
