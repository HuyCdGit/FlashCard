using FlashCardAppWebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FlashCardAppWebApi.Attribute
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter))
        { }
    }
}