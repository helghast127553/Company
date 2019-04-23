namespace Company.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        public int EquipmentID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Status { get; set; }

        public int? EmployeeID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
