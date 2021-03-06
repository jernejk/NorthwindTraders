﻿using NorthwindTraders.Application.Reports.Queries;
using NorthwindTraders.Persistance;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Reports
{
    public class EmployeesWithManagersQueryTests : TestBase
    {
        [Fact]
        public async Task ShouldReturnReport()
        {
            UseSqlite();

            using (var context = GetDbContext())
            {
                // Arrange
                // Use same seed data as production.
                NorthwindInitializer.Initialize(context);

                var query = new EmployeesWithManagersQuery(context);

                // Act
                var result = await query.Execute();

                // Assert
                Assert.NotEmpty(result);
                Assert.Equal(8, result.Count());
                Assert.Contains(result, r => r.ManagerTitle == "Vice President, Sales");
                Assert.DoesNotContain(result, r => r.EmployeeTitle == "Vice President, Sales");
            }
        }
    }
}
