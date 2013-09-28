using System.Collections.Generic;
using Tourizen.Models;

namespace Tourizen.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<RoomTypeViewModel> FRoomTypeViews { get; set; }
        public IEnumerable<LocationGroupModel> LocationGroups { get; set; }

        public HomeViewModel()
        {
            FRoomTypeViews = new List<RoomTypeViewModel>();
            LocationGroups = new List<LocationGroupModel>();
        }
    }

    public class RoomTypeViewModel
    {
        public RoomTypeModel RoomType { get; set; }
        public string HotelName { get; set; }
        public string LocationName { get; set; }
    }

    public class LocationGroupModel
    {
        public string Letter { get; set; }
        public IEnumerable<LocationModel> Locations { get; set; }
    }
}