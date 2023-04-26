using CalculatorService.Core.Models.Entities.RequestsModels;

namespace CalculatorService.Core.Models.Entities
{
    public class SubOperationMenu
    {
        private readonly HttpGateaway _httpGateaway = new();
        private readonly string endpoint = "calculator/sub";

        public async Task PerfomanceExecution(string url)
        {
            Console.WriteLine("You have selected the Diff operation");

            var minuend = 0;

            var substrahend = 1;

            var validInput = false;

            while(!validInput) 
            {
                Console.WriteLine("Please insert the Minuend for the operation");
                var option = Console.ReadLine();
                if(!int.TryParse(option, out minuend)) 
                {
                    Console.WriteLine("The value inserted is not numeric");
                }
                else
                {
                    validInput = true;
                }
            }

            validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Please insert the Substrahend for the operation");
                var option = Console.ReadLine();
                if (!int.TryParse(option, out substrahend))
                {
                    Console.WriteLine("The value inserted is not numeric");
                }
                else
                {
                    validInput = true;
                }
            }

            Console.WriteLine("If you want to track the request, insert the value now:");
            var trackingId = Console.ReadLine();

            var modelToBeSend = new SubModel()
            {
                Minuend = minuend,
                Subtrahend = substrahend
            };

            await _httpGateaway.PostAsyncAndShow(url + endpoint, trackingId, modelToBeSend);

            Console.WriteLine("Press any key to return the main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
