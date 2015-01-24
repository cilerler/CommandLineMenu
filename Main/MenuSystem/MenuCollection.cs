using System;
using System.Collections;
using System.Collections.Generic;
using Menu.Properties;

namespace Menu
{
    /// <code>
    ///     new MenuCollection("MyMenu", throwError: true)
    ///     {
    ///         MethodName
    ///     }.Run()
    /// </code>
    public sealed class MenuCollection : IEnumerable<MenuOption>
    {
        private bool _shouldQuit;
        private readonly string _introText;
        private readonly bool _throwError;
        private readonly List<MenuOption> _menuOptions = new List<MenuOption>();

        private MenuCollection(string introText, bool throwError)
        {
            _introText = introText;
            _throwError = throwError;
        }

        public MenuCollection(string introText, bool throwError, params Action[] options) : this(introText, throwError)
        {
            if (options != null)
            {
                foreach (Action option in options)
                {
                    Add(option);
                }
            }
        }

        public void Add(MenuOption option)
        {
            _menuOptions.Add(option);
        }

        public void Add(Action optionAction)
        {
            _menuOptions.Add(new DelegateMenuOption(optionAction));
        }

        public void Run()
        {
            while (!_shouldQuit)
            {
                DisplayMenu();
                DoUserSelection();
                PromptToContinue();
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            WriteMenuHeader();

            for (var i = 0; i < _menuOptions.Count; ++i)
            {
                WriteMenuOption(i);
            }

            WriteMenuFooter();
        }

        private void WriteMenuHeader()
        {
            Console.WriteLine(MenuOption.Underline);
            Console.WriteLine(_introText);
            Console.WriteLine(MenuOption.Underline);
        }

        private void WriteMenuOption(int index)
        {
            Console.WriteLine(Resources.MenuCollection_WriteMenuOption, Convert.ToChar(index + 65), _menuOptions[index].Text);
        }

        private static void WriteMenuFooter()
        {
            Console.WriteLine();
            Console.Write(Resources.MenuCollection_WriteMenuFooter_SelectOptionOrQuit);
        }

        private void DoUserSelection()
        {
            int selectedOption = ReadValidSelectedOptionFromUser();
            if (selectedOption != -1)
            {
                Console.Clear();
                _menuOptions[selectedOption].Execute(_throwError);
            }
        }

        private int ReadValidSelectedOptionFromUser()
        {
            int selectedOptionIndex = -1;
            while (!_shouldQuit &&
                   !SelectedOptionInValidRange(selectedOptionIndex))
            {
                selectedOptionIndex = ReadSelectedOptionFromUser();
            }
            return selectedOptionIndex;
        }

        private int ReadSelectedOptionFromUser()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                _shouldQuit = true;
                return -1;
            }
            return (int) key.Key - (int) ConsoleKey.A;
        }

        private bool SelectedOptionInValidRange(int selectedOptionIndex)
        {
            return selectedOptionIndex >= 0 && selectedOptionIndex < _menuOptions.Count;
        }

        private void PromptToContinue()
        {
            if (!_shouldQuit)
            {
                Console.WriteLine();
                Console.Write(Resources.MenuCollection_PromptToContinue_ContinueOrQuit);
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    _shouldQuit = true;
                }
            }
        }

        #region IEnumerable<MenuOption> Members

        public IEnumerator<MenuOption> GetEnumerator()
        {
            return _menuOptions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}