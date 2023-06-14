using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaciesService.Controllers;
using PharmaciesService.Models;

namespace PharmaciesService.Tests
{
  [TestClass]
    public class PharmaciesControllerTests
    {
        [TestMethod]
        public void GetLocationsByTimeSpan_ReturnsOpenPharmacies()
        {
            // Arrange
            var controller = new PharmaciesController();

            // Act
            ActionResult<List<Pharmacy>> result = controller.GetLocationsByTimeSpan(
                "10:00",
                "13:00"
            );

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Pharmacy>));
            var pharmacies = (List<Pharmacy>)okResult.Value;
            Assert.AreEqual(2, pharmacies.Count);
            // Add more assertions as needed
        }

        [TestMethod]
        public void GetLocationsByTimeSpan_ReturnsNotFound()
        {
            // Arrange
            var controller = new PharmaciesController();

            // Act
            ActionResult<List<Pharmacy>> result = controller.GetLocationsByTimeSpan(
                "01:00",
                "04:00"
            );

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual("No pharmacies are open during the specified time span.", notFoundResult.Value);
        }
    }
}
