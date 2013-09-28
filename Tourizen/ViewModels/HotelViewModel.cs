using System.Collections.Generic;
using Tourizen.Models;


namespace Tourizen.ViewModels
{
    public class HotelViewModel
    {
      
        public HotelModel Hotel { get; set; }
        public IEnumerable<RoomTypeModel> RoomTypes { get; set; }

        public HotelViewModel()
        {
            RoomTypes = new List<RoomTypeModel>();
        }
    }
}