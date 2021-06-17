namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class san_pham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(15)]
        public string ID { get; set; }

        [Required]
        [StringLength(15)]
        public string ID_l { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int UnitCost { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        public int? Status { get; set; }

        public virtual loai_sp loai_sp { get; set; }
    }
}
