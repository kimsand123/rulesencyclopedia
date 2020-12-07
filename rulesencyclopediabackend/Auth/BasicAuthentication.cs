using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace rulesencyclopediabackend.Auth
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        //Implementing AuthorizationFilterAttribute
        //Checking token validity as attribute which is 
        //annotated upon the relevant methods, which are all,
        //except during login, as one has not recieved a token
        //at that time in the process.

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //if there is a token.
            if (actionContext.Request.Headers.Authorization != null)
            {
                //Getting the authToken from the actionContext
                var authToken = actionContext.Request.Headers.Authorization.Parameter;
                //check token, if it is not valid
                if (!(CheckToken.Instance.doCheckToken(authToken)))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token is not valid, login again or create a user");
                }
            }
            //If there is no token.
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "No Auth token in request.");
            } 
        }
    }
}