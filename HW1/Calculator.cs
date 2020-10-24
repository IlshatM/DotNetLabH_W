using System;
using System.Text;

namespace HW1
{
    public static class Calculator
    {
        public static double Calculate(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            bool reversed = false;
            if (sb[0] == '-')
            {
                sb.Remove(0, 1);
                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] == '-') sb.Replace('-','+',i,1);
                    else if (sb[i] == '+') sb.Replace('+','-',i,1);
                }

                reversed = true;
            }
            for (int i = 0; i < sb.Length-1; i++)
            {
                if ((sb[i] == '+' || sb[i] == '-') && (sb[i + 1] == '+' || sb[i + 1] == '-'))
                {
                    sb.Remove(i, 1);
                }
            }

            if (reversed) return -1*NumberPlus(sb.ToString());
            return NumberPlus(sb.ToString());
        }
        private static double NumberPlus(string s)
        {
            string[] str = s.Split('+');
            var result = 0.0;
            foreach (var i in str)
            {
                result += NumberMinus(i);
            }
            return result;
        }
        private static double NumberMinus(string s)
        {
            string[] str = s.Split('-');
            var result = NumberMult(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result -= NumberMult(str[i]);
            }
            return result;
        }
        private static double NumberMult(string s)
        {
            string[] str = s.Split('*');
            var result = NumberDev(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result *= NumberMult(str[i]);
            }
            return result;
        }
        private static double NumberDev(string s)
        {
            string[] str = s.Split('/');
            var result = Number(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result /= Number(str[i]);
            }
            return result;
        }
        private static double Number(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i])||s[i]==',')
                {
                    sb.Append(s[i]);
                }
            }
            return double.Parse(sb.ToString());
        }
    }
}
