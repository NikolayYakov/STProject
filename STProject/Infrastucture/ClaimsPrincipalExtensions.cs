using System.Security.Claims;

namespace STProject.Infrastucture
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.Identity.Name != null ? user.FindFirst(ClaimTypes.NameIdentifier).Value : null;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole("Administrator");
    }


}
