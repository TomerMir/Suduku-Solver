using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudukuSolver
{
    /// <summary>
    /// תא יחיד בלוח הסודוקו, יורש מהמחלקה של תיבת טקסט
    /// </summary>
    class SudukuCell : System.Windows.Forms.TextBox
    {
        /// <summary>
        /// הטור שבו התא נמצא
        /// </summary>
        public int row { get; }

        /// <summary>
        /// השורה שבו התא נמצא
        /// </summary>
        public int colume { get; }

        /// <summary>
        /// משתנה בוליאני שמסמן אם התא הוא תא שאין לשנות אותו
        /// </summary>
        public bool isMaster;

        /// <summary>
        /// הערך שבתוך התא
        /// </summary>
        private int value;

        public SudukuCell(int colume, int row)
        {
            this.row = row;
            this.colume = colume;
            isMaster = false;
        }

        public void SetValue(int n)
        {
            this.value = n;
        }

        /// <summary>
        /// מחזיר את הערך של התא לפי הטקסט שהוא מכיל
        /// </summary>
        /// <returns></returns>
        public int GetValueFromText()
        {
            try
            {
                if (this.Text == "")
                {
                    return 0;
                }
                int text = int.Parse(this.Text);
                if (text == 0)
                {
                    return -1;
                }
                return text;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int GetValue()
        {
            return this.value;
        }
    }
    /// <summary>
    /// TableLayoutPanel קבוצת תאים בלוח הסודוקו, יורש מהמחלקה 
    /// </summary>
    class SudukuCellGroup : System.Windows.Forms.TableLayoutPanel
    {
        public int row { get; } //הטור שבה הקבוצה נמצאת
        public int colume { get; } //השורה שבה הקבוצה נמצאת

        /// <summary>
        /// פעולה בונה שמייצרת את הקבוצה מבחינה גרפית
        /// </summary>
        /// <param name="colume"></param>
        /// <param name="row"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
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
                    tmpCell.TextChanged += new EventHandler((s, e) => tmpCell.SetValue(tmpCell.GetValueFromText()));
                    
                    this.Controls.Add(tmpCell, i, k);
                }
            }
        }

        /// <summary>
        /// מחזיר מערך של ערכי התאים שהקבוצה מכילה
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// מחזיר ערך של תא בקבוצה לפי אינדקס
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SudukuCell GetCellByIndex(int index)
        {
            return (SudukuCell)this.Controls[index];
        }

        /// <summary>
        /// מחזיר מערך של כל התאים שנמצאים בשורה מסויימת
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public SudukuCell[] GetAllCellsByRow(int row)
        {
            List<SudukuCell> list = new List<SudukuCell>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetCellByIndex(i).row == row) list.Add(this.GetCellByIndex(i));
            }
            return list.ToArray();
        }

        /// <summary>
        /// מחזיר מערך של כל התאים שנמצאים בטור מסויים
        /// </summary>
        /// <param name="colume"></param>
        /// <returns></returns>
        public SudukuCell[] GetAllCellsByColume(int colume)
        {
            List<SudukuCell> list = new List<SudukuCell>();
            for (int i = 0; i < 9; i++)
            {
                if (this.GetCellByIndex(i).colume == colume) list.Add(this.GetCellByIndex(i));
            }
            return list.ToArray();
        }

        /// <summary>
        /// מחזיר מערך של כל הערכים ממערך של תאים
        /// </summary>
        /// <param name="cellsArr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// מקבל מערך של מספרים ומחזיר מערך של כל המספרים ממערך זה שגדולים מ0
        /// </summary>
        /// <param name="arr">מערך של ערכים</param>
        /// <returns></returns>
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
