namespace FirstHandData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [Key]
        public int event_id { get; set; }

        [Required]
        [StringLength(50)]
        public string event_name { get; set; }

        [Required]
        [StringLength(50)]
        public string location { get; set; }
    }
}
