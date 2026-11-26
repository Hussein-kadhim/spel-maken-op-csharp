using System.Net.Http.Json;

namespace RPG_Console;

class Program
{
    static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5204") };

    static async Task Main(string[] args)
    {
        // Haal bij het opstarten de status op van de API
        try
        {
            var status = await client.GetFromJsonAsync<StatusDto>("/api/game/status");
            Console.WriteLine($"[Server status: {status?.Message}]");
        }
        catch
        {
            Console.WriteLine("[Server status: Offline / niet bereikbaar]");
        }

        bool bezig = true;

        while (bezig)
        {
            ToonMenu();
            Console.Write("Kies een optie: ");
            string? keuze = Console.ReadLine();

            switch (keuze)
            {
                case "1":
                    await NieuwSpel();
                    break;
                case "2":
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
        Console.WriteLine("2. Afsluiten");
    }

    static async Task NieuwSpel()
    {
        Console.Write("\nWat is je naam? ");
        string? naam = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(naam))
        {
            Console.WriteLine("Fout: De naam mag niet leeg zijn!");
            return;
        }

        Console.WriteLine($"\nWelkom, {naam.Trim()}!");

        // Spelinformatie ophalen van de API en tonen
        try
        {
            var info = await client.GetFromJsonAsync<GameInfoDto>("/api/game/info");
            if (info != null)
            {
                Console.WriteLine($"\n--- {info.Title.ToUpper()} ---");
                Console.WriteLine($"Omschrijving:  {info.Description}");
                Console.WriteLine($"Aantal levels: {info.TotalLevels}");
                Console.WriteLine("Je reis door de donkere gangen begint nu...\n");
            }
        }
        catch
        {
            Console.WriteLine("Kon spelinformatie niet ophalen van de API.\n");
        }
    }
}

// DTO's voor JSON communicatie met de API
public record StatusDto(string Message);
public record GameInfoDto(string Title, string Description, int TotalLevels);
