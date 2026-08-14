using System;

namespace StudentManagementOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            MenuManager menu = new MenuManager();
            menu.Run();
        }
    }
}