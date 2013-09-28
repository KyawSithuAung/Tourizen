using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tourizen.Models;
using Tourizen.ViewModels;

namespace Tourizen.Controllers
{

    public class HomeController : Controller
    {
        private MainDBContext db = new MainDBContext();

        public ActionResult Index()
        {
            var Locations = new List<LocationModel>();
            Locations = db.Locations.ToList();

            var locationGroups = Locations.GroupBy(l => l.Name.Substring(0, 1)).Select(g => new LocationGroupModel
            {
                Letter = g.Key,
                Locations = g
            }).OrderBy(x => x.Letter);

            List<RoomTypeModel> fRoomTypes = db.RoomTypes.Where(x => x.IsFeatured == true).ToList();
            List<RoomTypeViewModel> fRoomTypeViews = new List<RoomTypeViewModel>();
            foreach (var item in fRoomTypes)
            {
                string hotelName = db.Hotels.Find(item.HotelId).Name;
                int locationId = db.Hotels.Find(item.HotelId).LocationId;
                string locationName = db.Locations.Find(locationId).Name;

                fRoomTypeViews.Add(new RoomTypeViewModel
                {
                    RoomType = item,HotelName = hotelName,LocationName = locationName
                });
            }

            var viewmodel = new HomeViewModel();
            viewmodel.FRoomTypeViews = fRoomTypeViews;
            viewmodel.LocationGroups = locationGroups;

            return View(viewmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Location(int id)
        {
            List<HotelModel> Hotels = new List<HotelModel>();
            List<HotelListItemModel> Items = new List<HotelListItemModel>();

            Hotels = db.Hotels.Where(x => x.LocationId == id).ToList();
            for (int i = 0; i < Hotels.Count; i++)
            {
                HotelListItemModel item = new HotelListItemModel();
                item.Name = Hotels.ElementAt(i).Name;
                item.HotelId = Hotels.ElementAt(i).HotelId;
                item.Rate = Hotels.ElementAt(i).Rate;
                decimal minPrice = db.RoomTypes.Where(h => h.HotelId == item.HotelId).ToList().Min(r => r.PricePerNight);
                item.RoomPrice = minPrice;

                Items.Add(item);
            }

            ViewBag.LocationName = db.Locations.Find(id).Name;
            return View(Items);
        }

    }
}
