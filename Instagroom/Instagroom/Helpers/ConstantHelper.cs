using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Instagroom.Helpers
{
    class ConstantHelper
    {
        public static Regex emailPattern = new Regex(@"^(?("")("".+?(?<!\\)\""@)|(([0 - 9a - z]((\.( ? !\.)) |[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        public static string PasswordIncorrectAlert { get => "Incorrect password. Try re-entering your credentials"; }

    }
}
