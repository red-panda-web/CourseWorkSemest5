namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orders")]
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Item_list = new HashSet<Item_list>();
        }

        [Key]
        public int id_Order { get; set; }

        public int id_Client { get; set; }

        public int id_Employee { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Order_cost { get; set; }

        public int Discount { get; set; }

        public int? id_Certificate { get; set; }

        public int? id_Delivery { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual Client Client { get; set; }

        public virtual Delivery Delivery { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item_list> Item_list { get; set; }
    }
}
