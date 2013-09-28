using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Tourizen.Models
{

    public class HotelDBContext : DbContext
    {
        public HotelDBContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<HotelModel> Hotels { get; set; }
    }
    
    [Table("Hotel")]
    public class HotelModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }


        public string Name { get; set; }

        public int Rate { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Website { get; set; }

        public string MerchantEmail { get; set; }

        public string Content { get; set; }

        public bool Status { get; set; }

        public int LocationId { get; set; }

    }

    public class HotelListItemModel
    {
        public int HotelId;
        public string Name;
        public int Rate;
        public string State;
        public decimal RoomPrice;
    }

}