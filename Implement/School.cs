using dotnet_console_sample.Interface;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace dotnet_console_sample.Implement
{
    public class School(IConfiguration configuration, ILogger logger) : ISchool
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger _logger = logger;

        public async Task<string> DisplayName()
        {
            _logger.Information("Getting school name...");
            return await Task.FromResult(_configuration["MySettings:School"]??"");
        }
    }
}