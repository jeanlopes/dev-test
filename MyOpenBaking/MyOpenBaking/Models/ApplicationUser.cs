using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace MyOpenBaking.Models
{
    public class ApplicationUser : MongoIdentityUser<Guid>
	{
		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
	}
}
