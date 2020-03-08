using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudukuSolver
{
    public partial class Suduku_Form : Form
    {
        private SudukuBoard Board = new SudukuBoard();
        public Suduku_Form()
        {
            this.Controls.Add(Board);
            InitializeComponent();
        }

        public bool ValidateResaults(int[][] arr)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (arr[i][k] == -1)
                    {
                        Board.GetGroupByIndex(i).GetCellByIndex(k).BackColor = Color.Red;
                        MessageBox.Show("Invalid input");
                        return false;

                    }
                    else if ((arr[i][k] > 9 || arr[i][k] < 1) && arr[i][k] != 0)
                    {
                        Board.GetGroupByIndex(i).GetCellByIndex(k).BackColor = Color.Red;
                        MessageBox.Show("Invalid number");
                        return false;
                    }
                    else if (arr[i][k] != 0)
                    {
                        if (!CheckIfPossible(arr, i, k))
                        {
                            Board.GetGroupByIndex(i).GetCellByIndex(k).BackColor = Color.Red;
                            MessageBox.Show("Can't be solved!");
                            return false;
                        }
                        Board.GetGroupByIndex(i).GetCellByIndex(k).isMaster = true;
                        Board.GetGroupByIndex(i).GetCellByIndex(k).Enabled = false;
                    }
                }
            }
            return true;
        }

        public bool CheckIfPossible(int[][] results, int groupIndex, int cellIndex)
        {
            int cellNumber = results[groupIndex][cellIndex];
            int[] columeNumbers = SudukuCellGroup.GetValuesByCellsArray(Board.GetAllCellsFromColume(Board.GetGroupByIndex(groupIndex).colume, Board.GetGroupByIndex(groupIndex).GetCellByIndex(cellIndex).colume));
            int[] rowNumbers = SudukuCellGroup.GetValuesByCellsArray(Board.GetAllCellsFromRow(Board.GetGroupByIndex(groupIndex).row, Board.GetGroupByIndex(groupIndex).GetCellByIndex(cellIndex).row));
            int[] groupNumbers = SudukuCellGroup.CorrectNumberArray(Board.GetGroupByIndex(groupIndex).GetValues());
            int columeCounter = 0;
            int rowCounter = 0;
            int groupCounter = 0;
            for (int i = 0; i < columeNumbers.Length; i++)
            {
                if (columeNumbers[i] == cellNumber)
                {
                    columeCounter++;
                }
            }
            for (int i = 0; i < rowNumbers.Length; i++)
            {
                if (rowNumbers[i] == cellNumber)
                {
                    rowCounter++;
                }
            }
            for (int i = 0; i < groupNumbers.Length; i++)
            {
                if (groupNumbers[i] == cellNumber)
                {
                    groupCounter++;
                }
            }
            if (columeCounter > 1 || rowCounter > 1 || groupCounter > 1) return false;
            return true;
        }
        public int[] GetPossibleNumbers(int groupIndex, int cellIndex)
        {
            int[] columeNumbers = SudukuCellGroup.GetValuesByCellsArray(Board.GetAllCellsFromColume(Board.GetGroupByIndex(groupIndex).colume, Board.GetGroupByIndex(groupIndex).GetCellByIndex(cellIndex).colume));
            int[] rowNumbers = SudukuCellGroup.GetValuesByCellsArray(Board.GetAllCellsFromRow(Board.GetGroupByIndex(groupIndex).row, Board.GetGroupByIndex(groupIndex).GetCellByIndex(cellIndex).row));
            int[] groupNumbers = SudukuCellGroup.CorrectNumberArray(Board.GetGroupByIndex(groupIndex).GetValues());
            int[] remaining = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < columeNumbers.Length; i++)
            {
                if (columeNumbers[i] == 0) continue;
                remaining[columeNumbers[i] - 1] = 1;
            }
            for (int i = 0; i < rowNumbers.Length; i++)
            {
                if (rowNumbers[i] == 0) continue;
                remaining[rowNumbers[i] - 1] = 1;
            }
            for (int i = 0; i < groupNumbers.Length; i++)
            {
                if (groupNumbers[i] == 0) continue;
                remaining[groupNumbers[i] - 1] = 1;
            }
            List<int> remainingNumbers = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (remaining[i] == 0)
                {
                    remainingNumbers.Add(i + 1);
                }
            }
            return remainingNumbers.ToArray();
        }


        public int[] GetNextIndex(int groupIndex, int cellIndex)
        {
            if (groupIndex == 8 && cellIndex == 8)
            {
                groupIndex = -1;
                cellIndex = -1;
            }
            else if (cellIndex == 8)
            {
                groupIndex++;
                cellIndex = 0;
            }
            else
            {
                cellIndex++;
            }
            return new int[2] { groupIndex, cellIndex };
        }

        public int[] GetNextNoMasterIndex(int groupIndex, int cellIndex)
        {
            int[] nextIndex = GetNextIndex(groupIndex, cellIndex);
            if (nextIndex[0] == -1 && Board.GetCellByIndexes(groupIndex, cellIndex).isMaster)
            {
                return nextIndex;
            }
            else if (nextIndex[0] == -1)
            {
                return new int[2] { groupIndex, cellIndex };
            }
            if (Board.GetCellByIndexes(nextIndex[0], nextIndex[1]).isMaster == false)
            {
                return nextIndex;
            }
            else
            {
                return GetNextNoMasterIndex(nextIndex[0], nextIndex[1]);
            }
        }

        public bool CanSolve(int[][] results, int groupIndex, int cellIndex)
        {
            if (SeeSolution.Checked)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }

            int[] possible = GetPossibleNumbers(groupIndex, cellIndex);
            int[] nextIndex = GetNextNoMasterIndex(groupIndex, cellIndex);

            if (possible.Length == 0)
            {
                return false;
            }
            if ((groupIndex == 8 && cellIndex == 8) || nextIndex[0] == -1)
            {
                Board.GetCellByIndexes(groupIndex, cellIndex).Text = possible[0].ToString();
                results[groupIndex][cellIndex] = possible[0];
                return true;
            }
            else
            {
                foreach (int choice in possible)
                {
                    Board.GetCellByIndexes(groupIndex, cellIndex).Text = choice.ToString();
                    results[groupIndex][cellIndex] = choice;
                    if (CanSolve(results, nextIndex[0], nextIndex[1])) return true;
                    Board.GetCellByIndexes(groupIndex, cellIndex).Text = "";
                    results[groupIndex][cellIndex] = 0;
                }
                return false;
            }
        }

        public bool SetSurePlaces(int groupIndex, int cellIndex)
        {
            int[] possible = GetPossibleNumbers(groupIndex, cellIndex);
            int[] nextIndex = GetNextNoMasterIndex(groupIndex, cellIndex);

            if (possible.Length == 1)
            {
                Board.GetCellByIndexes(groupIndex, cellIndex).Text = possible[0].ToString();
                Board.GetCellByIndexes(groupIndex, cellIndex).isMaster = true;
            }

            if ((groupIndex == 8 && cellIndex == 8) || nextIndex[0] == -1)
            {
                return true;
            }

            return SetSurePlaces(nextIndex[0], nextIndex[1]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void Solve_Click(object sender, EventArgs e)
        {
            int[][] arr = Board.GetValues();
            int[] startPlace;
            if (ValidateResaults(arr) == false)
            {
                return;
            }
            Solve.Enabled = false;
            Reset.Enabled = false;
            if (Board.GetCellByIndexes(0, 0).isMaster == false)
            {
                startPlace = new int[2] { 0, 0 };
            }
            else
            {
                startPlace = GetNextNoMasterIndex(0, 0);
            }
            try
            {
                SetSurePlaces(startPlace[0], startPlace[1]);
                if (!CanSolve(arr, startPlace[0], startPlace[1]))
                {
                    MessageBox.Show("Can't Solve", ":(");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Solve", ":(");
            }
            Reset.Enabled = true;
            Solve.Enabled = true;
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            Board.Reset();            
        }
    }
}
