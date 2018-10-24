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
                float result = CalculateResult(validationResult);
                DisplayAlert("Result: " + result, "Valid", "Close");
            }
            else 
            {
                // Dispaly error
                DisplayAlert("Result", "Invalid Expression", "Close");
            }
        }

        private float CalculateResult(ExpressionValidationResult validationResult)
        {
            float[] numbers = validationResult.numbers;
            List<char> operators = validationResult.operators;

            float num1 = numbers[0];
            float num2 = numbers[1];
            float num3;

            char operator1 = operators.ElementAt(0);
            char operator2;

            float result = 0;
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

        private float CalculateExpression(float num1, float num2, char op)
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
                if (!float.TryParse(number, out float parsedResult))
                {
                    isValidNumbers = false;
                    break;
                }
            }

            return isValidNumbers && numbers.Length > 1;
        }

        private float[] ConvertNumbersToFloat(string[] numbers)
        {
            float[] convertedNumbers = new float[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                float parsedResult;
                string number = numbers[i];
                if (float.TryParse(number, out parsedResult))
                {
                    convertedNumbers[i] = parsedResult;
                }
            }

            return convertedNumbers;
        }

        struct ExpressionValidationResult {
            public Boolean isValid;
            public float[] numbers;
            public List<char> operators;

            public ExpressionValidationResult(Boolean isValid, float[] numbers, List<char> operators) 
            {
                this.isValid = isValid;
                this.numbers = numbers;
                this.operators = operators;
            }
        }

    }

}
