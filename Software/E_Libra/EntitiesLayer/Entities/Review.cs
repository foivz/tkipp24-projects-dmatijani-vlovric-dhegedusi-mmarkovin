namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Review")]
    public partial class Review
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Member_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Book_id { get; set; }

        [Column(TypeName = "text")]
        public string comment { get; set; }

        public int rating { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public virtual Book Book { get; set; }

        public virtual Member Member { get; set; }


    }
}
