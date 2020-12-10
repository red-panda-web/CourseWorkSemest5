namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Item_list = new HashSet<Item_list>();
            Supply_list = new HashSet<Supply_list>();
        }

        [Key]
        public int id_Item { get; set; }

        public int id_Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Untits { get; set; }

        [Required]
        [StringLength(255)]
        public string Photo { get; set; }

        public int? Guarantee { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        public virtual Item_type Item_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item_list> Item_list { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supply_list> Supply_list { get; set; }
    }
}
