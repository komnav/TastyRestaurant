using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.UnitTests
{
    [TestFixture]
    public class MenuCategoryRepositoryTests
    {
        private ApplicationDbContext _dbContext;
        private MenuCategoryRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _repository = new MenuCategoryRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_ShouldAddCategory()
        {
            var category = new MenuCategory { Id = 1, Name = "Test Category", ParentId = 2 };

            var result = await _repository.CreateAsync(category);

            Assert.NotNull(result);
            //Assert.AreEqual(category.Name, result.Name);

            var categoryInDb = await _dbContext.MenuCategories.FindAsync(category.Id);
            Assert.NotNull(categoryInDb);
            Assert.AreEqual(category.Name, categoryInDb.Name);
        }
    }
}
