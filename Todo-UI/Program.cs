// See https://aka.ms/new-console-template for more information

using ConsoleTableExt;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using TodoApi.Models;

static async Task<List<Note>?> GetProcessRepository()
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync("https://localhost:7033/api/Note");

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        List<Note>? myNote = JsonConvert.DeserializeObject<List<Note>>(content);

        return myNote;

    }
    return null;
}

static async Task PostProcessRepository(Note note)
{
    HttpClient client = new HttpClient();

    var json = JsonConvert.SerializeObject(note);
    var data = new StringContent(json, Encoding.UTF8 ,"application/json");

    var response = await client.PostAsync("https://localhost:7033/api/Note/", data);
    var result = await response.Content.ReadAsStringAsync();

    Console.WriteLine(result);

}

static async Task DeleteProcessRepository(string id)
{
    HttpClient client = new HttpClient();

    var response = await client.DeleteAsync($"https://localhost:7033/api/Note/{id}");
    var result  = await response.Content.ReadAsStringAsync();

    Console.WriteLine(result);
}

static async Task MainMenu()
{
    Console.WriteLine("Welcome to the Todo-Api");
    Console.WriteLine("-----------------------");
    Console.WriteLine("1. Get All Notes");
    Console.WriteLine("2. Get a Single Note");
    Console.WriteLine("3. Search by Title or Text");
    Console.WriteLine("4. Update a Note");
    Console.WriteLine("5. Delete a Note");
    Console.WriteLine("6. Create a Note");
    Console.Write("Choose an option: ");
    string? reply = Console.ReadLine();

    switch (reply)
    {
        case "1": await GetAllNotes(); break;
        case "5": await DeleteNote(); break;
        case "6": await AddNote(); break;

    }
}

static async Task DeleteNote()
{
    await GetAllNotes();
    Console.Write("Please choose a Note to delete (id): ");
    string? reply = Console.ReadLine();
    await DeleteProcessRepository(reply);
}

static async Task GetAllNotes()
{
    var content = await GetProcessRepository();
    ConsoleTableBuilder
        .From(content)
        .WithFormat(ConsoleTableBuilderFormat.Alternative)
        .ExportAndWriteLine(TableAligntment.Center);

}

static async Task AddNote()
{
    Console.Write("Add your Title: ");
    string? title = Console.ReadLine();
    Console.Write("Add your Text: ");
    string? text = Console.ReadLine();

    var content = new Note { Title = title, Text = text };

    await PostProcessRepository(content);
}

    

await MainMenu();
