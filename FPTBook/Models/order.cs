//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.order_detail = new HashSet<order_detail>();
        }
        [Display(Name = "Order id")]
        public int order_id { get; set; }
        [Display(Name = "User name")]
        public string acc_name { get; set; }
        [Display(Name = "Receiver's name")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string receiver_name { get; set; }
        [Display(Name = "Number phone")]
        [Phone]
        [Required]
        [StringLength(11, MinimumLength = 9)]
        public string phone { get; set; }
        [Display(Name = "Order date")]
        public System.DateTime order_date { get; set; }
        [Display(Name = "Address")]
        [Required]
        [StringLength(100, MinimumLength = 5)]

        public string delivery_address { get; set; }
        [Display(Name = "Total price")]
        public int total_price { get; set; }
    
        public virtual account account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_detail> order_detail { get; set; }
    }
}
