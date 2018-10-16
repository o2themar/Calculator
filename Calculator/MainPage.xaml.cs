using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void AppendText(String text)
        {
            if (display.Text.Equals("0")) {
                display.Text = text;
            } else {
                display.Text += text;
            }
        }

        void Clear(object sender, System.EventArgs e)
        {
            display.Text = "0";
        }

        void Negation(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Modulus(object sender, System.EventArgs e)
        {
            AppendText("%");
        }

        void Divide(object sender, System.EventArgs e)
        {
            AppendText("÷");
        }

        void Seven(object sender, System.EventArgs e)
        {
            AppendText("7");
        }

        void Eight(object sender, System.EventArgs e)
        {
            AppendText("8");
        }

        void Nine(object sender, System.EventArgs e)
        {
            AppendText("9");
        }

        void Multiplication(object sender, System.EventArgs e)
        {
            AppendText("x");
        }

        void Four(object sender, System.EventArgs e)
        {
            AppendText("4");
        }

        void Five(object sender, System.EventArgs e)
        {
            AppendText("5");
        }

        void Six(object sender, System.EventArgs e)
        {
            AppendText("6");
        }

        void Subtraction(object sender, System.EventArgs e)
        {
            AppendText("-");
        }

        void One(object sender, System.EventArgs e)
        {
            AppendText("1");
        }

        void Two(object sender, System.EventArgs e)
        {
            AppendText("2");
        }

        void Three(object sender, System.EventArgs e)
        {
            AppendText("3");
        }

        void Addition(object sender, System.EventArgs e)
        {
            AppendText("+");
        }

        void Zero(object sender, System.EventArgs e)
        {
            AppendText("0");
        }

        void Period(object sender, System.EventArgs e)
        {
            AppendText(".");
        }

        void Equal(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
