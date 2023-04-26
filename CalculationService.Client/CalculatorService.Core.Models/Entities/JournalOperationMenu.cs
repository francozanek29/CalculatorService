using CalculatorService.Core.Models.Entities.RequestsModels;

namespace CalculatorService.Core.Models.Entities
{
    public class JournalOperationMenu
    {
        private readonly HttpGateaway _httpGateaway = new();
        private readonly string endpoint = "journal/query";

        public async Task PerfomanceExecution(string url)
        {
            Console.WriteLine("You have selected the Journal operation");

            Console.WriteLine("Please insert the tracking id");
            var option = Console.ReadLine();

            var modelToBeSend = new JournalModel()
            {
                TrackingId = option,
            };

            await _httpGateaway.PostAsyncAndShow(url + endpoint, string.Empty, modelToBeSend);

            Console.WriteLine("Press any key to return the main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
