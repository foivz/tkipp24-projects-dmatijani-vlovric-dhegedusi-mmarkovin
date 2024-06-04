namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Reservation")]
    public partial class Reservation
    {
        [Key]
        public int idReservation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? reservation_date { get; set; }

        public int? Member_id { get; set; }

        public int Book_id { get; set; }

        public virtual Book Book { get; set; }

        public virtual Member Member { get; set; }
    }
}
