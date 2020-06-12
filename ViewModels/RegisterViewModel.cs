using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationWebApp.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Wrong password!")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}
