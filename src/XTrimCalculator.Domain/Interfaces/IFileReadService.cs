using System.Threading.Tasks;

namespace XTrimCalculator.Domain.Interfaces
{
    public interface IFileReadService
    {
        Task<string[]> ReadAllLines(string filePath);
    }
}