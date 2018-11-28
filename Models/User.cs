using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models
{
    public class MainUser
    {
        [Key]
        public int UserId {get; set;}

        [Required]
        [Display(Name="First Name")]
        [MinLength(2, ErrorMessage="First Name field must be a minimum length of 2 characters long.")]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2, ErrorMessage="Last Name field must be a minimum length of 2 characters long.")]
        [Display(Name="Last Name")]
        public string LastName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [Required]
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword {get; set;}

    }

    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name="Email")]
        public string EmailAttempt {get; set;}

        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string PasswordAttempt {get; set;}
    }
}