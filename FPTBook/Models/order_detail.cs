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
    
    public partial class order_detail
    {
        public int order_id { get; set; }
        public string book_id { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    
        public virtual book book { get; set; }
        public virtual order order { get; set; }
    }
}