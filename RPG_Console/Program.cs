using System.Net.Http.Json;

namespace RPG_Console;

class Program
{
    static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5204") };

    static async Task Main(string[] args)
    {
        bool bezig = true;

        while (bezig)
        {
            ToonMenu();
            Console.Write("Kies een optie: ");
            string? keuze = Console.ReadLine();

            switch (keuze)
            {
                case "1":
                    NieuwSpel();
                    break;
                case "2":
                    await ToonSpelInformatie();
                    break;
                case "3":
                    await CheckStatus();
                    break;
                case "4":
                    bezig = false;
                    Console.WriteLine("\nBedankt voor het spelen! Tot ziens.");
                    break;
                default:
                    Console.WriteLine("\nOngeldige keuze, probeer opnieuw.");
                    break;
            }
        }
    }

    static void ToonMenu()
    {
        Console.WriteLine("\n=== Dungeon Crawler ===");
        Console.WriteLine("1. Nieuw spel");
        Console.WriteLine("2. Spelinformatie bekijken (API)");
        Console.WriteLine("3. API Status controleren");
        Console.WriteLine("4. Afsluiten");
    }

    static void NieuwSpel()
    {
        Console.Write("\nWat is je naam? ");
        string? naam = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(naam))
        {
            Console.WriteLine("Fout: De naam mag niet leeg zijn!");
            return;
        }

        Console.WriteLine($"Welkom {naam.Trim()}! Je avontuur in de kerker begint nu.");
    }

    static async Task CheckStatus()
    {
        try
        {
            var status = await client.GetFromJsonAsync<StatusDto>("/api/game/status");
            Console.WriteLine($"\n[API Response]: {status?.Message}");
        }
        catch
        {
            Console.WriteLine("\nFout: Kon geen verbinding maken met de API. Staat de backend wel aan?");
        }
    }

    static async Task ToonSpelInformatie()
    {
        try
        {
            var info = await client.GetFromJsonAsync<GameInfoDto>("/api/game/info");
            if (info != null)
            {
                Console.WriteLine("\n--- SPELINFORMATIE ---");
                Console.WriteLine($"Titel:         {info.Title}");
                Console.WriteLine($"Omschrijving:  {info.Description}");
                Console.WriteLine($"Aantal levels: {info.TotalLevels}");
            }
        }
        catch
        {
            Console.WriteLine("\nFout: Kon geen verbinding maken met de API. Staat de backend wel aan?");
        }
    }
}

// DTO's voor JSON communicatie
public record StatusDto(string Message);
public record GameInfoDto(string Title, string Description, int TotalLevels);
