using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using together_aspcore.Shared;

namespace together_aspcore.Test
{
    public static class TestHelper
    {
        public static TogetherDbContext GetInMemoryDbContext(string dbName = "db")
        {
            var options = new DbContextOptionsBuilder<TogetherDbContext>()
                .UseInMemoryDatabase(dbName)
                .EnableSensitiveDataLogging()
                .Options;
            var context = new TogetherDbContext(options);
            return context;
        }
    }
}