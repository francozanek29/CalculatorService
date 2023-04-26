using CalculatorService.Core.Models.Entities.RequestsModels;

namespace CalculatorService.Core.Models.Entities
{
    public class DivitionOperationMenu
    {
        private readonly HttpGateaway _httpGateaway = new();
        private readonly string endpoint = "calculator/div";

        public async Task PerfomanceExecution(string url)
        {
            Console.WriteLine("You have selected the Div operation");

            var dividend = 0;

            var divisor = 1;

            var validInput = false;

            while(!validInput) 
            {
                Console.WriteLine("Please insert the Dividen for the operation");
                var option = Console.ReadLine();
                if(!int.TryParse(option, out dividend)) 
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
                Console.WriteLine("Please insert the Divisor for the operation, should be any number except zero");

                var option = Console.ReadLine();

                if (!int.TryParse(option, out divisor))
                {
                    Console.WriteLine("The value inserted is not numeric");
                }
                else
                {
                    if(divisor != 0) 
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("The value inserted should not be zero");
                    }   
                    
                }
            }

            Console.WriteLine("If you want to track the request, insert the value now:");
            var trackingId = Console.ReadLine();

            var modelToBeSend = new DivOperationModel()
            {
                Dividend = dividend,
                Divisor = divisor
            };

            await _httpGateaway.PostAsyncAndShow(url + endpoint, trackingId, modelToBeSend);

            Console.WriteLine("Press any key to return the main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
