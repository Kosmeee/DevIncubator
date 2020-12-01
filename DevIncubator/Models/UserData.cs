using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevIncubator.Models
{
    public class UserData
    {
        public int UserDataId { get; set; }
        [Range(-99,99, ErrorMessage = "A Must be in between -99 and 99")]
        public int A { get; set; }
        [Range(-99, 99, ErrorMessage = "B Must be in between -99 and 99")]
        public int B { get; set; }
        [Range(-99, 99, ErrorMessage = "C Must be in between -99 and 99")]
        public int C { get; set; }
        [Range(1, 99,ErrorMessage ="Step Must be in between 1 and 99")]
        public int Step { get; set; }
        [Range(-99, 99, ErrorMessage = "Start Must be in between -99 and 99")]
        public int Start { get; set; }
        [Range(-99, 99, ErrorMessage = "End Must be in between -99 and 99")]
        public int End { get; set; }
        public List<Point> Points { get; set; }
    }
}