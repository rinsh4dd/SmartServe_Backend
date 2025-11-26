using System.Security.Claims;

namespace SmartServe.Application.Helpers
{
    public static class ClaimsHelper
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(id))
                return int.Parse(id);

            id = user.FindFirst("UserId")?.Value;
            if (!string.IsNullOrEmpty(id))
                return int.Parse(id);

            id = user.FindFirst("Id")?.Value;
            if (!string.IsNullOrEmpty(id))
                return int.Parse(id);

            id = user.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(id))
                return int.Parse(id);

            throw new Exception("UserId claim is missing in JWT.");
        }

        public static string GetRole(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value
                ?? user.FindFirst("role")?.Value
                ?? "Unknown";
        }
        public static string GetUserName(ClaimsPrincipal user)
        {
            return user.FindFirst("UserName")?.Value;
        }

        public static string GetEmail(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value
                ?? user.FindFirst("UserEmail")?.Value
                ?? string.Empty;
        }

        public static LoggedInUser GetUser(ClaimsPrincipal user)
        {
            return new LoggedInUser
            {
                UserId = int.Parse(user.FindFirst("UserId").Value),
                Role = user.FindFirst(ClaimTypes.Role)?.Value ?? "Unknown"
            };
        }


    }
}
