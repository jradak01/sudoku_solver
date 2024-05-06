using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_JR
{
    class RootObject : DataObject
    {
    }
    // pojedini čvor
    public class DataObject
    {
        public DataObject()
        {
            //cvor koji se nalazi povise
            Gore = this;
            //cvor koji se nalazi ispod
            Dolje = this;
            //cvor koji se nalazi desno
            Desno = this;
            //cvor koji se nalazi lijevo
            Lijevo = this;
            Zaglavlje = null;
            Red = 0;
            Stupac = 0;
            Vrijednost = 0;
        }

        public DataObject Gore { get; set; }
        public DataObject Dolje { get; set; }
        public DataObject Lijevo { get; set; }
        public DataObject Desno { get; set; }
        public ColumnObject Zaglavlje { get; set; }

        public int Red { get; set; }
        public int Stupac { get; set; }
        public int Vrijednost { get; set; }
        //povezivanje cvorova desno i lijevo
        public void DodajHorizontalniCvor(DataObject lijevo, DataObject desno)
        {
            Lijevo = lijevo;
            Desno = desno;

            desno.Lijevo = this;
            lijevo.Desno = this;
        }
        //povezivanje cvorova s prethodnim i iducim
        public void DodajVertikalniCvor(DataObject gore, DataObject dolje)
        {
            Gore = gore;
            Dolje = dolje;

            dolje.Gore = this;
            gore.Dolje = this;
        }
    }
    //cvor koji predstavlja zaglavlje
    public class ColumnObject : DataObject
    {
        public ColumnObject()
        {
            //stavi na 0 
            Velicina = default;
            Zaglavlje = this;
        }

        public ColumnObject(int velicina)
        {
            Velicina = velicina;
        }

        public int Velicina { get; set; }
    }
    public class Konstante
    {
        //normalna matrica
        public static int Velicina = 9;

        //u matrici velicine 9 može biti 9 vrijednosti, 9 redova po 9 kucica ,
        //9 stupaca po 9 kucica i 9 kvadrata velicine 9 -->sve skupa 81
        public static int Vrijednosti = Velicina;
        public static int Redovi = Velicina;
        public static int Stupci = Velicina;
        public static int Kvadrat = Velicina;

        //stupciMatrice-->324-->da isprovamo svaku mogucu varijantu 
        //redoviMatrice-->729-->imamo 9 redaka 9 stupaca i 9 vrijednosti
        public static int StupciMatrice = (Redovi * Stupci) + (Redovi * Vrijednosti) + 
            (Stupci * Vrijednosti) + (Kvadrat * Vrijednosti);
        public static int RedoviMatrice = Redovi * Stupci * Vrijednosti;
    }

    class DlxMatrix
    {
        public RootObject Korijen;
        //niz zaglavlja
        private ColumnObject[] zaglavljaStupaca;
        // matrica po kojoj tražimo
        private DataObject[,] matricaOgranicenja;
        //lista rješenja
        private List<DataObject> rjesenja;

        public DlxMatrix()
        {
            Inicijaliziraj();
        }
        public void Resetiraj()
        {
            Inicijaliziraj();
        }
        // postavi na pocetno
        private void Inicijaliziraj()
        {
            // postavi korijen kao novi objekt
            Korijen = new RootObject();
            //duzina niza zaglavlja stupaca je 324
            zaglavljaStupaca = new ColumnObject[Konstante.StupciMatrice];
            //matrica 324x729 ()
            matricaOgranicenja = new DataObject[Konstante.RedoviMatrice, Konstante.StupciMatrice];
            //skup podskupova rjesenja
            rjesenja = new List<DataObject>();
            // naseljava matricu pocetnim vrijednostima
            Naseli();
        }
        private void Naseli()
        {
            // za svaki stupac, od njih 324 u matrici, stvori zaglavlje
            for (int stupac = 0; stupac < Konstante.StupciMatrice; stupac++)
            {
                StvoriZaglavljaStupaca(stupac);
            }
            //za sve moguce kombinacije u Sudoku, postavi ogranicenja - za svako polje
            for (int red = 0; red < Konstante.Redovi; red++)
            {
                for (int stupac = 0; stupac < Konstante.Stupci; stupac++)
                {
                    for (int vrijednost = 0; vrijednost < Konstante.Vrijednosti; vrijednost++)
                    {
                        NapraviOgranicenja(red, stupac, vrijednost);
                    }
                }
            }
        }
        //ogranučenja za svako polje
        private void NapraviOgranicenja(int red, int stupac, int vrijednost)
        {
            int redMatrice = red * Konstante.Stupci * Konstante.Vrijednosti + stupac * Konstante.Vrijednosti + vrijednost;
            int[] stupciMatrice = new int[] {
                (Konstante.Redovi * red + stupac),
                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * red + vrijednost),
                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * Konstante.Vrijednosti) + (Konstante.Stupci * stupac + vrijednost),
                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * Konstante.Vrijednosti) + (Konstante.Stupci * Konstante.Vrijednosti) + ((3 * (red / 3) + (stupac / 3)) * 9 + vrijednost)
            };
            NapraviOgranicenja(redMatrice, stupciMatrice, red, stupac, vrijednost);
        }

        private void NapraviOgranicenja(int redMatrice, int[] stupciMatrice, int red, int stupac, int vrijednost)
        {
            DataObject lijevi = null;

            foreach (int stupacMatrice in stupciMatrice)
            {
                //cvor s podacima o redu, stupcu u kojem se nalzi i vrijednoscu
                DataObject cvor = new DataObject { Red = red, Stupac = stupac, Vrijednost = ++vrijednost };
                // ako lijevi nije  vec popunjen
                if (lijevi == null)
                {
                    lijevi = cvor;
                }

                ColumnObject gore = zaglavljaStupaca[stupacMatrice];
                //dodaj cvor u matricu ogranicenja
                matricaOgranicenja[redMatrice, stupacMatrice] = cvor;
                //povezi kako bi dobili punu povezanost
                UnesiDuplePovezaneCvorove(lijevi, gore, cvor);
            }
        }
        // stvaranje zaglavlja, dodavanje u listu zaglavlja te povezivanje s ostalim zaglavljima
        private void StvoriZaglavljaStupaca(int stupac)
        {
            zaglavljaStupaca[stupac] = new ColumnObject(stupac) { Stupac = stupac };
            zaglavljaStupaca[stupac].DodajHorizontalniCvor(Korijen.Lijevo, Korijen);
        }
        // umetni dvostruko povezane cvorove
        private void UnesiDuplePovezaneCvorove(DataObject lijevi, ColumnObject gore, DataObject cvor)
        {
            cvor.DodajHorizontalniCvor(lijevi.Lijevo, lijevi);
            cvor.DodajVertikalniCvor(gore.Gore, gore);

            cvor.Zaglavlje = gore;
            cvor.Zaglavlje.Velicina++;
        }

        //za svaki red ili stupac vrati cvorove koji je razlicit od pocetnog
        private IEnumerable<DataObject> NabrojiCvorove(DataObject pocetak, Func<DataObject, DataObject> funkcija)
        {
            //funkcija prima dataobject i vraca dataobject
            DataObject cvor = pocetak;
            while ((cvor = funkcija(cvor)) != pocetak)
            {
                //vrati svako rješenje jednom u vremenu
                yield return cvor;
            }
        }
        // trazi Exact Cover
        public int Trazi(int dubina, Rjesenje rjesenje)
        {
            //ako nema više vrati dubinu
            if (Korijen.Desno == Korijen)
            {
                Naseli(rjesenje);
                return dubina;
            }
            // nadi stupac od kojeg kreces (najmanji)
            ColumnObject istrazivackiStupac = PribaviIstrazivackiStupac();
            //pokrij odabrani stupac
            Pokrij(istrazivackiStupac);
            //iz funkcije enumerate
            foreach (DataObject red in NabrojiCvorove(istrazivackiStupac, objekt => objekt.Dolje))
            {
                rjesenja.Add(red);
                foreach (DataObject stupac in NabrojiCvorove(red, objekt => objekt.Desno))
                {
                    Pokrij(stupac.Zaglavlje);
                }
                //otiđi na iduci level rekurzivno
                int konacnaDubina = Trazi(dubina + 1, rjesenje);
                // ako rješenje nije moguce, vrati se i otkrij
                // i makni odabrani red iz rjesenja
                if (Korijen.Desno == Korijen)
                {
                    return konacnaDubina;
                }

                rjesenja.Remove(red);
                istrazivackiStupac = red.Zaglavlje;
                foreach (DataObject stupac in NabrojiCvorove(red, o => o.Lijevo))
                {
                    Otkrij(stupac.Zaglavlje);
                }
            }
            Otkrij(istrazivackiStupac);
            return dubina;
        }
        //trazenje stupca s najmanjom velicinom
        private ColumnObject PribaviIstrazivackiStupac()
        {
            int maksimalnaVelicina = int.MaxValue;
            ColumnObject Objekt = null;

            foreach (ColumnObject stupac in NabrojiCvorove(Korijen, objekt => objekt.Desno))
            {
                if (stupac.Velicina < maksimalnaVelicina)
                {
                    maksimalnaVelicina = stupac.Velicina;
                    Objekt = stupac;
                }
            }

            return Objekt;
        }
        //pokrivanje-->odspajanje stupaca
        private void Pokrij(DataObject zaglavljeStupca)
        {
            zaglavljeStupca.Desno.Lijevo = zaglavljeStupca.Lijevo;
            zaglavljeStupca.Lijevo.Desno = zaglavljeStupca.Desno;

            foreach (DataObject red in NabrojiCvorove(zaglavljeStupca, objekt => objekt.Dolje))
            {
                foreach (DataObject stupac in NabrojiCvorove(red, objekt => objekt.Desno))
                {
                    stupac.Dolje.Gore = stupac.Gore;
                    stupac.Gore.Dolje = stupac.Dolje;
                    stupac.Zaglavlje.Velicina--;
                }
            }
        }
        //otkrivanje-->vracanje stupaca
        private void Otkrij(DataObject zaglavljeStupca)
        {
            foreach (DataObject red in NabrojiCvorove(zaglavljeStupca, objekt => objekt.Gore))
            {
                foreach (DataObject stupac in NabrojiCvorove(red, objekt => objekt.Lijevo))
                {
                    stupac.Dolje.Gore = stupac;
                    stupac.Gore.Dolje = stupac;
                    stupac.Zaglavlje.Velicina++;
                }
            }

            zaglavljeStupca.Desno.Lijevo = zaglavljeStupca;
            zaglavljeStupca.Lijevo.Desno = zaglavljeStupca;
        }
        //ucitavanje matrice
        public void UcitajMatricu(int[,] matrica)
        {
            for (int red = 0; red < Konstante.Redovi; red++)
            {
                for (int stupac = 0; stupac < Konstante.Stupci; stupac++)
                {
                    //uzmi vrijednost svakog polja Sudoku-a
                    int vrijednost = matrica[red, stupac];
                    //ako je vrijednost polja veca od 0
                    if (vrijednost > 0)
                    {
                        //pozicije stupaca u kojem se nalazi
                        int[] indeksiStupaca = new int[] {
                                (Konstante.Redovi * red + stupac),
                                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * red + vrijednost - 1),
                                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * Konstante.Vrijednosti) +
                                (Konstante.Stupci * stupac + vrijednost - 1),
                                (Konstante.Redovi * Konstante.Stupci) + (Konstante.Redovi * Konstante.Vrijednosti) +
                                (Konstante.Stupci * Konstante.Vrijednosti) + ((3 * (red / 3) + (stupac / 3)) * 9 + vrijednost - 1)
                            };
                        //u rjesenja dodaj odgovarajuće polje iz matrice u koja upisujemo ogranicenja
                        rjesenja.Add(matricaOgranicenja[(red * Konstante.Stupci * Konstante.Vrijednosti +
                            stupac * Konstante.Vrijednosti + vrijednost - 1), Konstante.Redovi * red + stupac]);
                        //"odspoji" stupce koji suvec ponudeni
                        foreach (var indeksStupca in indeksiStupaca)
                        {
                            Pokrij(zaglavljaStupaca[indeksStupca]);
                        }

                    }
                }
            }
        }
        //popuni matricu
        private void Naseli(Rjesenje rjesenje)
        {
            foreach (DataObject Object in rjesenja)
            {
                rjesenje.Matrica[Object.Red, Object.Stupac] = Object.Vrijednost;
            }
        }
    }
    //kreiraj matricu za rješenje
    public class Rjesenje
    {
        public int[,] Matrica { get; set; } = new int[Konstante.Redovi, Konstante.Stupci];
    }
}
