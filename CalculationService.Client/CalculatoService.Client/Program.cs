bool showMenu = true;
while (showMenu)
{
  Console.WriteLine("1. Opción 1");
  Console.WriteLine("2. Opción 2");
  Console.WriteLine("3. Salir");
  Console.Write("Elija una opción: ");
  var option = Console.ReadLine();

  switch (option)
  {
    case "1":
      // llamar a una función para manejar la opción 1
      break;
    case "2":
      // llamar a una función para manejar la opción 2
      break;
    case "3":
      showMenu = false;
      break;
    default:
      Console.WriteLine("Opción inválida");
      break;
  }
  Console.Clear();
}
