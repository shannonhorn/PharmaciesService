using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmaciesService.Controllers;
using PharmaciesService.Models;
using System;
using System.Collections.Generic;

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
                new DateTime(2023, 1, 1, 10, 0, 0),  // Start time: 10:00
                new DateTime(2023, 1, 1, 13, 0, 0)  // End time: 13:00
            );

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result.Result;
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
                new DateTime(2023, 1, 1, 1, 0, 0),   // Start time: 01:00
                new DateTime(2023, 1, 1, 4, 0, 0)   // End time: 04:00
            );

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            var notFoundResult = (NotFoundObjectResult)result.Result;
            Assert.AreEqual("No pharmacies are open during the specified time span.", notFoundResult.Value);
        }
    }
}
