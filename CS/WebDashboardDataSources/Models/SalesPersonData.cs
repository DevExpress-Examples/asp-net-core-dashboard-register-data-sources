using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDashboardDataSources {
    public class SalesPersonData {
        public string SalesPerson { get; set; }
        public int Quantity { get; set; }

        public static List<SalesPersonData> CreateData() {
            List<SalesPersonData> data = new List<SalesPersonData>();
            string[] salesPersons = { "Andrew Fuller", "Michael Suyama",
                                "Robert King", "Nancy Davolio",
                                "Margaret Peacock", "Laura Callahan",
                                "Steven Buchanan", "Janet Leverling" };
            var rnd = new Random();
            for(int i = 0; i < 100; i++) {
                SalesPersonData record = new SalesPersonData();
                record.SalesPerson = salesPersons[rnd.Next(0, salesPersons.Length)];
                record.Quantity = rnd.Next(0, 100);
                data.Add(record);
            }
            return data;
        }
    }
}
