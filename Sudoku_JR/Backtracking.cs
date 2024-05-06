using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_JR
{
    class Backtracking
    {
        public static bool Siguran(int[,] matrica, int red, int stupac, int broj)
        {
            // za svaki stupac u retku ako sadrzi broj vrati da nije siguran
            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                if (matrica[red, i] == broj)
                {
                    return false;
                }
            }
            // za svaki redak u stupcu ako sadrzi broj vrati da nije siguran
            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                if (matrica[i, stupac] == broj)
                {
                    return false;
                }
            }
            // korijenuj duljinu prvog retka da dobijes duljinu subkvadrata 
            int korijen = (int)Math.Sqrt(matrica.GetLength(0));
            //odakle krece red subkvadrata 
            int kvadratRKreni = red - red % korijen;
            //odakle krece stupac subkvadrata
            int kvadratSKreni = stupac - stupac % korijen;

            // ako se broj nalazi u subkvadratu vec, vrati da nije siguran
            for (int i = kvadratRKreni; i < kvadratRKreni + korijen; i++)
            {
                for (int j = kvadratSKreni; j < kvadratSKreni + korijen; j++)
                {
                    if (matrica[i, j] == broj)
                    {
                        return false;
                    }
                }
            }
            //ako se ne nalazi ni u retku ni u stupcu ni u subkvadratu broj vrati da je siguran
            return true;
        }

        public static bool Pretraga(int[,] matrica, int n)
        {
            int red = -1;
            int stupac = -1;
            bool prazan = true;
            //nadi prvog praznog, ako je prazan spremi indeks retka i stupca
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrica[i, j] == 0)
                    {
                        red = i;
                        stupac = j;
                        prazan = false;
                        break;
                    }
                }
                if (!prazan)
                {
                    break;
                }
            }
            if (prazan)
            {
                return true;
            }
            //za svaki broj provjeri je li siguran, ako je stavi ga na praznu poziciju
            for (int i = 1; i <= n; i++)
            {
                if (Siguran(matrica, red, stupac, i))
                {
                    //IspisKoraka(board);
                    matrica[red, stupac] = i;
                    //rekurzivna promjena i dodavanje, ako ne ide, posatvi na 0
                    if (Pretraga(matrica, n))
                    {
                        return true;
                    }
                    else
                    {
                        matrica[red, stupac] = 0;
                    }
                }
            }
            return false;
        }
        // ispis rjesenog sudokua
        public static int[,] Ispis(int[,] matrica)
        {

            return matrica;
        }
    }
}
