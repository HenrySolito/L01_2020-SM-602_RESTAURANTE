﻿using System.ComponentModel.DataAnnotations;
namespace L01_2020_SM602.Models
{
    public class platos
    {
        [Key]
        public int platoId { get; set; }
        public string nombrePlato { get; set; }
        public decimal precio { get; set; }

    }
}
