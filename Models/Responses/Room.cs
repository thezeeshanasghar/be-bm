using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNo { get; set; }
        public string RoomType { get; set; }
        public int RoomCapacity { get; set; }
        public double RoomCharges { get; set; }

    }
    public class Rooms
    {
        public IEnumerable<Room> rooms { get; set; }
        public int Count { get; set; }
    }

}
