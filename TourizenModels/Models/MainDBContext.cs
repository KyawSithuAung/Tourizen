using System.Data.Entity;

namespace Tourizen.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext()
            : base("DefaultConnection")
        { 
        }

        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<RoomTypeModel> RoomTypes { get; set; }
        public DbSet<RoomUnitModel> RoomUnits { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<BookedUnit> BookedUnit { get; set; }
        public DbSet<GuestModel> Guests { get; set; }
    }
}