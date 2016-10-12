using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UAVdrone.Models
{
    public class BattleFieldModels
    {
        [Required(ErrorMessage = "Width is required")]        
        public int? Width { get; set; }
        [Required(ErrorMessage = "Height is required")]
        public int? Height { get; set; }
    }
}