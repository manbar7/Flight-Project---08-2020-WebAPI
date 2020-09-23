using ClassLibrary1;
using ClassLibrary1.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.Controllers
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {


        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    "You must enter user name + password");
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));

                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                FlyingCenterSystem fcs = FlyingCenterSystem.GetInstance();
                ILoginToken loginToken = fcs.Login(username, password);
                FacadeBase facade = fcs.GetFacade(loginToken);
                int count = 0;
                List<string> blockedUsernames = new List<string>();
                if (blockedUsernames.Contains(username))
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized, "You are not allowed");
                }
                else
                {

                    if (loginToken.GetType() == typeof(LoginToken<Administrator>))
                    {
                        // LoginToken<Administrator> token = (LoginToken<Administrator>)loginToken;
                        // LoggedInAdministratorFacade LogFacade = (LoggedInAdministratorFacade)facade;
                        actionContext.Request.Properties["AdminUser"] = loginToken;
                        actionContext.Request.Properties["AdminFacade"] = facade;
                    }
                    else if (loginToken.GetType() == typeof(LoginToken<AirlineCompany>))
                    {
                        actionContext.Request.Properties["AirlineUser"] = loginToken;
                        actionContext.Request.Properties["AirlineFacade"] = facade;
                    }
                    else if (loginToken.GetType() == typeof(LoginToken<Customer>))
                    {
                        actionContext.Request.Properties["CustomerUser"] = loginToken;
                        actionContext.Request.Properties["CustomerFacade"] = facade;
                    }

                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateResponse(HttpStatusCode.Unauthorized, "You are not allowed");
                        count++;
                        if (count >= 3)
                        {
                            actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "you reached maximum number of logins, you are now blocked");
                            blockedUsernames.Add(username);
                            count = 0;
                        }



                    }
                }

            }
        }
    }
}