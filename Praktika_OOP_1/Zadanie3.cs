using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika_OOP_1
{
    public partial class Zadanie3 : Form
    {
        public Zadanie3()
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
        public Stack<int> GetInNumberSystem(int value, int system)
        {
            if (system > 36)
                return new Stack<int>();
            var result = new Stack<int>();
            while (value > 0)
            {
                result.Push(value % system);
                value /= system;
            }
            return result;
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            int boxValue;
            var newValue = new StringBuilder();
            if (!int.TryParse(textBox1.Text, out boxValue))
            {
                MessageBox.Show("Невозможно преобразовать.");
                return;
            }
            var system = int.Parse(comboBox1.Text);
            var stack = GetInNumberSystem(boxValue, system);
            while (stack.Count > 0)
            {
                var element = stack.Pop();
                if (element > 9)
                    newValue.Append((char)(element + 55));
                else
                    newValue.Append(element);
            }
            label1.Text = newValue.ToString();
        }
    }
}
