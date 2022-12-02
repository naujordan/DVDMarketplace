using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        [DisplayName("Movie Title")]
        public string MovieTitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}
