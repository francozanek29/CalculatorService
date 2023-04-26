using CalculatorService.Core.Models.Entities;

var showMenu = true;
var url = "https://localhost:7241/";

var addOperationMenu = new AddOperationMenu();
var divOperationMenu = new DivitionOperationMenu();
var multiplyOperationMenu = new MultiplyOperationMenu();
var subOperationMenu = new SubOperationMenu();
var sqrtOperationMenu = new SqrtOperationMenu();
var journalOperationMenu = new JournalOperationMenu();  

while (showMenu)
{
    Console.WriteLine("1. Add Multiple values");
    Console.WriteLine("2. Multiple Multiple values");
    Console.WriteLine("3. Difference for two elements");
    Console.WriteLine("4. Divide two numbers");
    Console.WriteLine("5. Square for a number");
    Console.WriteLine("6. Get a Journal");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            await addOperationMenu.PerfomanceExecution(url);
            break;
        case "2":
            await multiplyOperationMenu.PerfomanceExecution(url);
            break;
        case "3":
            await subOperationMenu.PerfomanceExecution(url);
            break;
        case "4":
            await divOperationMenu.PerfomanceExecution(url);
            break;
        case "5":
            await sqrtOperationMenu.PerfomanceExecution(url);
            break;
        case "6":
            await journalOperationMenu.PerfomanceExecution(url);
            break;
        case "7":
            showMenu = false;
            break;
        default:
            Console.WriteLine("Opción inválida");
            break;
    }

}
