﻿using System;
using System.Collections.Generic;

namespace JTN.DVDCentral.PL
{
    public partial class tblMovieGenre
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}
