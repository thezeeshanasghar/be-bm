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
        public RoomType RoomType { get; set; }
        public int RoomCapacity { get; set; }
        public double RoomCharges {get; set;}
        
}

    public enum RoomType
    {
     RoomType1,RoomType2
    }

}
