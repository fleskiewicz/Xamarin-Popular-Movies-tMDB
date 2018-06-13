using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmiki.Models
{
    [Table("Films")]
    public class Film
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public string VoteAverage { get; set; }
        public bool IsAddedToList { get; set; }
    }
}
