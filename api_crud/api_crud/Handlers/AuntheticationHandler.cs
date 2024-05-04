using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

using jsonCrud.Data;
using Microsoft.AspNetCore.Authentication;

using Microsoft.Extensions.Options;

namespace api_crud.Handlers
{
	public class BasicAuntheticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{

        //private readonly UserController _userController;
        private readonly UserDBHandler _userDbHandler;

        public BasicAuntheticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, UserDBHandler userDb )
			:base(options,logger,encoder,clock)
		{
            _userDbHandler = userDb;
		}

		//protected string user = "hello";
		//protected string pass = "123";

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {

			if (!Request.Headers.ContainsKey("Authorization")) {
				return AuthenticateResult.Fail("Authorization Header not found");
			}

			try {
				var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
				var bytes = Convert.FromBase64String(authenticationHeader.Parameter);
				string credential = System.Text.Encoding.UTF8.GetString(bytes);
				string[] parts = credential.Split(new[] { ':' }, StringSplitOptions.None);
                string username = parts[0];
                string password = parts[1];

				var user_cred = await _userDbHandler.GetUserByUsername(username);

				if(user_cred == null || user_cred.Password != password ) {

                    return AuthenticateResult.Fail("Invalid Username or Password");


                }

				else {

					var claims = new[] {
						new Claim(ClaimTypes.Name,username),
                        new Claim(ClaimTypes.Role, user_cred.Role)


                    };

					var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
				

            }
            catch
			{
                return AuthenticateResult.Fail("Invalid authorization header");
            }


		}
	}
}

