using AuthorizationWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationWebApp.ViewModels
{
	public class TableViewModel
	{
		public short OperationType { get; set; }
		public List<bool> checkList { get; set; }
		public List<User> Users { get; set; }
	}
}
