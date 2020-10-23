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
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;
                if (!(CheckToken.Instance.doCheckToken(authToken)))
                {
                    
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token is not valid, login again or create a user");
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "No Auth token in request.");
            } 
        }
    }
}