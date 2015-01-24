using System;
using System.ComponentModel;
using System.Linq;
using Menu.Properties;

namespace Menu
{
    internal sealed class DelegateMenuOption : MenuOption
    {
        private readonly Action _optionCode;

        internal DelegateMenuOption(Action optionCode)
        {
            _optionCode = optionCode;
            Text = GetDescriptionFromOptionCodeDelegate();
        }

        internal override string Text { get; }

        protected override void DoExecute()
        {
            _optionCode();
        }

        private string GetDescriptionFromOptionCodeDelegate()
        {
            DescriptionAttribute description = _optionCode.Method.GetCustomAttributes(typeof (DescriptionAttribute), false)
                                                          .Cast<DescriptionAttribute>()
                                                          .FirstOrDefault();
            return description == null
                       ? Resources.DelegateMenuOption_GetDescriptionFromOptionCodeDelegate_NoDescription
                       : description.Description;
        }
    }
}