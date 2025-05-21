namespace LabelRemover;

internal class Program
{
    static void Main(string[] args)
    {
        
        string spaceKey = "MAPCO";
        string label = "deletion_candidate";
        string cql = $"space={spaceKey} AND label={label}";
        //string cql = $"space={spaceKey}";

        Task.Run(async () => 
        {
            using var service = new Confluence("confluence", "LabelRemover");

            var list = service.SearchContentAsync(cql, "space");
            await foreach (var item in list)
            {
                Console.WriteLine($"{item.Id}: {item.Type} {item.Space?.Key} {item.Title}");
                await service.DeleteLabelAsync(item.Id, label);
            }

        }).Wait();

        Console.WriteLine("Ready!");
        Console.ReadLine();

    }
}
