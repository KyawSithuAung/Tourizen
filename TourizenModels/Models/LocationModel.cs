using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Tourizen.Models
{
    [Table("Location")]
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        public string Name { get; set; }
    }

    public class LocationDBContext : DbContext
    {
        public LocationDBContext()
            : base("DefaultConnection")
    {
    }

        public DbSet<LocationModel> Locations { get; set; }
    }
}