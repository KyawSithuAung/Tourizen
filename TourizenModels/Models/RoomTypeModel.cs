using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Tourizen.Models
{
    [Table("RoomType")]
    public class RoomTypeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomTypeId { get; set; }

        public string Name { get; set; }

        public string RoomImageUrl { get; set; }

        public int GuestPerRoom { get; set; }

        public decimal PricePerNight { get; set; }

        public string RoomDescription { get; set; }

        public bool Cancellable { get; set; }

        public bool IsFeatured { get; set; }

        public int HotelId { get; set; }

    }

    public class CheckAvailablityModel
    {
        public int RoomTypeId { get; set; }

        [Required]
        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [Display(Name = "Number Of Night")]
        public int Night { get; set; }

        [Required]
        [Display(Name = "Number Of Room")]
        public int Room { get; set; }
    }
}