using dotnet_console_sample.DataAccess;
using dotnet_console_sample.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace dotnet_console_sample.Implement
{
    public class Sclass : ISclass
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ISchool _school;
        private readonly GenerDbContext _dbContext;
        public Sclass(IConfiguration configuration, ILogger logger, ISchool school, GenerDbContext dbContext)
        {
            _configuration = configuration;
            _logger = logger;
            _school = school;
            _dbContext = dbContext;
        }
        public async Task<string> DisplayClassName()
        {
            var school = await _school.DisplayName();
            var className = _configuration["MySettings:Class"];
            return await Task.FromResult($"{Environment.NewLine}🏫School: {school}{Environment.NewLine}👨‍🎓Class: {className}");
        }

        public async Task GetCustomerList()
        {
            var customerList = await _dbContext.Customer.Where(a => a.CusID > 0).OrderByDescending(a => a.CusID).Take(10).ToListAsync();
            _logger.Information("Get customer list: ");
            foreach (var item in customerList)
            {
                Console.WriteLine($"ID:{item.CusID} Name:{item.CusName}");
            }
            _logger.Information("Get customer list end.");
        }
    }
}