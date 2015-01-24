using System;
using System.ComponentModel;

namespace Menu.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new MenuCollection("Hello Menu", false)
                       {
                           HelloWorld
                       };
            menu.Run();
        }

        [Description("Hello World Menu Item")]
        private static void HelloWorld()
        {
            Console.WriteLine(@"Hello");
        }
    }
}
