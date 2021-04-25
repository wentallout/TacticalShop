using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TacticalShop.Backend.Controllers;
using TacticalShop.Backend.Models;
using TacticalShop.ViewModels;
using Xunit;

namespace TacticalShop.Test
{
    public class BrandsControllerTests : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public BrandsControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task PostBrand_Success()
        {
            var dbContext = _fixture.Context;
            var brand = new BrandCreateRequest { BrandName = "TestPostBrand" };

            var controller = new BrandsController(dbContext);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);
            Assert.Equal("TestPostBrand", returnValue.BrandName);
        }

        [Fact]
        public async Task GetBrand_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { BrandName = "TestGetBrand" });
            await dbContext.SaveChangesAsync();

            var controller = new BrandsController(dbContext);
            var result = await controller.GetBrand();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BrandVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }
    }
}