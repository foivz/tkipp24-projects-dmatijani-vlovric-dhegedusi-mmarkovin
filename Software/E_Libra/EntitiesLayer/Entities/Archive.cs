namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Archive")]
    public partial class Archive
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Book_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Employee_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime arhive_date { get; set; }

        public virtual Book Book { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
