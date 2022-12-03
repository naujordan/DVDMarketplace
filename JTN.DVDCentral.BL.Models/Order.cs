using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Models
{
    public class Order
    {
        [DisplayName("Order #")]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }

        public string UserName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ShipDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SubTotal { get
        {
                double tempTotal = 0;
                foreach (var item in OrderItems)
                {
                    
                    tempTotal += item.Cost; 
                }
                return tempTotal; 
            } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Tax { get { return SubTotal * .055; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Total { get { return SubTotal + Tax; } }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
