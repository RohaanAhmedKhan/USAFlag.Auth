using System.Security.Claims;
using System.Security.Principal;
using USAFlag.Auth.Core.Domain.Constants;

namespace USAFlag.AuthAPI.Extension
{
    public static class IdentityExtensions
    {
        public const string TenantId = "http://schemas.microsoft.com/identity/claims/tenantid";

        public static int? GetTenantId(this IIdentity identity)
        {
            var ident = identity as ClaimsIdentity;
            if (ident != null)
            {
                var claim = ident.FindFirst(TenantId);
                if (claim != null)
                    return int.Parse(claim.Value);
            }
            return null;
        }

        public static int? GetUserId(this IIdentity identity)
        {
            var ident = identity as ClaimsIdentity;
            if (ident != null)
            {
                var claim = ident.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                    return int.Parse(claim.Value);
            }

            return null;
        }


        public static int? GetUserRoleId(this IIdentity identity)
        {
            var ident = identity as ClaimsIdentity;
            if (ident != null)
            {
                var claim = ident.FindFirst(CustomClaimTypes.RoleId);
                if (claim != null)
                    return int.Parse(claim.Value);
            }

            return null;
        }
    }
}
