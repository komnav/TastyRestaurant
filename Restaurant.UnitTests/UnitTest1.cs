using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Restaurant.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            applicationDbContext.Database.Migrate();
        }
    }
}