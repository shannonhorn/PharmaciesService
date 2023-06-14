using Microsoft.AspNetCore.Mvc;
using PharmaciesService.Models;
using PharmaciesService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmaciesService.Controllers
{
    [ApiController]
    [Route("[pharmacies]")]
    public class PharmaciesController : ControllerBase
    {
      [HttpGet("getLocationsByTimeSpan")]
      public ActionResult<List<Pharmacy>> GetLocationsByTimeSpan([FromQuery] string startTimeString, [FromQuery] string endTimeString)
      {
          var pharmacies = PharmacyData.LoadPharmacies();
          var openPharmacies = new List<Pharmacy>();
          
          TimeSpan startTime = TimeSpan.Parse(startTimeString); // Input format: "10:00"
          TimeSpan endTime = TimeSpan.Parse(endTimeString); // Input format: "13:00"

          foreach (var pharmacy in pharmacies)
          {
              var hours = pharmacy.HoursOfOperation.Split('-');
              var openingTime = DateTime.Parse(hours[0]).TimeOfDay;
              var closingTime = DateTime.Parse(hours[1]).TimeOfDay;

              // Check if the pharmacy is open during the specified time span
              if ((startTime >= openingTime && startTime <= closingTime) && 
                  (endTime >= openingTime && endTime <= closingTime))
              {
                  openPharmacies.Add(pharmacy);
              }
          }

          if (openPharmacies.Count == 0)
          {
              return NotFound("No pharmacies are open during the specified time span.");
          }

          return Ok(openPharmacies);
      }
    }
}
