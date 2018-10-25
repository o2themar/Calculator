using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private static readonly char[] STANDARD_OPERATORS = { '+', '-', '*', '/', '%' };

        public MainPage()
        {
            InitializeComponent();
        }

		void OnSelectButton(object sender, System.EventArgs e)
		{
			Button button = (Button) sender;
			string input = button.Text;

            if(display.Text.Equals("0")) {
                display.Text = "";
            }

            display.Text += input;
		}

        void OnDelete(object sender, System.EventArgs e)
        {
            display.Text = display.Text.Remove(display.Text.Length - 1);
        }

        void OnClear(object sender, System.EventArgs e)
        {
            display.Text = "0";
        }

        void OnEqual(object sender, System.EventArgs e)
        {
            ExpressionValidationResult validationResult = ValidateExpression(display.Text);
            // If valid expression calculate
            if (validationResult.isValid)
            {
                Decimal result = CalculateResult(validationResult);
                DisplayAlert("Result: " + result, "Valid", "Close");
            }
            else 
            {
                // Dispaly error
                DisplayAlert("Result", "Invalid Expression", "Close");
            }
        }

        private Decimal CalculateResult(ExpressionValidationResult validationResult)
        {
            Decimal[] numbers = validationResult.numbers;
            List<char> operators = validationResult.operators;

            Decimal num1 = numbers[0];
            Decimal num2 = numbers[1];
            Decimal num3;

            char operator1 = operators.ElementAt(0);
            char operator2;

            Decimal result = 0;
            if (numbers.Length > 2) { 
                for (int i = 2; i < numbers.Length; i++)
                {
                    num3 = numbers[i];
                    operator2 = operators.ElementAt(i - 1);

                    if(operator2.Equals('*') ||
                                operator2.Equals('/') ||
                                operator2.Equals('%'))
                    {
                        num2 = CalculateExpression(num2, num3, operator2);
                    }
                    else
                    {
                        num1 = CalculateExpression(num1, num2, operator1);
                        num2 = num3;
                        operator1 = operator2;
                    }
                }
                result = CalculateExpression(num1, num2, operator1);
            } else 
            {
                result = CalculateExpression(num1, num2, operator1);
            }

            return result;
        }

        private Decimal CalculateExpression(Decimal num1, Decimal num2, char op)
        {
            switch(op) 
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    return num1 / num2;
                case '%':
                    return num1 % num2;
                default:
                    return 0;
            }
        }

        private ExpressionValidationResult ValidateExpression(String expression)
        {
            string[] numbers = expression.Split(STANDARD_OPERATORS);
            numbers = numbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            int operatorCount = 0;
            List<char> operators = new List<char>();

            foreach (char c in expression)
            {
                if (STANDARD_OPERATORS.Contains(c))
                {
                    operatorCount++;
                    operators.Add(c);
                }
            }

            Boolean isValidExpression = IsValidOperators(numbers, operatorCount) && IsValidNumbers(numbers);
            ExpressionValidationResult result = new 
                ExpressionValidationResult(isValidExpression, ConvertNumbersToFloat(numbers), operators);

            return result;
        }

        private static bool IsValidOperators(string[] numbers, int operatorCount)
        {
            return (operatorCount < numbers.Length);
        }

        private static bool IsValidNumbers(string[] numbers)
        {
            Boolean isValidNumbers = true;

            for (int i = 0; i < numbers.Length; i++)
            {
                string number = numbers[i];
                if (!Decimal.TryParse(number, out Decimal parsedResult))
                {
                    isValidNumbers = false;
                    break;
                }
            }

            return isValidNumbers && numbers.Length > 1;
        }

        private Decimal[] ConvertNumbersToFloat(string[] numbers)
        {
            Decimal[] convertedNumbers = new Decimal[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                Decimal parsedResult;
                string number = numbers[i];
                if (Decimal.TryParse(number, out parsedResult))
                {
                    convertedNumbers[i] = parsedResult;
                }
            }

            return convertedNumbers;
        }

        struct ExpressionValidationResult {
            public Boolean isValid;
            public Decimal[] numbers;
            public List<char> operators;

            public ExpressionValidationResult(Boolean isValid, Decimal[] numbers, List<char> operators) 
            {
                this.isValid = isValid;
                this.numbers = numbers;
                this.operators = operators;
            }
        }

    }

}
