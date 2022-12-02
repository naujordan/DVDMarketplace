using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        public List<Movie> Items { get; set; }
        public int TotalCount { get { return Items.Count; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SubTotal { get 
            {
                double tempTotal = 0;
                foreach (var item in Items)
                {
                    
                    tempTotal += item.Cost; 
                }
                return tempTotal; 
            } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Tax { get { return SubTotal * .055; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Total { get { return SubTotal + Tax; } }

        public ShoppingCart()
        {
            Items = new List<Movie>();
        }
    }
}
