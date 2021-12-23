using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Shared
{
    public static class GeneralStringFunctions
    {
       
        public static string GeneratePassword(int size)
        {
            if (size < 7)
                size = 7;

            size = size - 3;
            StringBuilder builder = new StringBuilder();

            Random random = new Random();
            char ch;
            char chCapital;
            int sizeSmall = (size / 2);
            int sizeCpital = size - sizeSmall;

            for (int i = 0; i < sizeSmall; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch.ToString().ToLower());
            }

            for (int i = 0; i < sizeCpital; i++)
            {
                chCapital = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(chCapital.ToString().ToUpper());
            }

            return builder.ToString() + random.Next(99) + "#";
        }

    }
}
