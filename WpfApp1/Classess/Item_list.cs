namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_list
    {
        [Key]
        public int id_List { get; set; }

        public int id_Order { get; set; }

        public int id_Item { get; set; }

        public int Quantity { get; set; }

        public virtual Item Item { get; set; }

        public virtual Orders Order { get; set; }
    }
}
