using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TacticalShop.Backend.Controllers;
using TacticalShop.Backend.Data;
using TacticalShop.Backend.Models;
using TacticalShop.ViewModels;

namespace TacticalShop.Test
{
    public class CategoriesControllerTests : IDisposable
    {
        private SqliteConnection _connection;
        private DatabaseContext _dbContext;

        public CategoriesControllerTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new DatabaseContext(options);
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        [Fact]
        public async Task PostCategory_Success()
        {
            var category = new CategoryCreateRequest { CategoryName = "PostTestCategory" };

            var controller = new CategoriesController(_dbContext);
            var result = await controller.PostCategory(category);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CategoryVm>(createdAtActionResult.Value);
            Assert.Equal("PostTestCategory", returnValue.CategoryName);
        }

        [Fact]
        public async Task GetCategory_Success()
        {
            _dbContext.Categories.Add(new Category { CategoryName = "GetCategoryTest" });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoriesController(_dbContext);
            var result = await controller.GetCategory();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<CategoryVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }
    }
}