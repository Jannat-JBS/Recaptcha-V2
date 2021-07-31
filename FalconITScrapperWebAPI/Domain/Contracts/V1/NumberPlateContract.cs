using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Contracts.V1
{
    public class NumberPlateContract
    {
        [Required]
        public string NumberPlate { get; set; }
        [Required]
        public string Proxy { get; set; }


    }
}
