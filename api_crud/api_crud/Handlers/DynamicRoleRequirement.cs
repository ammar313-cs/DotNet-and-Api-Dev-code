using System;
using Microsoft.AspNetCore.Authorization;

namespace api_crud.Handlers
{
	public class DynamicRoleRequirement: IAuthorizationRequirement
    {
		public DynamicRoleRequirement()
		{
		}
	}
}

