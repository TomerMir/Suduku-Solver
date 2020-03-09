using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudukuSolver
{
    class SudukuBoard : System.Windows.Forms.TableLayoutPanel
    {
        private int width = 500;
        private int height = 500;

        public SudukuBoard()
        {
            this.ColumnCount = 3;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));

            this.RowCount = 3;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));


            this.Location = new System.Drawing.Point(0, 40);
            this.Name = "Board";

            this.Size = new System.Drawing.Size(this.width, this.height);

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    SudukuCellGroup tmp_group = new SudukuCellGroup(i, k, this.width / 3, this.height / 3);
                    this.Controls.Add(tmp_group, i, k);
                }
            }
        }
        public int[][] GetValues()
        {
            int[][] tmp = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                SudukuCellGroup tmpGroup = (SudukuCellGroup)this.Controls[i];
                tmp[i] = tmpGroup.GetValues();
            }
            return tmp;
        }

        public SudukuCellGroup GetGroupByIndex(int index)
        {
            return (SudukuCellGroup)this.Controls[index];
        }

        public void Reset()
        {
            foreach (SudukuCell cell in this.GetAllCells())
            {
                cell.BackColor = Color.White;
                cell.Enabled = true;
                cell.Text = "";
                cell.isMaster = false;
                cell.SetValue(0);
            }
        }

        public void ResetColors()
        {
            foreach (SudukuCell cell in this.GetAllCells())
            {
                cell.BackColor = Color.White;
            }
        }

        public SudukuCell[] GetAllCells()
        {
            SudukuCell[] arr = new SudukuCell[81];
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    arr[counter] = this.GetGroupByIndex(i).GetCellByIndex(k);
                    counter++;
                }
            }
            return arr;
        }

        public SudukuCellGroup[] GetAllGroupsByRow(int row)
        {
            List<SudukuCellGroup> list = new List<SudukuCellGroup>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetGroupByIndex(i).row == row) list.Add(this.GetGroupByIndex(i));
            }
            return list.ToArray();
        }

        public SudukuCellGroup[] GetAllGroupsByColume(int colume)
        {
            List<SudukuCellGroup> list = new List<SudukuCellGroup>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetGroupByIndex(i).colume == colume) list.Add(this.GetGroupByIndex(i));
            }
            return list.ToArray();
        }

        public SudukuCell[] GetAllCellsFromRow(int groupRow, int row)
        {
            SudukuCell[] arr = new SudukuCell[9];
            SudukuCell[] tmp;
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                if (this.GetGroupByIndex(i).row == groupRow)
                {

                    tmp = this.GetGroupByIndex(i).GetAllCellsByRow(row);
                    for (int j = 0; j < 3; j++)
                    {
                        arr[counter] = tmp[j];
                        counter++;
                    }
                }
            }
            return arr;
        }

        public SudukuCell[] GetAllCellsFromColume(int groupColume, int colume)
        {
            SudukuCell[] arr = new SudukuCell[9];
            SudukuCell[] tmp;
            int counter = 0;
            for (int i = 0; i < 9; i++)
            {
                if (this.GetGroupByIndex(i).colume == groupColume)
                {
                    tmp = this.GetGroupByIndex(i).GetAllCellsByColume(colume);
                    for (int j = 0; j < 3; j++)
                    {
                        arr[counter] = tmp[j];
                        counter++;
                    }
                }
            }
            return arr;
        }

        public SudukuCell GetCellByIndexes(int groupIndex, int cellIndex)
        {
            return this.GetGroupByIndex(groupIndex).GetCellByIndex(cellIndex);
        }

        public void AppendFromArray(int[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int k = 0; k < arr[i].Length; k++)
                {
                    this.GetCellByIndexes(i, k).Text = arr[i][k].ToString();
                }
            }
        }
    }
}
