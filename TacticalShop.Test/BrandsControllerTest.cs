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
    public class BrandsControllerTests : IDisposable
    {
        private SqliteConnection _connection;
        private DatabaseContext _dbContext;

        public BrandsControllerTests()
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
        public async Task PostBrand_Success()
        {
            var brand = new BrandCreateRequest { BrandName = "PostTestBrand" };

            var controller = new BrandsController(_dbContext);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);
            Assert.Equal("PostTestBrand", returnValue.BrandName);
        }

        [Fact]
        public async Task GetBrand_Success()
        {
            _dbContext.Categories.Add(new Brand { BrandName = "GetBrandTest" });
            await _dbContext.SaveChangesAsync();

            var controller = new BrandsController(_dbContext);
            var result = await controller.GetBrand();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BrandVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }
    }
}