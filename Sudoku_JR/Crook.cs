using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_JR
{
    class Crook
    {
        public static int velicinaUk = 81;
        public static int velicina = 9;
        public static bool ProvjeraMatrice(string[,] matrica)
        {
            bool provjera = true;
            int[] sazmi = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] brojevi = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    foreach (char p in matrica[i, j])
                    {
                        if (p != ' ')
                        {
                            for (int k = 0; k < velicina; k++)
                            {
                                if (int.Parse(p.ToString()) == brojevi[k])
                                {
                                    sazmi[k] += 1;
                                }
                            }
                        }
                    }
                }
            }
            foreach (int br in sazmi)
            {
                if (br != 9)
                {
                    provjera = false;
                }
            }
            return provjera;

        }
        public static int[] prviKvadrat = new int[] { 0, 1, 2 };
        public static int[] drugiKvadrat = new int[] { 3, 4, 5 };
        public static int[] treciKvadrat = new int[] { 6, 7, 8 };
        private static int[] KvadratRed(int red)
        {
            int[] kvadratRed = new int[3];
            if (red < 3)
            {
                kvadratRed = prviKvadrat;
            }
            else if (red > 2 && red < 6)
            {
                kvadratRed = drugiKvadrat;
            }
            else if (red > 5 && red < 9)
            {
                kvadratRed = treciKvadrat;
            }
            return kvadratRed;
        }
        private static int[] KvadratStupac(int stupac)
        {
            int[] kvadratStupac = new int[3];
            if (stupac < 3)
            {
                kvadratStupac = prviKvadrat;
            }
            else if (stupac > 2 && stupac < 6)
            {
                kvadratStupac = drugiKvadrat;
            }
            else if (stupac > 5 && stupac < 9)
            {
                kvadratStupac = treciKvadrat;
            }
            return kvadratStupac;
        }
        public static void MogucaRjesenja(string[,] pocetnaMatrica)
        {
            //matrica u koju se spremaju brojevi
            //ako ima spremljeno više od jedan broj ili ako nije spremljen ni jedan broj onda je iznos nula
            //inaće iznos je jednak vrijednosti
            int[,] pretvorenaMatrica = new int[9, 9];
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    if (pocetnaMatrica[i, j].Length > 1 || pocetnaMatrica[i, j].Length < 1)
                    {
                        pretvorenaMatrica[i, j] = 0;
                    }
                    else
                    {
                        int broj = int.Parse(pocetnaMatrica[i, j]);
                        pretvorenaMatrica[i, j] = broj;
                    }
                }
            }
            //lista koja sprema sve pozicije koje imaju nulu
            List<string> pozicije = new List<string>();
            //petlja koja traži pozicije onih koji imaju vrijednost nula
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {

                    if (pretvorenaMatrica[i, j] == 0)
                    {
                        string s = i.ToString() + " " + j.ToString();
                        pozicije.Add(s);
                    }
                }
            }


            foreach (string s in pozicije)
            {
                //za svaku poziciju koja je dana u listu, traži prema pravilima manji kvadrat, stupac, red gdje se nalazi
                string[] a = s.Split(' ');
                int red = int.Parse(a[0]);
                int stupac = int.Parse(a[1]);
                int[] kvadratRed = KvadratRed(red);
                int[] kvadratStupac = KvadratStupac(stupac);


                List<int> stupacPosjeduje = new List<int>();
                List<int> redPosjeduje = new List<int>();
                List<int> kvadratPosjeduje = new List<int>();
                //za svaki element reda/stupca koji je razlicit od nula, dodaj ga u listu
                for (int i = 0; i < velicina; i++)
                {
                    if (pretvorenaMatrica[i, stupac] != 0)
                    {
                        redPosjeduje.Add(pretvorenaMatrica[i, stupac]);
                    }
                    if (pretvorenaMatrica[red, i] != 0)
                    {
                        stupacPosjeduje.Add(pretvorenaMatrica[red, i]);
                    }
                }
                //za svaki element kvadrata koji je razlicit od 0, dodaj ga listu
                for (int i = kvadratRed[0]; i < kvadratRed[2] + 1; i++)
                {
                    for (int j = kvadratStupac[0]; j < kvadratStupac[2] + 1; j++)
                    {
                        if (pretvorenaMatrica[i, j] != 0)
                        {
                            kvadratPosjeduje.Add(pretvorenaMatrica[i, j]);
                        }
                    }
                }
                // za spremanje svih mogucih brojeva koji se ne nalaze u retku, stupcu ili pak kvadratu
                string moguciBrojevi = "";
                for (int i = 1; i < velicina + 1; i++)
                {

                    if (!redPosjeduje.Contains(i) && !stupacPosjeduje.Contains(i) && !kvadratPosjeduje.Contains(i))
                    {
                        if (moguciBrojevi == "")
                        {
                            moguciBrojevi = i.ToString();
                        }
                        else
                        {
                            moguciBrojevi += " " + i.ToString();
                        }
                    }
                }
                pocetnaMatrica[red, stupac] = moguciBrojevi;
            }
        }
        public static void OtkriveniSingulariteti(string[,] matrica)
        {
            string[,] matr = new string[9, 9];
            bool promjena = true;
            for (int i = 1; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    matr[i, j] = String.Copy(matrica[i, j]);
                }
            }
            while (promjena)
            {
                MogucaRjesenja(matrica);
                int pr = 0;
                for (int i = 1; i < velicina; i++)
                {
                    for (int j = 0; j < velicina; j++)
                    {
                        if (matr[i, j] != matrica[i, j])
                        {
                            pr++;
                        }
                    }
                }
                if (pr > 0)
                {
                    for (int i = 1; i < velicina; i++)
                    {
                        for (int j = 0; j < velicina; j++)
                        {
                            matr[i, j] = String.Copy(matrica[i, j]);
                        }
                    }
                }
                else
                {
                    promjena = false;
                }
            }
        }
        public static void SkriveniSingulariteti(string[,] matrica)
        {
            OtkriveniSingulariteti(matrica);
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    List<int> skupinaRjesenja = new List<int>();
                    foreach (char p in matrica[i, j])
                    {
                        if (p != ' ')
                        {
                            skupinaRjesenja.Add(int.Parse(p.ToString()));
                        }
                    }
                    if (skupinaRjesenja.Count > 1)
                    {
                        List<int> nedozvoljeniS = new List<int>();
                        List<int> nedozvoljeniR = new List<int>();
                        List<int> nedozvoljeniK = new List<int>();
                        for (int k = 0; k < velicina; k++)
                        {
                            if (k != i)
                            {
                                foreach (char p in matrica[k, j])
                                {
                                    if (p != ' ')
                                    {
                                        nedozvoljeniS.Add(int.Parse(p.ToString()));
                                    }
                                }
                            }
                            if (k != j)
                            {
                                foreach (char p in matrica[i, k])
                                {
                                    if (p != ' ')
                                    {
                                        nedozvoljeniR.Add(int.Parse(p.ToString()));
                                    }
                                }
                            }
                        }
                        int[] kS = KvadratStupac(j);
                        int[] kR = KvadratRed(i);
                        for (int r = kR[0]; r <= kR[2]; r++)
                        {
                            for (int s = kS[0]; s <= kS[2]; s++)
                            {
                                if (i != r || j != s)
                                {
                                    foreach (char p in matrica[r, s])
                                    {
                                        if (p != ' ')
                                        {
                                            nedozvoljeniK.Add(int.Parse(p.ToString()));
                                        }
                                    }
                                }
                            }
                        }
                        string ga = "";
                        foreach (int p in skupinaRjesenja)
                        {
                            if (!nedozvoljeniS.Contains(p) || !nedozvoljeniR.Contains(p) || !nedozvoljeniK.Contains(p))
                            {
                                if (ga == "")
                                {
                                    ga = p.ToString();
                                }
                                else
                                {
                                    ga += " " + p.ToString();
                                }
                            }
                        }
                        matrica[i, j] = ga;
                        OtkriveniSingulariteti(matrica);
                    }
                }
            }
        }

        public static string s = "";
        private static void PronadiKombinacije(int[] niz, int[] podaci, int start, int kraj, int indeks, int velicinaKombinacije)
        {

            if (indeks == velicinaKombinacije)
            {
                for (int j = 0; j < velicinaKombinacije; j++)
                    s += (podaci[j]);
                s += " ";
                return;
            }
            for (int i = start; i <= kraj &&
                     kraj - i + 1 >= velicinaKombinacije - indeks; i++)
            {
                podaci[indeks] = niz[i];
                PronadiKombinacije(niz, podaci, i + 1, kraj, indeks + 1, velicinaKombinacije);
            }
        }
        public static void IspisKombinacija(int[] niz, int n, int velicinaKombinacije)
        {
            s = "";
            int[] podaci = new int[velicinaKombinacije];
            PronadiKombinacije(niz, podaci, 0, n - 1, 0, velicinaKombinacije);
        }

        public static void PreventivniSkupovi(string[,] matrica)
        {
            for (int i = 0; i < velicina; i++)
            {
                List<string> pozicijeS = new List<string>();
                for (int j = 0; j < velicina; j++)
                {
                    List<int> a = new List<int>();
                    foreach (char p in matrica[j, i])
                    {
                        if (p != ' ')
                        {
                            a.Add(int.Parse(p.ToString()));
                        }
                    }
                    if (a.Count > 1)
                    {

                        pozicijeS.Add(j.ToString() + i.ToString());
                    }
                }
                int[] niz1 = new int[pozicijeS.Count];
                for (int j = 0; j < niz1.Length; j++)
                {
                    niz1[j] = j;
                }
                foreach (int j in niz1)
                {
                    if (j > 1)
                    {
                        IspisKombinacija(niz1, niz1.Length, j);
                        string[] komb = s.Split(' ');

                        foreach (string km in komb)
                        {
                            if (km != "")
                            {
                                List<int> brojevi = new List<int>();
                                List<int> poz = new List<int>();
                                foreach (char p in km)
                                {
                                    poz.Add(int.Parse(p.ToString()));
                                }
                                foreach (int pe in poz)
                                {
                                    string pl = pozicijeS[pe];
                                    foreach (char kl in matrica[int.Parse(pl[0].ToString()), int.Parse(pl[1].ToString())])
                                    {

                                        if (kl != ' ')
                                        {
                                            if (!brojevi.Contains(int.Parse(kl.ToString())))
                                                brojevi.Add(int.Parse(kl.ToString()));
                                        }
                                    }
                                }
                                if (brojevi.Count == j)
                                {
                                    for (int ro = 0; ro < pozicijeS.Count; ro++)
                                    {
                                        if (!poz.Contains(ro))
                                        {
                                            string z = "";
                                            string be = pozicijeS[ro];
                                            foreach (char p in matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())])
                                            {
                                                if (p != ' ')
                                                {
                                                    if (!brojevi.Contains(int.Parse(p.ToString())))
                                                    {
                                                        if (z == "")
                                                        {
                                                            z += p.ToString();
                                                        }
                                                        else
                                                        {
                                                            z += " " + p.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                            matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())] = z;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            for (int i = 0; i < velicina; i++)
            {
                List<string> pozicijeS = new List<string>();
                for (int j = 0; j < velicina; j++)
                {
                    List<int> a = new List<int>();
                    foreach (char p in matrica[i, j])
                    {
                        if (p != ' ')
                        {
                            a.Add(int.Parse(p.ToString()));
                        }
                    }
                    if (a.Count > 1)
                    {

                        pozicijeS.Add(i.ToString() + j.ToString());
                    }
                }
                int[] niz1 = new int[pozicijeS.Count];
                for (int j = 0; j < niz1.Length; j++)
                {
                    niz1[j] = j;
                }
                foreach (int j in niz1)
                {
                    if (j > 1)
                    {
                        IspisKombinacija(niz1, niz1.Length, j);
                        string[] komb = s.Split(' ');

                        foreach (string km in komb)
                        {
                            if (km != "")
                            {
                                List<int> brojevi = new List<int>();
                                List<int> poz = new List<int>();
                                foreach (char p in km)
                                {
                                    poz.Add(int.Parse(p.ToString()));
                                }
                                foreach (int pe in poz)
                                {
                                    string pl = pozicijeS[pe];
                                    foreach (char kl in matrica[int.Parse(pl[0].ToString()), int.Parse(pl[1].ToString())])
                                    {

                                        if (kl != ' ')
                                        {
                                            if (!brojevi.Contains(int.Parse(kl.ToString())))
                                                brojevi.Add(int.Parse(kl.ToString()));
                                        }
                                    }
                                }
                                if (brojevi.Count == j)
                                {
                                    for (int ro = 0; ro < pozicijeS.Count; ro++)
                                    {
                                        if (!poz.Contains(ro))
                                        {
                                            string z = "";
                                            string be = pozicijeS[ro];
                                            foreach (char p in matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())])
                                            {
                                                if (p != ' ')
                                                {
                                                    if (!brojevi.Contains(int.Parse(p.ToString())))
                                                    {
                                                        if (z == "")
                                                        {
                                                            z += p.ToString();
                                                        }
                                                        else
                                                        {
                                                            z += " " + p.ToString();
                                                        }
                                                    }
                                                }
                                            }
                                            matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())] = z;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int[] kvadratRed_ = new int[3];
                    int[] kvadratStupac_ = new int[3];
                    if (i == 1)
                    {
                        kvadratRed_ = prviKvadrat;
                    }
                    else if (i == 2)
                    {
                        kvadratRed_ = drugiKvadrat;
                    }
                    else if (i == 3)
                    {
                        kvadratRed_ = treciKvadrat;
                    }
                    if (j == 1)
                    {
                        kvadratStupac_ = prviKvadrat;
                    }
                    else if (j == 2)
                    {
                        kvadratStupac_ = drugiKvadrat;
                    }
                    else if (j == 3)
                    {
                        kvadratStupac_ = treciKvadrat;
                    }
                    List<string> pozicije = new List<string>();
                    for (int k = kvadratRed_[0]; k <= kvadratRed_[2]; k++)
                    {
                        for (int l = kvadratStupac_[0]; l <= kvadratStupac_[2]; l++)
                        {
                            List<int> a = new List<int>();
                            foreach (char p in matrica[k, l])
                            {
                                if (p != ' ')
                                {
                                    a.Add(int.Parse(p.ToString()));
                                }
                            }
                            if (a.Count > 1)
                            {
                                pozicije.Add(k.ToString() + l.ToString());
                            }
                        }
                    }
                    int[] niz1 = new int[pozicije.Count];
                    for (int o = 0; o < niz1.Length; o++)
                    {
                        niz1[o] = o;
                    }
                    foreach (int o in niz1)
                    {
                        if (o > 1)
                        {
                            IspisKombinacija(niz1, niz1.Length, o);
                            string[] komb = s.Split(' ');

                            foreach (string km in komb)
                            {
                                if (km != "")
                                {
                                    List<int> brojevi = new List<int>();
                                    List<int> poz = new List<int>();
                                    //pozicije untar pozicijeS
                                    foreach (char p in km)
                                    {
                                        poz.Add(int.Parse(p.ToString()));
                                    }

                                    foreach (int pe in poz)
                                    {
                                        string pl = pozicije[pe];
                                        foreach (char kl in matrica[int.Parse(pl[0].ToString()), int.Parse(pl[1].ToString())])
                                        {
                                            if (kl != ' ')
                                            {
                                                if (!brojevi.Contains(int.Parse(kl.ToString())))
                                                    brojevi.Add(int.Parse(kl.ToString()));
                                            }
                                        }
                                    }
                                    if (brojevi.Count == o)
                                    {
                                        for (int ro = 0; ro < pozicije.Count; ro++)
                                        {
                                            if (!poz.Contains(ro))
                                            {
                                                string be = pozicije[ro];
                                                string z = "";
                                                foreach (char p in matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())])
                                                {
                                                    if (p != ' ')
                                                    {
                                                        if (!brojevi.Contains(int.Parse(p.ToString())))
                                                        {
                                                            if (z == "")
                                                            {
                                                                z = p.ToString();
                                                            }
                                                            else
                                                            {
                                                                z += " " + p.ToString();
                                                            }
                                                        }
                                                    }
                                                }
                                                matrica[int.Parse(be[0].ToString()), int.Parse(be[1].ToString())] = z;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        public static string[,] mat;
        public static string[,] Crooks(string[,] matrica)
        {
            string[,] matr = new string[9, 9];
            bool promjena = true;
            for (int i = 1; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    matr[i, j] = String.Copy(matrica[i, j]);
                }
            }
            while (promjena)
            {
                SkriveniSingulariteti(matrica);
                PreventivniSkupovi(matrica);
                int pr = 0;
                for (int i = 0; i < velicina; i++)
                {
                    for (int j = 0; j < velicina; j++)
                    {
                        if (matr[i, j] != matrica[i, j])
                        {
                            pr++;
                        }
                    }
                }
                if (pr > 0)
                {
                    for (int i = 0; i < velicina; i++)
                    {
                        for (int j = 0; j < velicina; j++)
                        {
                            matr[i, j] = String.Copy(matrica[i, j]);
                        }
                    }
                }
                else
                {
                    promjena = false;
                }
            }

            mat = matrica;
            int[,] mx = new int[9, 9];
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    if (matrica[i, j].Length == 1)
                    {
                        mx[i, j] = int.Parse(matrica[i, j]);
                    }
                    else
                    {
                        mx[i, j] = 0;
                    }
                }
            }
            Slucajni(mx, velicina);
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    matrica[i, j] = mx[i, j].ToString();
                }
            }
            return matrica;
        }

        public static bool Slucajni(int[,] matrica, int n)
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
            string[,] matr = new string[9, 9];
            for (int i = 0; i < velicina; i++)
            {
                for (int j = 0; j < velicina; j++)
                {
                    matr[i, j] = String.Copy(mat[i, j]);
                }
            }
            foreach (char p in mat[red, stupac])
            {
                if (p != ' ')
                {
                    if (Backtracking.Siguran(matrica, red, stupac, int.Parse(p.ToString())))
                    {
                        matrica[red, stupac] = int.Parse(p.ToString());
                        mat[red, stupac] = p.ToString();
                        SkriveniSingulariteti(mat);
                        PreventivniSkupovi(mat);
                        //rekurzivna promjena i dodavanje, ako ne ide, posatvi na 0
                        if (Slucajni(matrica, n))
                        {
                            return true;
                        }
                        else
                        {
                            for (int i = 0; i < velicina; i++)
                            {
                                for (int j = 0; j < velicina; j++)
                                {
                                    if (mat[i, j] != matr[i, j])
                                    {
                                        mat[i, j] = String.Copy(matr[i, j]);
                                    }
                                }
                            }
                            matrica[red, stupac] = 0;
                        }
                    }
                }
            }
            return false;
        }
    }
}
