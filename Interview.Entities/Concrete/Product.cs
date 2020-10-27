using Interview.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Interview.Entities.Concrete
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int AdsNumber { get; set; }
        public string Price { get; set; }
        public string FirstPrice { get; set; }
        public DateTime Date { get; set; }

    }
}
