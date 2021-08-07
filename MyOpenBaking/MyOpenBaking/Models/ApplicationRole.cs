using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace MyOpenBaking.Api.Models
{
    public class ApplicationRole : MongoIdentityRole<Guid>
	{
		public ApplicationRole() : base()
		{
		}

		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
