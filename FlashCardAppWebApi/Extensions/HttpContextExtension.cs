namespace FlashCardAppWebApi.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            return httpContext.Items["userId"] as int? ??
                throw new ArgumentException("UserId not found in HttpContext");
        }
    }
}