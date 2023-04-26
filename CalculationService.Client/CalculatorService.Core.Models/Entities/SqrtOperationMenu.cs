using CalculatorService.Core.Models.Entities.RequestsModels;

namespace CalculatorService.Core.Models.Entities
{
    public class SqrtOperationMenu
    {
        private readonly HttpGateaway _httpGateaway = new();
        private readonly string endpoint = "calculator/sqrt";

        public async Task PerfomanceExecution(string url)
        {
            Console.WriteLine("You have selected the Square operation");

            var number = 0;

            var validInput = false;

            while(!validInput) 
            {
                Console.WriteLine("Please insert the Number for the operation, it should be positive");
                var option = Console.ReadLine();
                if(!int.TryParse(option, out number)) 
                {
                    Console.WriteLine("The value inserted is not numeric");
                }
                else
                {
                    if (number <= 0)
                    {
                        Console.WriteLine("The number should be positive");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
            }

            Console.WriteLine("If you want to track the request, insert the value now:");
            var trackingId = Console.ReadLine();

            var modelToBeSend = new SqrtModel()
            {
                Number = number,
            };

            await _httpGateaway.PostAsyncAndShow(url + endpoint, trackingId, modelToBeSend);

            Console.WriteLine("Press any key to return the main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
