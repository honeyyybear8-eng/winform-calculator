using System;
using System.Windows.Forms;

namespace WinFormCalculator
{
    public partial class CalculatorForm : Form
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private string operation = "";
        private bool isNewNumber = true;

        public CalculatorForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Calculator";
            this.Width = 400;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label displayLabel = new Label();
            displayLabel.Name = "displayLabel";
            displayLabel.Text = "0";
            displayLabel.Font = new System.Drawing.Font("Arial", 24);
            displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            displayLabel.Dock = DockStyle.Top;
            displayLabel.Height = 60;
            displayLabel.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(displayLabel);

            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Fill;
            this.Controls.Add(buttonPanel);

            string[] buttonTexts = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", ".", "=", "+" };
            int row = 0, col = 0;
            foreach (string text in buttonTexts)
            {
                Button btn = new Button();
                btn.Text = text;
                btn.Font = new System.Drawing.Font("Arial", 18);
                btn.Width = 80;
                btn.Height = 60;
                btn.Left = col * 90 + 10;
                btn.Top = row * 70 + 10;
                btn.Click += Button_Click;
                buttonPanel.Controls.Add(btn);
                col++;
                if (col > 3)
                {
                    col = 0;
                    row++;
                }
            }

            Button clearBtn = new Button();
            clearBtn.Text = "C";
            clearBtn.Font = new System.Drawing.Font("Arial", 18);
            clearBtn.Width = 80;
            clearBtn.Height = 60;
            clearBtn.Left = 10;
            clearBtn.Top = 290;
            clearBtn.Click += (s, e) => Clear();
            buttonPanel.Controls.Add(clearBtn);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string text = btn.Text;
            Label displayLabel = this.Controls["displayLabel"] as Label;
            if (char.IsDigit(text[0]))
            {
                if (isNewNumber)
                {
                    displayLabel.Text = text;
                    isNewNumber = false;
                }
                else
                {
                    displayLabel.Text += text;
                }
            }
            else if (text == ".")
            {
                if (!displayLabel.Text.Contains(".")) displayLabel.Text += text;
            }
            else if (text == "=")
            {
                Calculate(displayLabel);
            }
            else
            {
                if (operation != "")
                {
                    Calculate(displayLabel);
                }
                firstNumber = double.Parse(displayLabel.Text);
                operation = text;
                isNewNumber = true;
            }
        }

        private void Calculate(Label displayLabel)
        {
            secondNumber = double.Parse(displayLabel.Text);
            double result = 0;
            switch (operation)
            {
                case "+": result = firstNumber + secondNumber; break;
                case "-": result = firstNumber - secondNumber; break;
                case "*": result = firstNumber * secondNumber; break;
                case "/": if (secondNumber != 0) result = firstNumber / secondNumber; else MessageBox.Show("Cannot divide by zero"); break;
            }
            displayLabel.Text = result.ToString();
            isNewNumber = true;
            operation = "";
        }

        private void Clear()
        {
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
            isNewNumber = true;
            Label displayLabel = this.Controls["displayLabel"] as Label;
            displayLabel.Text = "0";
        }
    }
}