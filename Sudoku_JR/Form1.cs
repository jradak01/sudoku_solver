using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sudoku_JR
{
    public partial class Sudoku : Form
    {
        public Sudoku()
        {
            InitializeComponent();
        }
        public static TextBox[,] enterBoxes = new TextBox[9, 9];
        
        private void Sudoku_Load(object sender, EventArgs e)
        {
            #region dodavanjeUtablicu
            enterBoxes[0, 0] = tba1;
            enterBoxes[0, 1] = tba2;
            enterBoxes[0, 2] = tba3;
            enterBoxes[0, 3] = tba4;
            enterBoxes[0, 4] = tba5;
            enterBoxes[0, 5] = tba6;
            enterBoxes[0, 6] = tba7;
            enterBoxes[0, 7] = tba8;
            enterBoxes[0, 8] = tba9;

            enterBoxes[1, 0] = tbb1;
            enterBoxes[1, 1] = tbb2;
            enterBoxes[1, 2] = tbb3;
            enterBoxes[1, 3] = tbb4;
            enterBoxes[1, 4] = tbb5;
            enterBoxes[1, 5] = tbb6;
            enterBoxes[1, 6] = tbb7;
            enterBoxes[1, 7] = tbb8;
            enterBoxes[1, 8] = tbb9;

            enterBoxes[2, 0] = tbc1;
            enterBoxes[2, 1] = tbc2;
            enterBoxes[2, 2] = tbc3;
            enterBoxes[2, 3] = tbc4;
            enterBoxes[2, 4] = tbc5;
            enterBoxes[2, 5] = tbc6;
            enterBoxes[2, 6] = tbc7;
            enterBoxes[2, 7] = tbc8;
            enterBoxes[2, 8] = tbc9;

            enterBoxes[3, 0] = tbd1;
            enterBoxes[3, 1] = tbd2;
            enterBoxes[3, 2] = tbd3;
            enterBoxes[3, 3] = tbd4;
            enterBoxes[3, 4] = tbd5;
            enterBoxes[3, 5] = tbd6;
            enterBoxes[3, 6] = tbd7;
            enterBoxes[3, 7] = tbd8;
            enterBoxes[3, 8] = tbd9;

            enterBoxes[4, 0] = tbe1;
            enterBoxes[4, 1] = tbe2;
            enterBoxes[4, 2] = tbe3;
            enterBoxes[4, 3] = tbe4;
            enterBoxes[4, 4] = tbe5;
            enterBoxes[4, 5] = tbe6;
            enterBoxes[4, 6] = tbe7;
            enterBoxes[4, 7] = tbe8;
            enterBoxes[4, 8] = tbe9;
            enterBoxes[3, 0] = tbd1;

            enterBoxes[5, 0] = tbf1;
            enterBoxes[5, 1] = tbf2;
            enterBoxes[5, 2] = tbf3;
            enterBoxes[5, 3] = tbf4;
            enterBoxes[5, 4] = tbf5;
            enterBoxes[5, 5] = tbf6;
            enterBoxes[5, 6] = tbf7;
            enterBoxes[5, 7] = tbf8;
            enterBoxes[5, 8] = tbf9;

            enterBoxes[6, 0] = tbg1;
            enterBoxes[6, 1] = tbg2;
            enterBoxes[6, 2] = tbg3;
            enterBoxes[6, 3] = tbg4;
            enterBoxes[6, 4] = tbg5;
            enterBoxes[6, 5] = tbg6;
            enterBoxes[6, 6] = tbg7;
            enterBoxes[6, 7] = tbg8;
            enterBoxes[6, 8] = tbg9;

            enterBoxes[7, 0] = tbh1;
            enterBoxes[7, 1] = tbh2;
            enterBoxes[7, 2] = tbh3;
            enterBoxes[7, 3] = tbh4;
            enterBoxes[7, 4] = tbh5;
            enterBoxes[7, 5] = tbh6;
            enterBoxes[7, 6] = tbh7;
            enterBoxes[7, 7] = tbh8;
            enterBoxes[7, 8] = tbh9;

            enterBoxes[8, 0] = tbi1;
            enterBoxes[8, 1] = tbi2;
            enterBoxes[8, 2] = tbi3;
            enterBoxes[8, 3] = tbi4;
            enterBoxes[8, 4] = tbi5;
            enterBoxes[8, 5] = tbi6;
            enterBoxes[8, 6] = tbi7;
            enterBoxes[8, 7] = tbi8;
            enterBoxes[8, 8] = tbi9;
            #endregion

        }
        
        public int[,] newMatrixB = new int[9, 9];
        public int[,] newMatrixC = new int[9, 9];
        public int[,] newMatrixK = new int[9, 9];
        public int[,] startMatrix = new int[9, 9];
        public int[,] remove;
        public int type = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    enterBoxes[i, j].ForeColor = Color.Black;
                    enterBoxes[i, j].BackColor = Color.White;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    enterBoxes[i, j].Text = "";
                    enterBoxes[i, j].ReadOnly = false;
                }
            }
            
            type=trackBar1.Value;
            remove = SudokuGenerator.Izbrisi(type);
            SudokuGenerator.Init(ref startMatrix);
            SudokuGenerator.Nadogradi(startMatrix, 10);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (remove[i, j] == 0)
                    {
                        newMatrixK[i, j] = startMatrix[i, j];
                        newMatrixC[i, j] = startMatrix[i, j];
                        newMatrixB[i, j] = startMatrix[i, j];
                        enterBoxes[i, j].Text = startMatrix[i, j].ToString();
                        enterBoxes[i, j].ReadOnly = true;
                    }
                    else
                    {
                        newMatrixK[i, j] = 0;
                        newMatrixC[i, j] = 0;
                        newMatrixB[i, j] = 0;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (enterBoxes[i, j].ReadOnly == false)
                    {
                        enterBoxes[i, j].ForeColor = Color.Black;
                        enterBoxes[i, j].BackColor = Color.White;
                    }
                }
            }
            int N = newMatrixB.GetLength(0);
            List<int> brojevi = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                brojevi.Add(i);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (Backtracking.Pretraga(newMatrixB, N))
            {
                int w=0;
                int c=0;
                
                int[,] solved = Backtracking.Ispis(newMatrixB);
                stopwatch.Stop();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (enterBoxes[i, j].ReadOnly == false)
                        {
                            int n=0; 
                            bool jeBroj = int.TryParse(enterBoxes[i,j].Text, out n);
                            if (jeBroj)
                            {
                                if (brojevi.Contains(n))
                                {
                                    if (n == solved[i, j])
                                    {
                                        enterBoxes[i, j].ForeColor = Color.Green;
                                        enterBoxes[i, j].Text = solved[i, j].ToString();
                                        c += 1;
                                    }
                                    else if(n!= solved[i, j])
                                    {
                                        enterBoxes[i, j].ForeColor = Color.Red;
                                        enterBoxes[i, j].Text = solved[i, j].ToString();
                                        w += 1;
                                    }
                                }
                            }
                            else if(!jeBroj)
                            {
                                enterBoxes[i, j].BackColor = Color.Red;
                                enterBoxes[i, j].Text = solved[i, j].ToString();
                                w += 1;
                            }
                        }
                    }
                }
                TimeSpan ts = stopwatch.Elapsed;
                label3.Text = ts.ToString(@"hh\:mm\:ss\.ffffff");
                label3.Text += "\nNumber of wrong fields: " + w.ToString();
                label3.Text += "\nNumber of correct fields: " + c.ToString();
            }
            else
            {
                label3.Text = "There is no solution for this!";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HowToPlay kako = new HowToPlay();
            kako.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[,] matrica = new string[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    matrica[i, j] = newMatrixC[i, j].ToString();
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (enterBoxes[i, j].ReadOnly == false)
                    {
                        enterBoxes[i, j].ForeColor = Color.Black;
                        enterBoxes[i, j].BackColor = Color.White;
                    }
                }
            }
            List<int> brojevi = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                brojevi.Add(i);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[,] solved = Crook.Crooks(matrica);
            stopwatch.Stop();
            int c = 0;
            int w = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (enterBoxes[i, j].ReadOnly == false)
                    {
                        int n = 0;
                        bool jeBroj = int.TryParse(enterBoxes[i, j].Text, out n);
                        if (jeBroj)
                        {
                            if (brojevi.Contains(n))
                            {
                                int k;
                                bool ke = int.TryParse(solved[i, j], out k);
                                if (ke == true && n == k)
                                {
                                    enterBoxes[i, j].ForeColor = Color.Green;
                                    enterBoxes[i, j].Text = solved[i, j].ToString();
                                    c += 1;
                                }
                                else
                                {
                                    enterBoxes[i, j].ForeColor = Color.Red;
                                    enterBoxes[i, j].Text = solved[i, j].ToString();
                                    w += 1;
                                }
                            }
                        }
                        else if (!jeBroj)
                        {
                            enterBoxes[i, j].BackColor = Color.Red;
                            enterBoxes[i, j].Text = solved[i, j].ToString();
                            w += 1;
                        }
                    }
                }
            }
            TimeSpan ts = stopwatch.Elapsed;
            label3.Text = ts.ToString(@"hh\:mm\:ss\.ffffff");
            label3.Text += "\nNumber of wrong fields: " + w.ToString();
            label3.Text += "\nNumber of correct fields: " + c.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (enterBoxes[i, j].ReadOnly == false)
                    {
                        enterBoxes[i, j].ForeColor = Color.Black;
                        enterBoxes[i, j].BackColor = Color.White;
                    }
                }
            }
            List<int> brojevi = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                brojevi.Add(i);
            }
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DlxMatrix _dlxMatrix = new DlxMatrix();

            _dlxMatrix.UcitajMatricu(newMatrixK);
            Rjesenje rjesenje = new Rjesenje();
            int dubina = _dlxMatrix.Trazi(0, rjesenje);
            stopwatch.Stop();

            int w = 0;
            int c = 0;
            for (int red = 0; red < Konstante.Redovi; red++)
            {
                for (int stupac = 0; stupac < Konstante.Stupci; stupac++)
                {
                    if (enterBoxes[red, stupac].ReadOnly == false)
                    {
                        int n = 0;
                        bool jeBroj = int.TryParse(enterBoxes[red, stupac].Text, out n);
                        if (jeBroj)
                        {
                            if (brojevi.Contains(n))
                            {
                                if (n == rjesenje.Matrica[red, stupac])
                                {
                                    enterBoxes[red, stupac].Text = rjesenje.Matrica[red, stupac].ToString();
                                    enterBoxes[red, stupac].ForeColor = Color.Green;
                                    c++;
                                }
                                else if (n != rjesenje.Matrica[red, stupac])
                                {
                                    enterBoxes[red, stupac].Text = rjesenje.Matrica[red, stupac].ToString();
                                    enterBoxes[red, stupac].ForeColor = Color.Red;
                                    w++;
                                }
                            }
                        }
                        else if (!jeBroj)
                        {
                            enterBoxes[red, stupac].BackColor = Color.Red;
                            enterBoxes[red, stupac].Text = rjesenje.Matrica[red, stupac].ToString();
                            w++;
                        }
                    }
                }
            }
            TimeSpan ts = stopwatch.Elapsed;
            label3.Text = ts.ToString(@"hh\:mm\:ss\.ffffff");
            label3.Text += "\nNumber of wrong fields: " + w.ToString();
            label3.Text += "\nNumber of correct fields: " + c.ToString();

        }
    }
}
