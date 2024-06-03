namespace EntitiesLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Administrator")]
    public partial class Administrator
    {
        public int id { get; set; }

        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string surname { get; set; }

        [Required]
        [StringLength(45)]
        public string username { get; set; }

        [Required]
        [StringLength(45)]
        public string password { get; set; }
    }
}
