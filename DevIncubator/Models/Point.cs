using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevIncubator.Models
{
    public class Point
    {
        public int PointId { get; set; }
        public UserData Chart { get; set; }
        public int ChartId { get; set; }
        public int PointX { get; set; }
        public int PointY { get; set; }
    }
}