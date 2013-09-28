using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tourizen.Models;
using Tourizen.ViewModels;

namespace Tourizen.Controllers
{
    public class HotelController : Controller
    {
        private MainDBContext db = new MainDBContext();

        //
        // GET: /Hotel/
        public ActionResult Hotel(int id)
        {
            var model = new HotelViewModel();
            model.Hotel = db.Hotels.Find(id);
            model.RoomTypes = db.RoomTypes.Where(x => x.HotelId == id).ToList();
            
            return View(model);
        }

        //
        // GET: /Room/

        public ActionResult Room(int id)
        {
            var model = new RoomViewModel();

            RoomTypeModel roomType = db.RoomTypes.Find(id);
            string hotelName = db.Hotels.Find(roomType.HotelId).Name;
            int locationId = db.Hotels.Find(roomType.HotelId).LocationId;
            string locationName = db.Locations.Find(locationId).Name;

            model.Room = new RoomTypeViewModel
            {
                RoomType = roomType,HotelName = hotelName,LocationName = locationName
            };

            model.Check = new CheckAvailablityModel()
            {
                RoomTypeId = id, CheckIn = DateTime.Now, Night = 1,Room = 1
            };
            return View(model);
        }

        //
        // POST: /Room/

        [HttpPost]
        public ActionResult Room(CheckAvailablityModel model)
        {
            RoomTypeModel roomType = db.RoomTypes.Find(model.RoomTypeId);

            if (ModelState.IsValid)
            {
                //init check in, check out date
                DateTime checkIn = model.CheckIn;
                DateTime checkOut = checkIn.AddDays(model.Night);

                List<int> AvailableUnitIds = CheckAvailableUnit(model.RoomTypeId, checkIn, checkOut);

                if(AvailableUnitIds.Count >= model.Room)
                {
                        List<RoomUnitModel>RoomToBook = new List<RoomUnitModel>();
                        for(int i = 0;i<model.Room;i++)
                        {
                            RoomToBook.Add(db.RoomUnits.Find(AvailableUnitIds.ElementAt(i)));
                        }

                        var detail = new DetailModel();

                        HotelModel hotel = db.Hotels.Find(roomType.HotelId);

                        BookingModel booking = new BookingModel();
                        booking.CheckIn = checkIn;
                        booking.CheckOut = checkOut;
                        booking.NumberNight = model.Night;
                        booking.NumberRoom = model.Room;
                        booking.RoomTypeId = model.RoomTypeId;
                        booking.TotalCharge = roomType.PricePerNight * booking.NumberRoom * booking.NumberNight;
                        booking.BookingStatus = BookingStatus.NEW;

                        detail.Booking = booking;
                        detail.Rooms = RoomToBook;
                        detail.RoomType = roomType;
                        detail.Hotel = hotel;

                        Session["Detail"] = detail;

                        return RedirectToAction("Confirmation","Booking");

                }else
                {
                    string hotelName = db.Hotels.Find(roomType.HotelId).Name;
                    int locationId = db.Hotels.Find(roomType.HotelId).LocationId;
                    string locationName = db.Locations.Find(locationId).Name;

                    RoomViewModel vm = new RoomViewModel();
                    vm.Check = model;
                    vm.Room = new RoomTypeViewModel
                    {
                        RoomType = roomType,HotelName = hotelName,LocationName = locationName
                    };

                    return View(vm);
                }

            }
            else
            {
                string hotelName = db.Hotels.Find(roomType.HotelId).Name;
                int locationId = db.Hotels.Find(roomType.HotelId).LocationId;
                string locationName = db.Locations.Find(locationId).Name;

                RoomViewModel vm = new RoomViewModel();
                vm.Check = model;
                vm.Room = new RoomTypeViewModel
                {
                    RoomType = roomType,
                    HotelName = hotelName,
                    LocationName = locationName
                };

                return View(vm);
            }
        }

        //Check Booking Of Room For Availablity
        public List<int> CheckAvailableUnit(int RoomTypeId, DateTime CheckIn, DateTime CheckOut)
        {
            List<BookingModel> Bookings = db.Bookings.Where(r => r.RoomTypeId == RoomTypeId)
                                         .Where(i => i.CheckIn == CheckIn).Where(o => o.CheckOut == CheckOut).ToList();

            List<int> AllUnitIds = db.RoomUnits.Where(x => x.RoomTypeId == RoomTypeId).ToList().Select(x => x.RoomUnitId).ToList();
            if (Bookings.Count != 0)
            {
                for (int i = 0; i < Bookings.Count; i++)
                {
                    int id = Bookings.ElementAt(i).BookingId;
                    List<BookedUnit> BookedUnits = db.BookedUnit.Where(x => x.BookingId == id).ToList();
                    foreach (var bUnit in BookedUnits)
                    {
                        if (AllUnitIds.Contains(bUnit.UnitId))
                        {
                            AllUnitIds.Remove(bUnit.UnitId);
                        }
                    }
                }

                return AllUnitIds;
            }
            else
            {
                return AllUnitIds;
            }
        }

    }
}
