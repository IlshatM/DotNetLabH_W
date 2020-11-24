using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    public class CalculatorInApp: ICalculatorAsync
    {
        public async Task<double?> CalculateAsync(string s)
        {
            s = MinusAfterMult(s);
            s = MinusAndIrCheck(s, out var reversed);
            
            if (reversed) return (-1*(NumberPlus(s)));
            return NumberPlus(s);
        }

        private void ChangePrev(StringBuilder sb, int index)
        {
            sb.Remove(index, 1);
            int i;
            bool changed = false;
            for (i=index; i >= 0; i--)
            {
                if (sb[i] == '+')
                {
                    sb[i] = '-';
                    changed = true;
                    break;
                }
                if (sb[i] == '-')
                {
                    sb[i] = '+';
                    changed = true;
                    break;
                }
                
            }
            if (!changed) sb.Insert(0, '-');
        }
        private string MinusAfterMult(string s)
        {
            List<int> ls = new List<int>();
            for (int i = 0; i < s.Length-1; i++)
            {
                if ((s[i] == '*' || s[i] == '/') && s[i + 1] == '-')
                {
                    ls.Add(i+1);
                }
            }
            StringBuilder sb = new StringBuilder(s);
            foreach (var i in ls)
            {
                ChangePrev(sb,i);
            }

            return sb.ToString();

        }
        private string MinusAndIrCheck(string s, out bool reversed)
        {
            StringBuilder sb = new StringBuilder(s);
            reversed = false;
            if (sb[0] == '-')
            {
                sb.Remove(0, 1);
                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] == '-') sb.Replace('-', '+', i, 1);
                    else if (sb[i] == '+') sb.Replace('+', '-', i, 1);
                }

                reversed = true;
            }

            for (int i = 0; i < sb.Length - 1; i++)
            {
                if ((sb[i] == '+' || sb[i] == '-') && (sb[i + 1] == '+' || sb[i + 1] == '-'))
                {
                    sb.Remove(i, 1);
                }
            }

            return sb.ToString();
        }

        private double NumberPlus(string s)
        {
            string[] str = s.Split('+');
            var result = 0.0;
            foreach (var i in str)
            {
                result += NumberMinus(i);
            }
            return result;
        }
        private double NumberMinus(string s)
        {
            string[] str = s.Split('-');
            var result = NumberMult(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result -= NumberMult(str[i]);
            }
            return result;
        }
        private double NumberMult(string s)
        {
            string[] str = s.Split('*');
            var result = NumberDev(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result *= NumberMult(str[i]);
            }
            return result;
        }
        private double NumberDev(string s)
        {
            string[] str = s.Split('/');
            var result = Number(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                result /= Number(str[i]);
            }
            return result;
        }
        private double Number(string s)
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
