using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autentikacio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                /*Console.WriteLine(Console.WindowWidth+" x "+Console.WindowHeight);
                for (int db = 1; db <= 8; db++)
                {
                    for (int i = 0; i < 10; i++) Console.Write(i);
                }*/
                Console.SetCursorPosition(50, 12);
                Console.WriteLine("1. Regisztráció");
                Console.SetCursorPosition(50, 13);
                Console.WriteLine("2. Bejelentkezés");
                Console.SetCursorPosition(50, 15);
                Console.WriteLine("X. Kilépés");
                Console.SetCursorPosition(50, 16);
                Console.Write("Kérem válasszon: ");
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.KeyChar == 'x' || cki.KeyChar == 'X')
                {
                    Console.WriteLine((byte)cki.KeyChar);
                    //a latin vagy angol ABC nagybetűi az ASCII kódrendszerben 65-től 90-ig terjednek, a kisbetűk pedig 32-vel arrébb 97-től 122-ig terjednek
                    //Az ASCII 1 byte-os kódrendszer, azonban a nemzeti sajátosságok érdekében sokkal inkább jellemző a 2 byte-os Unicode kódolás
                    Environment.Exit(0);
                    //itt a 0 jelentheti azt, hogy a programunk hibamentesen fejeződött be
                }
                switch (cki.KeyChar)
                {
                    case '1':
                        //static minősítésű tartalmak példányosítás nélkül lesznek használhatók, amiknél viszont nincs ez a minősítés, azok csak a new művelettel történő példányosítást követően lesznek használhatók
                        //private láthatósági minősítés azt írja elő, hogy a tartalom: ADAT vagy KÓD, csak az adott osztályból lesz hozzáférhető
                        //public más osztályokból történő hozzáférést biztosít
                        //protected pedig az ősben megjelenő ilyen minősítésű tartalmat az utódban is elérhetőként jelöl
                        //ha a fentiek egyike sem szerepel, akkor private-ként értelmezhetjük
                        Regisztracio();
                        break;
                    case '2':
                        //az alábbiak csak akkor kellenének, ha a Bejelentkezes metódus nem statikus, hanem dinamikus volna
                        /*Program p=new Program();
                        p.Bejelentkezes();*/
                        //(new Program()).Bejelentkezes();
                        EvBekeres();
                        break;
                    default:
                        //se nem 1 se nem 2 a leütött karakter
                        break;
                }
            }
        }
        private static void Regisztracio()
        {
            Console.Clear();
            Console.SetCursorPosition(19, 0);
            Console.Write("Név:");
            Console.SetCursorPosition(0, 1);
            Console.Write("Felhasználói azonosító:");
            Console.SetCursorPosition(7, 2);
            Console.Write("Születési dátum:");
            Console.SetCursorPosition(12, 3);
            Console.Write("E-mail cím:");
            Console.SetCursorPosition(14, 4);
            Console.Write("Jelszó 1:");
            Console.SetCursorPosition(14, 5);
            Console.Write("Jelszó 2:");
            //Console.SetCursorPosition(24, 0);
            //string nev = Console.ReadLine();

            string nev="";
            while(nev.Length <5)
            {
                Console.SetCursorPosition(24, 0);
                nev = Console.ReadLine();

                if(nev.Length <= 5)
                {
                    Console.SetCursorPosition(44,0);
                    Console.Write("Túl rövid nevet adtál meg!");
                }
            }
            for(int i = 44; i <= 69; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(' ');
            }
            //Console.SetCursorPosition(24, 1);
            //string felhasznalo = Console.ReadLine();
            string felhasznalo = "";
            while(felhasznalo.Length == 0)
            {
                Console.SetCursorPosition(24, 1);
                felhasznalo = Console.ReadLine();
                if(felhasznalo.Length == 0)
                {
                    Console.SetCursorPosition(44, 1);
                    Console.Write("Kötelező kitőlteni!");
                }
            }
            for(int i = 44; i <= 62; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write(' ');
            }
            Console.SetCursorPosition(24, 2);
            DateTime datum = EvBekeres();
            // Console.WriteLine(datum.ToString());
            Console.SetCursorPosition(24, 3);
            string email;
            int kukacdb = 0;
            while (true)
            {
                
                Console.SetCursorPosition(24, 3);
                email = Console.ReadLine();
                foreach(char kar in email)
                {
                    if(kar == '@')
                    {
                        kukacdb++;
                    }
                }
                if(kukacdb > 1 || kukacdb == 0)
                {

                }
                
                if (email.Contains("@") && email.Contains(".") && !email.Contains(" ") && email.Length >= 3)
                {
                    for (int i = 45; i <= 118; i++)
                    {
                        Console.SetCursorPosition(i, 3);
                        Console.Write(' ');
                    }

                    break;
                }
                else
                {
                    Console.SetCursorPosition(45, 3);
                    Console.Write("Az E-mail címnek tartalmaznia kell @ és . jelet és nem lehet benne szóköz!");
                    for (int i = 24; i < 44; i++)
                    {

                        Console.SetCursorPosition(i, 3);
                        Console.Write(" ");
                    }
                }
            }


            /*           Console.SetCursorPosition(24, 3);
                       string jelszo1 = "";
                       do
                       {
                           char c = Console.ReadKey(true).KeyChar;
                           if (c == '\r') break; //különböző operációs rendszerek más-más módon viszonyulnak az Enter leütéshez
                           //Windows: 13+10 ASCII kódú karaktereket
                           //Linux/UNIX: csak 10 ASCII kódú karaktert
                           //MacOSX: pedig csak 13 ASCII kódú karaktert
                           //ASCII 10 new line \n
                           //ASCII 13 caridge return \r
                           Console.Write("*");
                           jelszo1 += c;
                       }while(true);
                       Console.SetCursorPosition(24, 4);
                       string jelszo2 = "";
                       do
                       {
                           char c = Console.ReadKey(true).KeyChar;
                           if (c == '\r') break; 
                           Console.Write("*");
                           jelszo2 += c;
                       } while (true);*/
            string jelszo1 = Jelszobekeres(4);
            string jelszo2 = Jelszobekeres(5);
            while (jelszo1 != jelszo2)
            {
                // a már beírt jelszó csillagainak eltűntetése
                for(int i = 24; i <= 44; i++)
                {
                    Console.SetCursorPosition(i, 4);
                    Console.Write(" ");
                    Console.SetCursorPosition(i, 5);
                    Console.Write(" ");
                }
                
                Console.SetCursorPosition(45, 5);
                Console.Write("A két jelszó nem egyezik meg!");
                jelszo1 = Jelszobekeres(4);
                
                jelszo2 = Jelszobekeres(5);
            }
/*
            //A két jelszó nem egyezik meg szöveg eltűntetése
            for (int i = 45; i <= 73; i++)
            {
                Console.SetCursorPosition(i, 4);
                Console.Write(" ");
                Console.SetCursorPosition(i, 5);
                Console.Write(" ");

            }
*/
            StreamWriter felhasznalok = new StreamWriter("felhasznalok.txt");
            felhasznalok.WriteLine("Név:Felhasználónév:E-mail cím:Születésidátum:Jelszó");
            felhasznalok.WriteLine($"{nev}:{felhasznalo}:{email}:{datum.ToString("yyyy-MM-dd")}:{jelszo1}");
            felhasznalok.Close();

        }
        static void Bejelentkezes() { }
        static string Jelszobekeres(int sor)
        {
            int oszlop = 24;
            Console.SetCursorPosition(24, sor);
            string jelszo = "";

            while (true)
            {
                char c = Console.ReadKey(true).KeyChar;

                if (c != 8 && c != '\r' )
                {
                    Console.Write("*");
                    jelszo += c;
                    oszlop++;
                }
                else if (c == '\r') break;
                else if ( c == 8)
                {
                    jelszo = jelszo.Substring(0, jelszo.Length - 1);
                    oszlop--;
                    Console.SetCursorPosition(oszlop, sor);
                    Console.Write(" ");
                    Console.SetCursorPosition(oszlop, sor);
                }
            }

            /*
             
            do
            {
                char c = Console.ReadKey(true).KeyChar;
                if (c == '\r') break;
                Console.Write("*");
                jelszo += c;
            } while (true);
            */
            return jelszo;
        }

        static DateTime EvBekeres()
        {
            bool ismetel = true;
            DateTime datumKimeno;
            do
            {
                //Console.Clear();
                // Console.WriteLine("dátum bekérés:");
                string[] ev = new string[] { "", "", "", "", "-", "", "", "-", "", "", };
                Console.SetCursorPosition(28, 2);
                Console.Write(ev[4]);
                Console.SetCursorPosition(31, 2);
                Console.Write(ev[4]);
                string ev1Jegy = "";
                string ev2Jegy = "";
                string ev3Jegy = "";
                string ev4Jegy = "";
                string h1;
                string h2;
                string n1;
                string n2;

                //első számjegy
                while (true)
                {
                    ev1Jegy = Console.ReadKey(true).KeyChar.ToString();
                    if (ev1Jegy == "1" || ev1Jegy == "2")
                    {
                        Console.SetCursorPosition(24, 2);
                        Console.Write(ev1Jegy);
                        ev[0] = ev1Jegy;
                        break;

                    }
                }

                //második számjegy
                if (ev[0] == "1")
                {
                    while (true)
                    {
                        ev2Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev2Jegy == "9")
                        {
                            Console.SetCursorPosition(25, 2);
                            Console.Write(ev2Jegy);
                            ev[1] = ev2Jegy;
                            break;
                        }
                    }
                }

                else if (ev[0] == "2")
                {
                    while (true)
                    {
                        ev2Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev2Jegy == "0")
                        {
                            Console.SetCursorPosition(25, 2);
                            Console.Write(ev2Jegy);
                            ev[1] = ev2Jegy;
                            break;
                        }
                    }
                }

                //harmadik számjegy

                if (ev[0] == "1" && ev[1] == "9")
                {
                    while (true)
                    {
                        ev3Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev3Jegy == "0" || ev3Jegy == "1" ||
                            ev3Jegy == "2" || ev3Jegy == "3" ||
                            ev3Jegy == "4" || ev3Jegy == "5" ||
                            ev3Jegy == "6" || ev3Jegy == "7" ||
                            ev3Jegy == "8" || ev3Jegy == "9")
                        {
                            Console.SetCursorPosition(26, 2);
                            Console.Write(ev3Jegy);
                            ev[2] = ev3Jegy;
                            break;
                        }
                    }
                }
                else if (ev[0] == "2" && ev[1] == "0")
                {
                    while (true)
                    {
                        ev3Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev3Jegy == "0" || ev3Jegy == "1" ||
                            ev3Jegy == "2")
                        {
                            Console.SetCursorPosition(26, 2);
                            Console.Write(ev3Jegy);
                            ev[2] = ev3Jegy;
                            break;
                        }
                    }
                }

                //negyedik számjegy

                if (ev[0] == "1" && ev[1] == "9" || ev[2] == "0" || ev[2] == "1" || ev[2] == "2" ||
                    ev[2] == "3" && ev[2] == "4" || ev[2] == "5" ||
                    ev[2] == "6" && ev[2] == "7" || ev[2] == "8" || ev[2] == "9")
                {
                    while (true)
                    {
                        ev4Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev4Jegy == "0" || ev4Jegy == "1" ||
                            ev4Jegy == "2" || ev4Jegy == "3" ||
                            ev4Jegy == "4" || ev4Jegy == "5" ||
                            ev4Jegy == "6" || ev4Jegy == "7" ||
                            ev4Jegy == "8" || ev4Jegy == "9")
                        {
                            Console.SetCursorPosition(27, 2);
                            Console.Write(ev4Jegy);
                            ev[3] = ev4Jegy;
                            break;
                        }
                    }
                }
                else if (ev[0] == "2" && ev[1] == "0" && ev[2] == "0")
                {
                    while (true)
                    {
                        ev4Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev4Jegy == "0" || ev4Jegy == "1" ||
                            ev4Jegy == "2" || ev4Jegy == "3" ||
                            ev4Jegy == "4" || ev4Jegy == "5" ||
                            ev4Jegy == "6" || ev4Jegy == "7" ||
                            ev4Jegy == "8" || ev4Jegy == "9")
                        {
                            Console.SetCursorPosition(27, 2);
                            Console.Write(ev4Jegy);
                            ev[3] = ev4Jegy;
                            break;
                        }
                    }
                }
                else if (ev[0] == "2" && ev[1] == "0" && ev[2] == "1")
                {
                    while (true)
                    {
                        ev4Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev4Jegy == "0" || ev4Jegy == "1" ||
                            ev4Jegy == "2" || ev4Jegy == "3" ||
                            ev4Jegy == "4" || ev4Jegy == "5" ||
                            ev4Jegy == "6" || ev4Jegy == "7" ||
                            ev4Jegy == "8" || ev4Jegy == "9")
                        {
                            Console.SetCursorPosition(27, 2);
                            Console.Write(ev4Jegy);
                            ev[3] = ev4Jegy;
                            break;
                        }
                    }
                }
                else if (ev[0] == "2" && ev[1] == "0" && ev[2] == "2")
                {
                    while (true)
                    {
                        ev4Jegy = Console.ReadKey(true).KeyChar.ToString();
                        if (ev4Jegy == "0" || ev4Jegy == "1" ||
                            ev4Jegy == "2" || ev4Jegy == "3" ||
                            ev4Jegy == "4")
                        {
                            Console.SetCursorPosition(27, 2);
                            Console.Write(ev4Jegy);
                            ev[3] = ev4Jegy;
                            break;
                        }
                    }
                }

                // hónap első jegye

                while (true)
                {
                    h1 = Console.ReadKey(true).KeyChar.ToString();
                    if (h1 == "0" || h1 == "1")
                    {
                        Console.SetCursorPosition(29, 2);
                        Console.Write(h1);
                        ev[5] = h1;
                        break;
                    }

                }

                // hónap második jegye

                while (true)
                {
                    h2 = Console.ReadKey(true).KeyChar.ToString();
                    if (ev[5] == "0")
                    {
                        if (h2 == "1" || h2 == "2" || h2 == "3" || h2 == "4" ||
                            h2 == "5" || h2 == "6" || h2 == "7" || h2 == "8" || h2 == "9")
                        {
                            Console.SetCursorPosition(30, 2);
                            Console.Write(h2);
                            ev[6] = h2;
                            break;
                        }
                    }
                    else if (ev[5] == "1")
                    {
                        if (h2 == "0" || h2 == "1" || h2 == "2")
                        {
                            Console.SetCursorPosition(30, 2);
                            Console.Write(h2);
                            ev[6] = h2;
                            break;
                        }
                    }
                }

                //Console.WriteLine("haha");
                // nap 1 meghatározása
                ///01=31
                ///02
                ///03=31
                ///04=30
                ///05=31
                ///06=30
                ///07=31
                ///08=31
                ///09=30
                ///10=31
                ///11=30
                ///12=31


                while (true)
                {
                    n1 = Console.ReadKey(true).KeyChar.ToString();
                    if ((h1 + h2) == "01" || (h1 + h2) == "03" ||
                        (h1 + h2) == "04" || (h1 + h2) == "05" ||
                        (h1 + h2) == "06" || (h1 + h2) == "07" ||
                        (h1 + h2) == "08" || (h1 + h2) == "09" ||
                        (h1 + h2) == "10" || (h1 + h2) == "11" || (h1 + h2) == "12")
                    {
                        if (n1 == "0" || n1 == "1" || n1 == "2" || n1 == "3")
                        {
                            Console.SetCursorPosition(32, 2);
                            Console.Write(n1);
                            ev[8] = n1;
                            break;
                        }

                        // Console.WriteLine("hiba1");
                    }
                    else if ((h1 + h2) == "02")
                    {
                        if (n1 == "0" || n1 == "1" || n1 == "2")
                        {
                            Console.SetCursorPosition(32, 2);
                            Console.Write(n1);
                            ev[8] = n1;
                            break;
                        }
                    }
                    //Console.WriteLine("hiba1");
                }

                // n2 értéke

                while (true)
                {
                    n2 = Console.ReadKey(true).KeyChar.ToString();
                    /*if ((h1 + h2) == "01" || (h1 + h2) == "03" ||
                         (h1 + h2) == "05" ||
                         (h1 + h2) == "07" ||
                        (h1 + h2) == "08" ||
                        (h1 + h2) == "10" || (h1 + h2) == "12")*/
                    if ((h1 + h2) != "02")
                    {
                        if (n1 == "0")
                        {
                            if (n2 == "1" || n2 == "2" || n2 == "3" ||
                               n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                               n2 == "8" || n2 == "9")
                            {
                                Console.SetCursorPosition(33, 2);
                                Console.Write(n2);
                                ev[9] = n2;
                                break;
                            }
                        }


                        if (n1 == "1" || n1 == "2")
                            if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                n2 == "8" || n2 == "9")
                            {
                                Console.SetCursorPosition(33, 2);
                                Console.Write(n2);
                                ev[9] = n2;
                                break;
                            }

                        if (n1 == "3")
                        {

                            if ((h1 + h2) == "01" || (h1 + h2) == "03" ||
                                (h1 + h2) == "05" ||
                                (h1 + h2) == "07" ||
                                (h1 + h2) == "08" ||
                                (h1 + h2) == "10" || (h1 + h2) == "12")
                            {
                                if (n2 == "0" || n2 == "1")
                                {
                                    Console.SetCursorPosition(33, 2);
                                    Console.Write(n2);
                                    ev[9] = n2;
                                    break;
                                }
                            }

                            else if ((h1 + h2) == "04" || (h1 + h2) == "06" ||
                            (h1 + h2) == "09" || (h1 + h2) == "11")
                            {

                                if (n2 == "0")
                                {
                                    Console.SetCursorPosition(33, 2);
                                    Console.Write(n2);
                                    ev[9] = n2;
                                    break;
                                }
                            }
                        }
                    }
                    //február vizsgálata

                    else if ((h1 + h2) == "02")
                    {
                        // év vizsgálat
                        // ha osztható 4-gyel ==> szökőév
                        //ha osztható 4-gyel és 400zal ==> szökőév
                        //osztható 4-gyel és 400zal, de osztható 100-zal az nem szökőév

                        //szökőévmeghatározása

                        int ev1Int = Convert.ToInt32(ev1Jegy) * 1000;
                        int ev2Int = Convert.ToInt32(ev2Jegy) * 100;
                        int ev3Int = Convert.ToInt32(ev3Jegy) * 10;
                        int ev4Int = Convert.ToInt32(ev4Jegy);

                        int evStringbol = ev1Int + ev2Int + ev3Int + ev4Int;
                        //Console.WriteLine("február");

                        if (n1 == "0")
                        {
                            if (n2 == "1" || n2 == "2" || n2 == "3" ||
                               n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                               n2 == "8" || n2 == "9")
                            {
                                Console.SetCursorPosition(33, 2);
                                Console.Write(n2);
                                ev[9] = n2;
                                break;
                            }
                        }


                        if (n1 == "1")
                            if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                n2 == "8" || n2 == "9")
                            {
                                Console.SetCursorPosition(33, 2);
                                Console.Write(n2);
                                ev[9] = n2;
                                break;
                            }
                        if (n1 == "2")
                        {
                            // Console.SetCursorPosition(23, 8);
                            //Console.WriteLine(evStringbol);
                            //szökőév
                            if (evStringbol % 4 == 0)
                            {
                                if (evStringbol % 100 == 0 && evStringbol % 400 == 0)
                                {
                                    if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                        n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                        n2 == "8" || n2 == "9")
                                    {
                                        Console.SetCursorPosition(33, 2);
                                        Console.Write(n2);
                                        ev[9] = n2;

                                        break;
                                    }
                                }

                                else if (evStringbol % 100 == 0)
                                {
                                    if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                        n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                        n2 == "8")
                                    {
                                        Console.SetCursorPosition(33, 2);
                                        Console.Write(n2);
                                        ev[9] = n2;

                                        break;

                                    }
                                }
                            }
                            else if (evStringbol % 400 == 0)
                            {
                                if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                        n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                        n2 == "8" || n2 == "9")
                                {
                                    Console.SetCursorPosition(33, 2);
                                    Console.Write(n2);
                                    ev[9] = n2;

                                    break;
                                }
                            }
                            else
                            {
                                if (n2 == "0" || n2 == "1" || n2 == "2" || n2 == "3" ||
                                        n2 == "4" || n2 == "5" || n2 == "6" || n2 == "7" ||
                                        n2 == "8")
                                {
                                    Console.SetCursorPosition(33, 2);
                                    Console.Write(n2);
                                    ev[9] = n2;

                                    break;

                                }
                            }
                        }







                    }


                }

                DateTime datum = new DateTime(Convert.ToInt32((ev1Jegy + ev2Jegy + ev3Jegy + ev4Jegy)), Convert.ToInt32(h1 + h2), Convert.ToInt32(n1 + n2));
                
                if (datum < DateTime.Now)
                {
                    ismetel = false;
                }
                datumKimeno = datum;

               

            } while(ismetel == true);
            return datumKimeno;
            

        }
    }
}
