using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_JR
{
    class SudokuGenerator
    {
        public static int[,] matrica = new int[9, 9];
        //napravi matricu tako da se ni jedan broj ne ponavlja u retku, stupcu ili kvadratu 3x3
        public static void Init(ref int[,] matrica)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrica[i, j] = (i * 3 + i / 3 + j) % 9 + 1;

                }
            }
        }
        //brisanje odredenog broja mjesta iz generirane matrice
        public static int[,] Izbrisi(int tip)
        {
            //broj prikazanih brojeva
            int k=37;
            Random r = new Random();
            if (tip == 0)
            {
                k = 37;
            }
            else if (tip == 1)
            {
                k = 27;
            }
            else if (tip == 2)
            {
                k = 17;
            }
            int z = 0;
            //u listu spremamo slucajno izgenerirane pozicije
            List<int> niz = new List<int>();
            while (z <= k)
            {
                int re = r.Next(0, 81);
                if (!niz.Contains(re))
                {
                    niz.Add(re);
                    z++;
                }
            }
            int[,] matrica = new int[9,9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrica[i, j] = -1;
                }
            }
            //za svaku slucajno izgeneriranu poziciju, ostavi broj
            foreach (int broj in niz)
            {
                if (broj < 9)
                {
                    matrica[0, broj] = 0;
                }
                else if (broj > 8 && broj < 18)
                {
                    matrica[1, broj - 9] = 0;
                }
                else if (broj > 17 && broj < 27)
                {
                    matrica[2, broj - 18] = 0;
                }
                else if (broj > 26 && broj < 36)
                {
                    matrica[3, broj - 27] = 0;
                }
                else if (broj > 35 && broj < 45)
                {
                    matrica[4, broj - 36] = 0;
                }
                else if (broj > 44 && broj < 54)
                {
                    matrica[5, broj - 45] = 0;
                }
                else if (broj > 53 && broj < 63)
                {
                    matrica[6, broj - 54] = 0;
                }
                else if (broj > 62 && broj < 72)
                {
                    matrica[7, broj - 63] = 0;
                }
                else if (broj > 71 && broj < 81)
                {
                    matrica[8, broj - 72] = 0;
                }
            }
            return matrica;
        }
        // pronadi 2 polja s odredenim vrijednostima i zamijeni im vrijednosti
        public static void PromijeniDvaPolja(int[,] matrica, int pronadiVrijednost1, int pronadiVrijednost2)
        {
            int xParm1, yParm1, xParm2, yParm2;
            xParm1 = yParm1 = xParm2 = yParm2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int k = 0; k < 9; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (matrica[i + j, k + z] == pronadiVrijednost1)
                            {
                                xParm1 = i + j;
                                yParm1 = k + z;

                            }
                            if (matrica[i + j, k + z] == pronadiVrijednost2)
                            {
                                xParm2 = i + j;
                                yParm2 = k + z;

                            }
                        }
                    }
                    matrica[xParm1, yParm1] = pronadiVrijednost2;
                    matrica[xParm2, yParm2] = pronadiVrijednost1;
                }
            }
        }
        public static void Nadogradi(int[,] matrica, int levelIzmjene)
        {
            //ponavljamo izmjene levelIzmjena puta
            for (int repeat = 0; repeat < levelIzmjene; repeat++)
            {
                //napravi izmjenu za 2 slucajne vrijednosti polja 
                Random rand = new Random();
                Random rand2 = new Random();
                PromijeniDvaPolja(matrica, rand.Next(1, 10), rand2.Next(1, 10));
            }
        }
    }
}
