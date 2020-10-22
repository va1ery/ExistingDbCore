using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ExistingDbCore
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<AdventureWorks2012Context> _optionsBuilder;
        static void Main(string[] args)
        {
            BuildConfiguration();
            BuildOptions();
            ListPeople();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true,
            reloadOnChange: true);
            _configuration = builder.Build();
        }
        static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorks2012Context>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
        }
        static void ListPeople()
        {
            using (var db = new AdventureWorks2012Context(_optionsBuilder.Options))
            {
                var people = db.Person.OrderByDescending(x => x.LastName).Take(20).
                ToList();
                foreach (var person in people)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
    }
}
