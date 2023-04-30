// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using TodoApi.Models;

static async Task ProcessRepository()
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync("https://localhost:7033/api/Note");

    if(response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        List<Note>? myNote = JsonConvert.DeserializeObject<List<Note>>(content);

        foreach(var item in myNote)
        {
            Console.WriteLine(item.Title, item.Text);
        }
    }
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
    Console.Write("Choose an option: ");
    string? reply = Console.ReadLine();

    switch (reply)
    {
        case "1": await ProcessRepository(); break;

    }


}

await MainMenu();
