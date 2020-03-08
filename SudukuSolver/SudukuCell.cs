using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudukuSolver
{
    class SudukuCell : System.Windows.Forms.TextBox
    {
        public int row { get; }
        public int colume { get; }

        public bool isMaster;
        public SudukuCell(int colume, int row)
        {
            this.row = row;
            this.colume = colume;
            isMaster = false;
        }
        
        public int GetValue()
        {
            try
            {
                if (this.Text == "") return 0;
                if (int.Parse(this.Text) == 0) return -1;
                return int.Parse(this.Text);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
    class SudukuCellGroup : System.Windows.Forms.TableLayoutPanel
    {
        public int row { get; }
        public int colume { get; }
        public SudukuCellGroup(int colume, int row, int width, int height)
        {
            this.row = row;
            this.colume = colume;

            this.ColumnCount = 3;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));

            this.RowCount = 3;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));


            this.Location = new System.Drawing.Point(0, 20);
            this.Name = row.ToString() + colume.ToString();

            this.Size = new System.Drawing.Size(width, height);

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    SudukuCell tmpCell = new SudukuCell(i, k);
                    tmpCell.ForeColor = Color.DimGray;
                    tmpCell.Font = new Font(tmpCell.Font.FontFamily, 25);
                    tmpCell.TextAlign = HorizontalAlignment.Center;
                    tmpCell.AutoSize = false;
                    tmpCell.Size = new System.Drawing.Size(width / 3, height / 3);
                    tmpCell.Click += new EventHandler((s, e) => tmpCell.BackColor = Color.White);

                    //tmpCell.Text = row.ToString() + colume.ToString() + tmpCell.row.ToString() + tmpCell.colume.ToString();
                    this.Controls.Add(tmpCell, i, k);
                }
            }
        }

        public int[] GetValues()
        {
            int[] tmp = new int[9];
            for (int i = 0; i < 9; i++)
            {
                SudukuCell tmpControl = (SudukuCell)this.Controls[i];
                tmp[i] = tmpControl.GetValue();
            }
            return tmp;
        }
        
        public SudukuCell GetCellByIndex(int index)
        {
            return (SudukuCell)this.Controls[index];
        }

        public SudukuCell[] GetAllCellsByRow(int row)
        {
            List<SudukuCell> list = new List<SudukuCell>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetCellByIndex(i).row == row) list.Add(this.GetCellByIndex(i));
            }
            return list.ToArray();
        }
         
        public SudukuCell[] GetAllCellsByColume(int colume)
        {
            List<SudukuCell> list = new List<SudukuCell>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetCellByIndex(i).colume == colume) list.Add(this.GetCellByIndex(i));
            }
            return list.ToArray();
        }

        public static int[] GetValuesByCellsArray(SudukuCell[] cellsArr)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < cellsArr.Length; i++)
            {
                if (cellsArr[i].GetValue() > 0)
                {
                    list.Add(cellsArr[i].GetValue());
                }
            }
            return list.ToArray();
        }

        public static int[] CorrectNumberArray(int[] arr)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    list.Add(arr[i]);
                }
            }
            return list.ToArray();
        }
    }
}
