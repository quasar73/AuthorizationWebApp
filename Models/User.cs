using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationWebApp.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
		public DateTime RegistrationDate { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public bool? Status { get; set; }
	}
}
