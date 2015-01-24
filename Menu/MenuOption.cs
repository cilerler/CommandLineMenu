using System;
using Menu.Properties;

namespace Menu
{
    /// <summary>
    ///     A class that encapsulates the behavior of a single menu item,
    ///     and is responsible for
    /// </summary>
    public abstract class MenuOption
    {
        internal static readonly string Underline = new string('-', 79);

        /// <summary>
        ///     Text to display in the menu.
        /// </summary>
        internal abstract string Text { get; }

        /// <summary>
        ///     Execute the menu option
        /// </summary>
        internal void Execute(bool throwError)
        {
            WriteExampleHeader();
            try
            {
                DoExecute();
            }
            catch (Exception ex)
            {
                if (throwError)
                {
                    throw;
                }
                ShowExceptionDetails(ex);
            }
        }

        /// <summary>
        ///     Execute the actual operation.
        /// </summary>
        protected abstract void DoExecute();

        private void WriteExampleHeader()
        {
            Console.WriteLine(Underline);
            Console.WriteLine(Text);
            Console.WriteLine(Underline);
        }

        private static void ShowExceptionDetails(Exception ex)
        {
            Console.WriteLine(Resources.MenuOption_ShowExceptionDetails_ExceptionType, ex.GetType());
            Console.WriteLine(Resources.MenuOption_ShowExceptionDetails_Message, ex.Message);
            Console.WriteLine(Resources.MenuOption_ShowExceptionDetails_Source, ex.Source);
            if (null == ex.InnerException)
            {
                Console.WriteLine(Resources.MenuOption_ShowExceptionDetails_NoInnerException);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(Resources.MenuOption_ShowExceptionDetails_InnerException, ex.InnerException);
            }
            Console.WriteLine();
        }
    }
}