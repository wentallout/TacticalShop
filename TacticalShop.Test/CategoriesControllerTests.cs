using System.Threading.Tasks;
using Xunit;
using TacticalShop.Backend.Controllers;
using TacticalShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TacticalShop.Backend.Models;
using TacticalShop.Test;

namespace TacticalShop.Test
{
    public class CategoriesControllerTests : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public CategoriesControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task PostCategory_Success()
        {
            var dbContext = _fixture.Context;
            var category = new CategoryCreateRequest { CategoryName = "TestPostCategory" };

            var controller = new CategoriesController(dbContext);
            var result = await controller.PostCategory(category);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CategoryVm>(createdAtActionResult.Value);
            Assert.Equal("TestPostCategory", returnValue.CategoryName);
        }

        [Fact]
        public async Task GetCategory_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Categories.Add(new Category { CategoryName = "TestGetCategory" });
            await dbContext.SaveChangesAsync();

            var controller = new CategoriesController(dbContext);
            var result = await controller.GetCategory();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<CategoryVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }
    }
}