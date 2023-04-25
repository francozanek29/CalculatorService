namespace CalculatoService.Client
{
  public class AddOperationMenu
  {
    private readonly HttpGateaway _httpGateaway = new();

    public async Task HandleMenuChoiceSelection()
    {
      List<int> addends = new();

      Console.WriteLine("You have selected the add operation");
      bool addMoreElements = true;

      while(addMoreElements)
      {
        Console.WriteLine("Please insert all the elements you want to add, to finalize press %");
        var option = Console.ReadLine();

        if(option != "%")
        {
          addends.Add(int.Parse(option));
        }
        else
        {
          addMoreElements = false;
        }
      };


    }
  }
}
