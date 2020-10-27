using System.Threading.Tasks;

namespace CalculatorASP.Services
{
    public interface ICalculator
    {
        double Calculate(string expression);
    }
}