using System.Text;

namespace FoldNyilvantarto;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            ShowMenu();
            Console.Write("Válassz egy menüpontot: ");

            string? input = Console.ReadLine();
            if (input is null)
            {
                break;
            }

            if (!int.TryParse(input, out int choice) || choice < 0 || choice > 8)
            {
                Console.WriteLine();
                Console.WriteLine("Érvénytelen menüpont!");
                Console.WriteLine("Kérlek, válassz egy számot 0 és 8 között.");
                WaitForEnter();
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine();
                    Console.WriteLine("Köszönjük, hogy a FöldNyilvántartó programot használtad!");
                    Console.WriteLine("Viszontlátásra!");
                    return;
                case 1:
                    ShowPlaceholder("Új földterület hozzáadása");
                    break;
                case 2:
                    ShowPlaceholder("Földterületek listázása");
                    break;
                case 3:
                    ShowPlaceholder("Földterület módosítása");
                    break;
                case 4:
                    ShowPlaceholder("Keresés növény szerint");
                    break;
                case 5:
                    ShowPlaceholder("Várható termés kiszámítása");
                    break;
                case 6:
                    ShowPlaceholder("Gazdasági statisztikák");
                    break;
                case 7:
                    ShowPlaceholder("Adatok mentése");
                    break;
                case 8:
                    ShowPlaceholder("Adatok betöltése");
                    break;
            }

            WaitForEnter();
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("           FÖLDNYILVÁNTARTÓ");
        Console.WriteLine("     Mezőgazdasági területek kezelése");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Console.WriteLine("1. Új földterület hozzáadása");
        Console.WriteLine("2. Földterületek listázása");
        Console.WriteLine("3. Földterület módosítása");
        Console.WriteLine("4. Keresés növény szerint");
        Console.WriteLine("5. Várható termés kiszámítása");
        Console.WriteLine("6. Gazdasági statisztikák");
        Console.WriteLine("7. Adatok mentése");
        Console.WriteLine("8. Adatok betöltése");
        Console.WriteLine("0. Kilépés");
        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
    }

    private static void ShowPlaceholder(string title)
    {
        Console.WriteLine();
        Console.WriteLine(title);
        Console.WriteLine();
        Console.WriteLine("Ez a funkció még nincs megvalósítva.");
    }

    private static void WaitForEnter()
    {
        Console.WriteLine();
        Console.WriteLine("Nyomj Entert a főmenühöz való visszatéréshez...");
        Console.ReadLine();
    }
}
