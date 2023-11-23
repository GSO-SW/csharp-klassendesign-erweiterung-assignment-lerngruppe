/// <summary>
/// SAS Klassendesign Erweiterung
/// Lernsituation 1 - Aufgabe 3
/// Banking - App
/// </summary>
/// 




Bank online_bank = new Bank();
online_bank.MenueAufruf();


public class Bank
{
    private List<Bankkonto> konto_liste = new List<Bankkonto>();
    private Bankkonto aktives_konto = new Bankkonto();
    private double gebuehren = 0.01;

    public void MenueAufruf()
    {
        while (true)
        {
            if (Environment.GetEnvironmentVariable("IsTesting") != "true")
            {
                Console.Clear();
            }

            Console.WriteLine($"Ihre Kontonummer: {aktives_konto.kontonummer}");
            Console.WriteLine("\nWählen Sie eine Option:\n1. Konto erstellen\n2. Konto auswählen\n3. Einzahlen\n4. Abheben\n5. Kontostand anzeigen\n6. Überweisen\n7. Dispo Einrichten\n8. Beenden");
            string option = Console.ReadLine();

            if ((option == "3" || option == "4" || option == "5") && aktives_konto.kontonummer == 0)
            {
                Console.WriteLine("Bitte Konto wählen oder eins erstellen.");
                Console.ReadLine();
                option = "";
            }
            else
            {
                switch (option)
                {
                    case "1":
                        Bankkonto konto_neu = new Bankkonto();
                        Console.WriteLine("Kontonummer für neues Konto eingeben:");
                        konto_neu.kontonummer = Convert.ToInt32(Console.ReadLine());
                        konto_liste.Add(konto_neu);
                        aktives_konto = konto_neu;
                        break;

                    case "2":
                        Console.WriteLine("Geben Sie die Kontonummer ihres Kontos an:");
                        int kontonummer = Convert.ToInt32(Console.ReadLine());

                        foreach (Bankkonto bankkonto in konto_liste)
                        {
                            if (bankkonto.kontonummer == kontonummer)
                            {
                                aktives_konto = bankkonto;
                                Console.WriteLine("Konto gefunden...");
                                Console.ReadLine();
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Einzahlungsbetrag:");
                        double einzahlbetrag = double.Parse(Console.ReadLine());
                        aktives_konto.Einzahlung(einzahlbetrag);
                        break;

                    case "4":
                        Console.WriteLine("Abhebungsbetrag:");
                        double abhebebetrag = double.Parse(Console.ReadLine());

                        aktives_konto.Auszahlung(abhebebetrag);

                        break;

                    case "5":
                        aktives_konto.Kontostand();
                        Console.ReadLine();
                        break;
                    case "6":
                        Ueberweisung();
                        break;

                    case "7":
                        DispoEinrichten();
                        break;
                    case "8":
                        Console.WriteLine("Auf Wiedersehen!");
                        Console.ReadLine();
                        return;

                    default:
                        Console.WriteLine("Ungültige Option!");
                        Console.ReadLine();
                        break;
                }

            }
        }
    }

    //Erstellen Sie hier eine Überweisungsmehtode
    private void Ueberweisung()
    {
        Bankkonto empfaenger = null;
        bool flag;

        do
        {
            flag = false;
            Console.Clear();

            Console.WriteLine("Geben Sie die Kontonummer des Zahlungsenpfängers an:");
            int empfaenger_kn = Convert.ToInt32(Console.ReadLine());

            foreach (Bankkonto bankkonto in konto_liste)
            {
                if (bankkonto.kontonummer == empfaenger_kn)
                {
                    empfaenger = bankkonto;
                }
            }
            if (empfaenger == null)
            {
                Console.WriteLine("Konto nicht gefunden");
                Console.ReadLine();

                Console.WriteLine("Erneut Versuchen(Y/N)?");
                string eingabe = Console.ReadLine();
                if (eingabe == "Y")
                {
                    flag = true;
                }
            }

            else
            {
                Console.WriteLine("Geben Sie die den Überweisungsbetrag an:");
                double betrag = Convert.ToDouble(Console.ReadLine());

                double betrag_gesamt = betrag + (betrag * gebuehren);

                if (aktives_konto.Auszahlung(betrag_gesamt))
                {
                    empfaenger.Einzahlung(betrag);

                    aktives_konto.transaktionen.Add($"{"Auszahlung",15}{"Kontonummer:"}{"-"+betrag_gesamt+"EUR",15}");
                    empfaenger.transaktionen.Add($"{"Einzahlung",15}{"Kontonummer:"}{"+" + betrag + "EUR",15}");
                    Console.WriteLine("Überweisung Erfolgreich");
                    Console.ReadLine();
                }

            }
        } while (flag);


    }

    private void DispoEinrichten()
    {
        Console.WriteLine("Wie hoch soll Ihr Dispo ein?");
        double dispo = Convert.ToDouble(Console.ReadLine());

        aktives_konto.dispo = dispo;
    }
}
public class Bankkonto
{
    #region Klassenfelder
    public double kontostand { get; set; } = 0;
    private int _kontonummer;
    public int kontonummer
    {
        get { return _kontonummer; }
        set
        {
            if (value >= 0)
                _kontonummer = value;
            else
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Fehler: Negativer Wert!");
            }

        }
    }

    public double dispo = 0;

    public List<string> transaktionen = new List<string>();
    #endregion

    #region Konstruktoren

    #endregion


    #region Klassenmethoden
    public void Einzahlung(double einzahlbetrag)
    {
        kontostand += einzahlbetrag;
    }

    public bool Auszahlung(double abhebebetrag)
    {
        if (kontostand + dispo < abhebebetrag)
        {
            Console.WriteLine("Guthaben reicht nicht aus");
            Console.ReadLine();
            return false;
        }
        else
        {
            kontostand -= abhebebetrag;
            return true;
        }
    }

    public void Kontostand()
    {
        Console.WriteLine($"-----------------------\nIhr Kontostand:{kontostand}EUR");
        Console.ReadLine();
    }
    
    public void TransactionenAnzeigen()
    {
        foreach (string s in transaktionen)
        {
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
    
    #endregion

    #region Statische Methoden
    #endregion
}