using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika_OOP_2
{
    internal class Punkt4
    {
        public static void ShowMatrix(Panel panel, in int[,] matrix)
        {
            // Создаем DataGridView и устанавливаем его свойства
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.CellValidating += dataGridView_CellValidating;

            // Создаем столбцы
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                dataGridView.Columns.Add("-", "-");
            }

            // Заполняем столбцы данными из матрицы
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                dataGridView.Rows.Add();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
            // Добавляем DataGridView в Panel
            panel.Controls.Add(dataGridView);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
        }
        private static void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (!int.TryParse(e.FormattedValue.ToString(), out int value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Значение должно быть числом");
                }
            }
        }
        public static void RandomMatrix(ref int[,] matrix, int max_rand = 100)
        {
            Random random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = random.Next(max_rand);
        }
        public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int columns2 = matrix2.GetLength(1);
            int[,] result = new int[rows1, columns2];

            for (int i = 0; i < rows1; i++)
                for (int j = 0; j < columns2; j++)
                    for (int k = 0; k < columns1; k++)
                        result[i, j] += matrix1[i, k] * matrix2[k, j];

            return result;
        }
        public static bool try_get_matrix(Panel panel, out int[,] matrix)
        {
            if (panel.Controls.Count > 0)
            {
                var data_grid = (panel.Controls[panel.Controls.Count - 1] as DataGridView);
                matrix = new int[data_grid.Rows.Count, data_grid.Columns.Count];

                for (int i = 0; i < data_grid.Rows.Count; i++)
                {
                    for (int j = 0; j < data_grid.Columns.Count; j++)
                    {
                        DataGridViewCell cell = data_grid.Rows[i].Cells[j];
                        int value = Convert.ToInt32(cell.Value);
                        matrix[i, j] = value;
                    }
                }
                return true;
            }
            matrix = null;
            return false;
        }
    }
}
