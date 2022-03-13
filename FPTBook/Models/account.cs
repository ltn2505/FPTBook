﻿//------------------------------------------------------------------------------
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

    public partial class account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public account()
        {
            this.orders = new HashSet<order>();
        }
        [Required(ErrorMessage = "User name cannot be left blank")]
        [Display(Name = "User name")]
        public string acc_name { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be left blank")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password cannot be left blank")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The new password and confirmation new password do not match.")]
        public string confirmpass { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name be left blank")]
        public string full_name { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender be left blank")]
        public string gender { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be left blank")]
        [EmailAddress(ErrorMessage = "Not an email address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email address is invalid")]
        public string email { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address cannot be left blank")]
        
        public string address { get; set; }

        public string state { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
    }
}
