using System;

namespace MenuSystem.Sample
{
    class Program
    {
        /// <code>
        /// static void Main()
        /// {
        ///     var menu = new MenuSystem.MenuDrivenApplication("Hello Menu");
        ///     menu.Add(HelloWorld);
        ///     menu.Run();
        /// }
        ///
        /// static void HelloWorld()
        /// {
        ///     Console.WriteLine("Hello");
        /// }
        /// </code>
        static void Main(string[] args)
        {
            var menu = new MenuSystem.MenuDrivenApplication("Hello Menu");
            menu.Add(HelloWorld);
            menu.Run();
        }

        private static void HelloWorld()
        {
            Console.WriteLine("Hello");
        }
    }
}
