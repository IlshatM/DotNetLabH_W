using System.Threading.Tasks;

namespace CalcExpProxy
{
    public interface ICalculatorAsync
    {
        Task<double?> CalculateAsync(string expression);
    }
}