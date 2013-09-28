using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tourizen.Models
{
    [Table("Booking")]
    public class BookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }


        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int NumberNight { get; set; }

        public int NumberRoom { get; set; }

        public decimal TotalCharge { get; set; }

        public int GuestId { get; set; }

        public bool Paid { get; set; }

        public int BookingStatus { get; set; }

        public int RoomTypeId { get; set; }

    }

    public class DetailModel
    {
        public BookingModel Booking { get; set; }
        public RoomTypeModel RoomType { get; set; }
        public HotelModel Hotel { get; set; }
        public List<RoomUnitModel> Rooms { get; set; }

        public DetailModel()
        {
            Rooms = new List<RoomUnitModel>();
        }
    }

    [Table("BookedUnit")]
    public class BookedUnit 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int UnitId { get; set; }
    }

    public class BookingStatus
    {
        public static int NEW = 0; 
        public static int CONFIRM = 1;
        public static int CANCELLED = -1;
    }

}