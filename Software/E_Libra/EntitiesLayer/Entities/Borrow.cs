namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Borrow")]
    public partial class Borrow
    {
        [Key]
        public int idBorrow { get; set; }

        [Column(TypeName = "date")]
        public DateTime borrow_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? return_date { get; set; }

        public int borrow_status { get; set; }

        public int Book_id { get; set; }

        public int? Member_id { get; set; }

        public int Employee_borrow_id { get; set; }

        public int? Employee_return_id { get; set; }

        public virtual Book Book { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Member Member { get; set; }
    }
}
