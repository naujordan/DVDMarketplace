using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public int RatingId { get; set; }
        [DisplayName("Rating")]
        public string RatingDesc { get; set; }
        public int FormatId { get; set; }
        [DisplayName("Format")]
        public string FormatDesc { get; set; }
        public int DirectorId { get; set; }
        [DisplayName("Director")]
        public string DirectorName { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
