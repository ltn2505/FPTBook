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

    public partial class book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public book()
        {
            this.order_detail = new HashSet<order_detail>();
        }
        [Required(ErrorMessage = "Book id cannot be left blank")]
        [Display(Name = "Book id")]
        public string book_id { get; set; }

        [Required(ErrorMessage = "Book name cannot be left blank")]
        [Display(Name = "Book name")]
        public string book_name { get; set; }

        [Required(ErrorMessage = "Category id cannot be left blank")]
        [Display(Name = "Category id")]
        public string cate_id { get; set; }

        [Required(ErrorMessage = "Price cannot be left blank")]
        [Display(Name = "Price")]
        [Range(1, 1000)]
        public int book_price { get; set; }

        [Required(ErrorMessage = "Quantity cannot be left blank")]
        [Display(Name = "Quantity")]
        [Range(1, 1000)]
        public int book_quantity { get; set; }

        [Required(ErrorMessage = "Image cannot be left blank")]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public string book_picture { get; set; }
        [Required(ErrorMessage = "Author cannot be left blank")]
        [Display(Name = "Author")]
        public string book_author { get; set; }
        [Required(ErrorMessage = "Description cannot be left blank")]
        [Display(Name = "Description")]
        public string book_description { get; set; }
    
        public virtual category category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_detail> order_detail { get; set; }
    }
}
