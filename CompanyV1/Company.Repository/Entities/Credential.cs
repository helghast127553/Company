namespace Company.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Credential")]
    public partial class Credential
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string UserGroupID { get; set; }

        [StringLength(50)]
        public string RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
