using System.Globalization;
using System.Text;
using FoldNyilvantarto.Models;
using FoldNyilvantarto.Services;

namespace FoldNyilvantarto;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        FieldService fieldService = new FieldService();

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
                    if (!AddField(fieldService))
                    {
                        return;
                    }
                    break;
                case 2:
                    ShowAllFields(fieldService);
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

    private static void ShowAllFields(FieldService fieldService)
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("          FÖLDTERÜLETEK LISTÁJA");
        Console.WriteLine("========================================");
        Console.WriteLine();

        IReadOnlyList<Field> fields = fieldService.GetAllFields();
        if (fields.Count == 0)
        {
            Console.WriteLine("Jelenleg nincs nyilvántartott földterület.");
            return;
        }

        Console.WriteLine($"Nyilvántartott földterületek száma: {fields.Count}");
        Console.WriteLine();
        CultureInfo hungarianCulture = CultureInfo.GetCultureInfo("hu-HU");
        foreach (Field field in fields)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Azonosító: {field.Id}");
            Console.WriteLine($"Név: {field.Name}");
            Console.WriteLine($"Terület: {field.AreaHectares.ToString(hungarianCulture)} hektár");
            Console.WriteLine($"Termesztett növény: {field.CropType}");
            Console.WriteLine($"Várható hozam: {field.ExpectedYieldPerHectare.ToString(hungarianCulture)} tonna/hektár");
            Console.WriteLine($"Művelési állapot: {field.Status}");
        }
        Console.WriteLine("----------------------------------------");
    }

    private static bool AddField(FieldService fieldService)
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("       ÚJ FÖLDTERÜLET HOZZÁADÁSA");
        Console.WriteLine("========================================");
        Console.WriteLine();

        string? name = ReadRequiredText("Földterület neve: ");
        if (name is null) return false;

        double? area = ReadNumber("Terület mérete (hektár): ", false);
        if (area is null) return false;

        string? cropType = ReadRequiredText("Termesztett növény: ");
        if (cropType is null) return false;

        double? expectedYield = ReadNumber("Várható terméshozam (tonna/hektár): ", true);
        if (expectedYield is null) return false;

        string? status = ReadRequiredText("Művelési állapot: ");
        if (status is null) return false;

        Field field = new Field(fieldService.GetNextId(), name, area.Value, cropType, expectedYield.Value, status);
        fieldService.AddField(field);

        Console.WriteLine();
        Console.WriteLine("A földterület sikeresen hozzáadva!");
        Console.WriteLine();
        Console.WriteLine($"Azonosító: {field.Id}");
        Console.WriteLine($"Név: {field.Name}");
        Console.WriteLine($"Terület: {field.AreaHectares} hektár");
        Console.WriteLine($"Növény: {field.CropType}");
        Console.WriteLine($"Várható hozam: {field.ExpectedYieldPerHectare} tonna/hektár");
        Console.WriteLine($"Állapot: {field.Status}");
        return true;
    }

    private static string? ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (input is null)
            {
                Console.WriteLine("\nA bevitel megszakadt; a földterület nem lett hozzáadva.");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Az érték nem lehet üres. Kérlek, próbáld újra!");
        }
    }

    private static double? ReadNumber(string prompt, bool allowZero)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (input is null)
            {
                Console.WriteLine("\nA bevitel megszakadt; a földterület nem lett hozzáadva.");
                return null;
            }

            bool parsed = double.TryParse(input, NumberStyles.Float, CultureInfo.GetCultureInfo("hu-HU"), out double value)
                || double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

            if (parsed && double.IsFinite(value) && (allowZero ? value >= 0 : value > 0))
            {
                return value;
            }

            Console.WriteLine(allowZero
                ? "Kérlek, adj meg egy nem negatív, véges számot!"
                : "Kérlek, adj meg egy pozitív, véges számot!");
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
