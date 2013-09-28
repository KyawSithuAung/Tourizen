using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourizen.Models
{
    [Table("RoomUnit")]
    public class RoomUnitModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomUnitId { get; set; }

        public string UnitNumber { get; set; }

        public int RoomTypeId { get; set; }

    }

    public class AvailableUnit
    {
        public int RoomUnitId { get; set; }
    }

}