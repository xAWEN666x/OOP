using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Praktika_OOP_1
{
    public partial class Zadanie2 : Form
    {
        decimal guess;
        decimal delta = Convert.ToDecimal(Math.Pow(10, -28));
        decimal? exact = null;
        public Zadanie2()
        {
            InitializeComponent();
        }

        private void OnlyValidDecimal(in TextBox textbox, ref KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSlash
            //MessageBox.Show($"{textBox1.Text.Contains('1')}");
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                if ((pressed != '.') && (pressed != ','))
                {
                    e.Handled = true;
                }
                if (textbox.Text.Contains('.') || textbox.Text.Contains(',')) e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidDecimal(in textBox1, ref e);
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            double parsed_value;
            string error_message;
            if (!parse_double_value(in entered_value, out parsed_value, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            label1.Text = $"{Math.Sqrt(parsed_value)}";
        }

        private bool parse_double_value(in string to_parse, out double parsed, out string error_message)
        {
            error_message = "";
            if (!double.TryParse("0" + to_parse.Replace('.', ','), out parsed))
            {
                error_message = "Пожалуйста, введите дробное число";
                return false;
            }
            if (parsed <= 0.0)
            {
                error_message = "Пожалуйста, введите положительное число, не равное нулю";
                return false;
            }
            return true;
        }

        private bool parse_decimal_value(in string to_parse, out decimal parsed, out string error_message)
        {
            double parsed_double;
            parsed = 0;
            if (!parse_double_value(in to_parse, out parsed_double, out error_message))
            {
                return false;
            }
            try
            {
                parsed = (decimal)parsed_double;
            }
            catch (System.OverflowException)
            {
                error_message = "Слишком большое число";
                return false;
            }
            return true;
        }

        private decimal newton_sqrt(decimal number, decimal initial, out int iterarions, ref decimal guess, decimal delta)
        {
            decimal result = initial;
            iterarions = 0;
            while (Math.Abs(result - guess) > delta)
            {
                do_newton_iter(in number, ref result, ref guess);
                iterarions += 1;
            }
            return result;
        }

        void calc_initial_appr(in string initial, out decimal appr)
        {
            decimal initial_decimal = Convert.ToDecimal(initial.Replace('.', ','));

            if (initial_decimal < 1)
            {
                appr = initial_decimal / 2;
            }
            double r = Math.Round(initial.Length * 1.6);  // Число бит в двоичной записи числа / 2
            appr = (decimal)Math.Pow(2, r);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            int iterations;
            decimal initial;
            calc_initial_appr(entered_value, out initial);
            decimal result = newton_sqrt(number_decimal, initial, out iterations, ref this.guess, this.delta);
            decimal change = this.guess - result;
            label3.Text = $"{iterations}";
            label4.Text = $"{Math.Abs(change)}";
            label2.Text = $"{result}";
        }
        private void do_newton_iter(in decimal number, ref decimal result, ref decimal guess)
        {
            guess = result;
            result = ((number / guess) + guess) / 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            decimal result;
            int _;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            if (this.exact == null)
            {
                decimal initial;
                calc_initial_appr(entered_value, out initial);
                this.exact = newton_sqrt(number_decimal, initial, out _, ref this.guess, this.delta);
            }
            entered_value = label2.Text.ToString();
            if (!parse_decimal_value(entered_value, out result, out error_message))
            {
                calc_initial_appr(entered_value, out result);
                label6.Text = $"{result}";
            }
            do_newton_iter(in number_decimal, ref result, ref this.guess);
            decimal change = Math.Abs(this.guess - result);
            decimal error = Math.Abs(result - (decimal)this.exact);
            label3.Text = $"{int.Parse(label3.Text.ToString()) + 1}";
            label4.Text = $"{change}";
            label5.Text = $"{error}";
            label2.Text = $"{result}";
        }
        private void clear()
        {
            this.exact = null;
            label2.Text = "0.00";
            label3.Text = "0";
            label4.Text = "0";
            label5.Text = "0";
            label6.Text = "0";
        }
    }
}
