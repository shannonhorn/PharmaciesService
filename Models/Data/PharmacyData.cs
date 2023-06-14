using CsvHelper;
using PharmaciesService.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PharmaciesService.Data
{
    public static class PharmacyData
    {
        public static List<Pharmacy> LoadPharmacies()
        {
            using var reader = new StreamReader("./PharmacyData.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Pharmacy>();
            return new List<Pharmacy>(records);
        }
    }
}
