namespace Assignment2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            Ordersses = new HashSet<Orderss>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Department_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Grocery_dept { get; set; }

        [StringLength(255)]
        public string Bakery_dept { get; set; }

        public int? Total_No_Of_Products { get; set; }

        [Column(TypeName = "money")]
        [Range(0, 100000, ErrorMessage ="Price cannot be negative")]
        [DataType(DataType.Currency)]
        public decimal? Price_of_Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orderss> Ordersses { get; set; }
    }
}
