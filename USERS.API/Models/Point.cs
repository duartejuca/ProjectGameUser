using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USERS.API.Models
{
    public class Point
    {

        public int Id { get; set; }
        public string PointPlayer { get; set; } = "";
        public string PointDescription { get; set; } = "";
        public int PointValue { get; set; } = 0;

        public Point() { }

        public Point(int id, string pointPlayer, string pointDescription, int pointValue)
        {
            Id = id;
            PointPlayer = pointPlayer;
            PointDescription = pointDescription;
            PointValue = pointValue;
        }
    }
}