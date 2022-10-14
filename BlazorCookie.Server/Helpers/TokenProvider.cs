namespace BlazorCookie.Server.Helpers
{
    // We use this class in host as host is an ASP.NET Core MVC razor page.
    // Blazor doesn`t allow access to the HttpContext because of SignalR so this is a workaround for passing the XSRF token to blazor
    // Host is running on the regular Asp.net core context
    public class TokenProvider
    {
        public string? XsrFToken { get; set; }

        public string? Cookie { get; set; } // This is used to pass it to our controllers so that we can successfully manage the api calls
    }

    public class InitialApplicationState
    {
        public string? XsrFToken { get; set; }

        public string? Cookie { get; set; }
    }
}
