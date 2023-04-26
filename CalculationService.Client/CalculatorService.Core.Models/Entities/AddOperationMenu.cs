using CalculatorService.Core.Models.Entities.RequestsModels;

namespace CalculatorService.Core.Models.Entities
{
    public class AddOperationMenu 
    {
        private readonly HttpGateaway _httpGateaway = new();
        private readonly string endpoint = "calculator/add";

        public async Task PerfomanceExecution(string url)
        {
            List<int> addends = new();

            Console.WriteLine("You have selected the add operation");
            bool addMoreElements = true;

            while (addMoreElements)
            {
                Console.WriteLine("Please insert all the elements you want to add, to finalize press %");

                var option = Console.ReadLine();

                if (option != "%")
                {
                    addends.Add(int.Parse(option));
                }
                else
                {
                    addMoreElements = false;
                }
            };

            Console.WriteLine("If you want to track the request, insert the value now:");
            var trackingId = Console.ReadLine();

            var modelToBeSend = new AddOperationModel()
            {
                Operands = addends
            };

            await _httpGateaway.PostAsyncAndShow(url + endpoint, trackingId, modelToBeSend);

            Console.WriteLine("Press any key to return the main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
