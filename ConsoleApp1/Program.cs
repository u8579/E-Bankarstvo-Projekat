using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace E_Bankarstvo
{
    internal class Program
    {
        enum ValuteIme { USD, EUR, GBP, CHF, CAD }
        static double[] prodKurs = { 101.15, 117.75, 135.65, 128.75, 73.15 };
        static double[] kupKurs = { 100.5, 117.05, 134.85, 127.95, 72.85 };

        static void Uplata(ref double din_racun)
        {
            string ponovo;
            do
            {
                bool valid;
                double iz;

                do
                {
                    Console.Write("{0,-3} {1}", "|", "Unesite iznos za uplatu na dinarski račun: ");
                    string iznos = Console.ReadLine();
                    valid = double.TryParse(iznos, out iz);
                    if (iz < 0)
                    {
                        valid = false;
                    }

                    if (!valid)
                    {
                        Console.WriteLine("{0,-3} {1}", "|", "Nije unet ispravan iznos, probajte ponovo.");
                    }

                } while (!valid);

                din_racun += iz;

                Console.WriteLine("\n--- Transakcija je uspešno izvršena! ---");
                Console.WriteLine("Novo stanje na dinarskom računu: {0} RSD", din_racun);
                Console.WriteLine("================================================");

                Console.Write("\nDa li želite da obavite još jednu uplatu na račun? (da/ne): ");
                ponovo = Console.ReadLine().Trim().ToLower();
            } while (ponovo == "da");
        }

        static void Menjacnica(ref double din_racun, double[] dev_racun)
        {
            string ponovo;
            do
            {
                Console.WriteLine();
                Console.WriteLine("================== MENJACNICA ==================");

                string izbor_str;
                int izbor = -1;
                Console.Write("{0,-3} {1}", "|", "Unesite željenu valutu (USD, EUR, GBP, CHF, CAD): ");
                izbor_str = Console.ReadLine().Trim().ToUpper();
                ValuteIme valuta;
                while (!Enum.TryParse<ValuteIme>(izbor_str, true, out valuta))
                {
                    Console.Write("{0,-3} {1}", "|", "Nepoznata valuta! Probajte ponovo: ");
                    izbor_str = Console.ReadLine().Trim().ToUpper();
                }

                izbor = (int)valuta;

                bool validkp;
                int izbor_kp;

                do
                {
                    Console.Write("{0,-3} {1}", "|", "Unesite 1 za prodaju, 2 za kupovinu valute: ");
                    validkp = int.TryParse(Console.ReadLine(), out izbor_kp) && (izbor_kp == 1 || izbor_kp == 2);

                    if (!validkp)
                    {
                        Console.WriteLine("{0,-3} {1}", "|", "Pogrešan izbor! Unesite ponovo.");
                    }

                } while (!validkp);

                if (izbor_kp == 1)
                {
                    bool validiz = true;
                    double iznos;

                    do
                    {
                        Console.Write("{0,-3} {1}", "|", "Unesite iznos za prodaju: ");

                        if (!double.TryParse(Console.ReadLine(), out iznos))
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Pogrešan unos! Unesite ponovo.");
                            validiz = false;
                        }
                        else if (iznos > dev_racun[izbor])
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Nedovoljan iznos na deviznom računu! Unesite ponovo.");
                            validiz = false;
                        }
                        else if(iznos < 0)
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Unet je negativan iznos! Unesite ponovo.");
                            validiz = false;
                        }
                        else
                        {
                            validiz = true;
                        }

                    } while (!validiz);
                    din_racun += iznos * prodKurs[izbor];
                    dev_racun[izbor] -= iznos;
                }
                else
                {
                    bool validiz = true;
                    double iznos;

                    do
                    {
                        Console.Write("{0,-3} {1}", "|", "Unesite iznos za kupovinu: ");

                        if (!double.TryParse(Console.ReadLine(), out iznos))
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Pogrešan unos! Unesite ponovo.");
                            validiz = false;
                        }
                        else if (din_racun < iznos * kupKurs[izbor])
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Nedovoljan iznos na dinarskom računu! Unesite ponovo.");
                            validiz = false;
                        }
                        else if (iznos < 0)
                        {
                            Console.WriteLine("{0,-3} {1}", "|", "Unet je negativan iznos! Unesite ponovo.");
                            validiz = false;
                        }
                        else
                        {
                            validiz = true;
                        }

                    } while (!validiz);
                    dev_racun[izbor] += iznos;
                    din_racun -= iznos * kupKurs[izbor];
                }

                Console.WriteLine("\n--- Transakcija je uspešno izvršena! ---");
                Console.WriteLine("Novo stanje na deviznom računu: {0} {1}", dev_racun[izbor], izbor_str);
                Console.WriteLine("Novo stanje na dinarskom računu: {0} RSD", din_racun);
                Console.WriteLine("================================================");

                Console.Write("\nDa li želite da obavite još jednu kupovinu/prodaju valuta? (da/ne): ");
                ponovo = Console.ReadLine().Trim().ToLower();
            } while (ponovo == "da");
        }

        static void Prenos(ref double din_racun)
        {
            string ponovo;
            do
            {
                Console.WriteLine();
                Console.WriteLine("=============== PRENOS SREDSTAVA ===============");

                Console.Write("Unesite naziv primaoca: ");
                string primalac = Console.ReadLine();

                Console.Write("Unesite mesto primaoca: ");
                string mesto = Console.ReadLine();

                Console.Write("Unesite broj računa primaoca: ");
                string brRacuna = Console.ReadLine();

                Console.Write("Unesite šifru plaćanja (253 ili 289): ");
                int sifra;
                while (!int.TryParse(Console.ReadLine(), out sifra) || (sifra != 253 && sifra != 289))
                {
                    Console.Write("Pogrešna šifra plaćanja! Unesite ponovo (253 ili 289): ");
                }

                Console.Write("Unesite model plaćanja: ");
                int model;
                while (!int.TryParse(Console.ReadLine(), out model) || (sifra == 253 && model != 97))
                {
                    if (sifra == 253)
                    {
                        Console.Write("Za šifru plaćanja 253 model mora biti 97! Unesite ponovo: ");
                    }
                    else
                    {
                        Console.Write("Pogrešan unos modela! Unesite ponovo: ");
                    }
                }
                Console.Write("Unesite poziv na broj: ");
                string poziv = Console.ReadLine();
                if (model == 97)
                {
                    while (string.IsNullOrWhiteSpace(poziv) || poziv.Length < 3)
                    {
                        Console.Write("Neispravan poziv na broj za model 97! Unesite ponovo: ");
                        poziv = Console.ReadLine();
                    }
                }
                Console.Write("Unesite iznos za prenos: ");
                double iznos = double.Parse(Console.ReadLine());
                double provizija = 20;
                if (iznos > 50000)
                {
                    provizija = iznos * 0.005;
                }
                else if (iznos == 0)
                {
                    provizija = 0;
                }

                while ((din_racun < (iznos + provizija)) || (iznos < 0))
                {
                    Console.WriteLine("\nGreška: Nedovoljno sredstava na računu za iznos i proviziju!/Unet negativan iznos!");
                    if (din_racun < (iznos + provizija))
                    {
                        Console.Write("Unesite manji iznos za prenos: ");
                    }
                    else
                    {
                        Console.Write("Unesite nenegativan iznos za prenos: ");
                    }
                    iznos = double.Parse(Console.ReadLine());
                    if (iznos > 50000)
                    {
                        provizija = iznos * 0.005;
                    }
                    else if (iznos < 50000 && iznos > 0)
                    {
                        provizija = 20;
                    }
                    else if (iznos == 0)
                    {
                        provizija = 0;
                        break;
                    }
                }
                din_racun -= (iznos + provizija);

                Console.WriteLine("\n--- Transakcija je uspešno izvršena! ---");
                Console.WriteLine("Kome: {0}, {1}", primalac, mesto);
                Console.WriteLine("Račun primaoca: {0}", brRacuna);
                Console.WriteLine("Iznos prenosa: {0} RSD", iznos);
                Console.WriteLine("Provizija banke: {0} RSD", provizija);
                Console.WriteLine("Ukupno skinuto sa računa: {0} RSD", iznos + provizija);
                Console.WriteLine("Novo stanje na dinarskom računu: {0} RSD", din_racun);
                Console.WriteLine("================================================");

                Console.Write("\nDa li želite da obavite još jedan prenos sredstava? (da/ne): ");
                ponovo = Console.ReadLine().Trim().ToLower();

            } while (ponovo == "da");
        }

        static void Pregled(double din_racun, double[] dev_racun)
        {
            Console.Write("Iznos na dinarskom računu: ");
            Console.WriteLine(din_racun + " RSD");
            Console.WriteLine("Iznos na deviznom računu: ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(dev_racun[i] + " " + (ValuteIme)i);
            }
        }

        static void Meni(ref double din_racun, double[] dev_racun)
        {
            int izbor;
            do
            {
                Console.WriteLine();
                Console.WriteLine("============MENI===========");
                Console.WriteLine("{0,-4} {1,-3} {2,4}", "|", "1. Pregled stanja", "|");
                Console.WriteLine("{0,-3} {1,-3} {2,3}", "|", "2. Uplata sredstava", "|");
                Console.WriteLine("{0,-6} {1,-3} {2,6}", "|", "3. Menjačnica", "|");
                Console.WriteLine("{0,-3} {1,-3} {2,3}", "|", "4. Prenos sredstava", "|");
                Console.WriteLine("{0,-8} {1,-3} {2,9}", "|", "5. Izlaz", "|");
                Console.WriteLine("===========================");
                Console.Write("{0,-3} {1}", "|", "Izbor: ");
                while (!int.TryParse(Console.ReadLine(), out izbor) ||
                       izbor < 1 || izbor > 5)
                {
                    Console.Write("{0,-3} {1}", "|", "Pogrešan unos. Probajte ponovo: ");
                }
                switch (izbor)
                {
                    case 1:
                        Pregled(din_racun, dev_racun);
                        break;

                    case 2:
                        Uplata(ref din_racun);
                        break;

                    case 3:
                        Menjacnica(ref din_racun, dev_racun);
                        break;

                    case 4:
                        Prenos(ref din_racun);
                        break;

                    case 5:
                        Console.WriteLine("Doviđenja!");
                        break;
                }
            } while (izbor != 5);
        }

        static void Main()
        {
            double din_racun = 0;
            double[] dev_racun = new double[5];
            Meni(ref din_racun, dev_racun);
        }
    }
}
