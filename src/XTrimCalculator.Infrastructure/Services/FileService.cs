using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using XTrimCalculator.Domain.Interfaces;

namespace XTrimCalculator.Infrastructure.Services
{
    public class FileReadService : IFileReadService
    {
        private readonly ILogger<FileReadService> logger;
        public FileReadService(ILogger<FileReadService> logger)
        {
            this.logger = logger;
        }

        public Task<string[]> ReadAllLines(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllLinesAsync(filePath);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Error trying to read the file");
                    throw;
                }
            }

            throw new FileNotFoundException();
        }
    }
}