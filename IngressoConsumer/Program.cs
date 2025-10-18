using IngressoApi.Models;
using IngressoApi.Services;
using Spectre.Console;

namespace IngressoConsumer;

class Program {
    public static async Task Main(string[] args) {
        var apiClient = new IngressoClient("v0", "test");

        AnsiConsole.MarkupLine("[bold yellow]Welcome to the Movie Session Finder![/]");

        try {
            await RunSelectionFlow(apiClient);
        }
        catch (Exception ex) {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        AnsiConsole.MarkupLine("\n[green]Done.[/]");
    }

    public static async Task RunSelectionFlow(IngressoClient apiClient) {
        // 1. Select State
        var states = await AnsiConsole.Status()
            .StartAsync("Buscando estados...", async ctx => await apiClient.GetAllStatesWithCitiesAsync());

        if (states.Count == 0) {
            AnsiConsole.MarkupLine("[red]Não foi possível encontrar nenhum estado.[/]");
            return;
        }

        var selectedState = AnsiConsole.Prompt(
            new SelectionPrompt<State>()
                .Title("Selecione um [green]Estado[/]:")
                .AddChoices(states)
                .PageSize(10)
                .MoreChoicesText("[grey](Mova para cima e para baixo para ver mais estados)[/]")
                .UseConverter(state => $"{state.Uf} - {state.Name}"));

        // 2. Select City
        if (selectedState.Cities.Count == 0) {
            AnsiConsole.MarkupLine($"[red]Não há cidades disponíveis para {selectedState.Name}.[/]");
            return;
        }

        var selectedCity = AnsiConsole.Prompt(
            new SelectionPrompt<City>()
                .Title("Selecione uma [green]Cidade[/]:")
                .AddChoices(selectedState.Cities)
                .PageSize(10)
                .MoreChoicesText("[grey](Mova para cima e para baixo para ver mais cidades)[/]")
                .UseConverter(city => city.Name));

        // 3. Select Theater
        var theaters = await AnsiConsole.Status()
            .StartAsync("Buscando cinemas...", async ctx => await apiClient.GetTheatersByCityAsync(selectedCity.Id));

        if (theaters.Count == 0) {
            AnsiConsole.MarkupLine($"[red]Não há cinemas disponíveis em {selectedCity.Name}.[/]");
            return;
        }

        var selectedTheater = AnsiConsole.Prompt(
            new SelectionPrompt<Theater>()
                .Title("Selecione um [green]Cinema[/]:")
                .AddChoices(theaters)
                .PageSize(10)
                .MoreChoicesText("[grey](Mova para cima e para baixo para ver mais cinemas)[/]")
                .UseConverter(theater => theater.Name));

        // 4. Get and Display Sessions
        var sessions = await AnsiConsole.Status()
            .StartAsync("Buscando sessões...", async ctx => await apiClient.GetSessionsByTheaterAsync(selectedCity.Id, selectedTheater.Id));

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"\n[bold yellow]Exibindo sessões para: {selectedTheater.Name}[/]");

        if (sessions.Count == 0) {
            AnsiConsole.MarkupLine("[red]Nenhuma sessão encontrada para os próximos dias.[/]");
            return;
        }

        foreach (var day in sessions) {
            var table = new Table().Expand();
            table.Title = new TableTitle($"[white on blue] {day.DayOfWeek}, {day.DateFormatted} [/]");
            table.AddColumn("Filme");
            table.AddColumn("Duração");
            table.AddColumn("Sala");
            table.AddColumn("Horários");

            foreach (var movie in day.Movies) {
                foreach (var room in movie.Rooms) {
                    var sessionTimes = string.Join(" ", room.Sessions.Select(s => $"[green]{s.Time}[/]"));
                    var roomType = room.Type != null && room.Type.Count != 0 ? string.Join(", ", room.Type) : "Padrão";

                    table.AddRow(
                        $"{movie.Title} ([italic]{movie.ContentRating}[/])",
                        $"{movie.Duration} min",
                        $"{room.Name} ([dim]{roomType}[/])",
                        sessionTimes
                    );
                }
            }

            AnsiConsole.Write(table);
        }
    }
}