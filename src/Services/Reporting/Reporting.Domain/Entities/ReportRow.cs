

namespace Reporting.Domain.Entities;

public class ReportRow
{
   
        public string ItemName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public ReportRow(string itemName, int quantity, decimal price)
        {
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
        }
    }


