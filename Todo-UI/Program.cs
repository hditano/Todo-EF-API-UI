// See https://aka.ms/new-console-template for more information

using ConsoleTableExt;
using Newtonsoft.Json;
using System.Text;
using TodoApi.Models;

static async Task<Note> SingleProcessRepository(string id)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync($"https://localhost:7033/api/Note/{id}");
    
    if(response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var myNote = JsonConvert.DeserializeObject<Note>(content);

        return myNote;
    }
    return null;
}

static async void UpdateProcessRepository(Note content, string id)
{
    try
    {
        HttpClient client = new HttpClient();

        var data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:7033/api/Note/{id}", data);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}

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
    var data = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await client.PostAsync("https://localhost:7033/api/Note/", data);
    var result = await response.Content.ReadAsStringAsync();

    Console.WriteLine(result);

}

static async Task DeleteProcessRepository(string id)
{
    HttpClient client = new HttpClient();

    var response = await client.DeleteAsync($"https://localhost:7033/api/Note/{id}");
    var result = await response.Content.ReadAsStringAsync();

    Console.WriteLine(result);
}

static async Task MainMenu()
{
    bool isActive = true;

    while (isActive)
    {
        Console.WriteLine("Welcome to the Todo-Api");
        Console.WriteLine("-----------------------");
        Console.WriteLine("1. Get All Notes");
        Console.WriteLine("2. Get Single Note");
        Console.WriteLine("3. Search by Title or Text");
        Console.WriteLine("4. Update a Note");
        Console.WriteLine("5. Delete a Note");
        Console.WriteLine("6. Create a Note");
        Console.WriteLine("7. Quit Program");
        Console.Write("Choose an option: ");
        string? reply = Console.ReadLine();

        switch (reply)
        {
            case "1": await GetAllNotes(); break;
            case "4": await UpdateNote(); break;
            case "5": await DeleteNote(); break;
            case "6": await AddNote(); break;
            case "7": isActive = false; break;

        }
    }
}

static async Task UpdateNote()
{
    Console.Write("Please chose a Note do Update (id): ");
    string? replyId = Console.ReadLine();
    var content = await SingleProcessRepository(replyId);

    Console.Write($"Current TItle: {content.Title} - Write your updated Title: ");
    string? titleReply = Console.ReadLine();
    Console.Write($"Current Text: {content.Text.Trim()} - Write your new Text: ");
    string? textReply = Console.ReadLine();

    var updateNote = new Note
    { 
        Title = titleReply, 
        Text = textReply 
    };

    UpdateProcessRepository(updateNote, replyId);

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
    Console.Write("Press any key to continue...");
    Console.ReadKey();
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
