using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HoGi.ToolsAndExtensions.Extensions
{
    public static class AccountClaimsExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.Claims.Where(w => w.Type == "sub").Select(s => new Guid(s.Value)).FirstOrDefault();
            return userId;
        }

        public static long[] GetFunds(this ClaimsPrincipal user)
        {
            return user.Claims.Where(w => w.Type == "fund-codes")
                            .Select(s => s.Value)
                            .FirstOrDefault()
                            ?.Split(',')
                            .Where(fund => !string.IsNullOrEmpty(fund))
                            .Select(s => Convert.ToInt64(s.Trim()))
                            .ToArray() ?? new long[] { };
        }

        public static long[] GetBrokers(this ClaimsPrincipal user)
        {
            return user.Claims.Where(w => w.Type == "broker-codes")
                .Select(s => s.Value)
                .FirstOrDefault()
                ?.Split(',')
                .Where(fund => !string.IsNullOrEmpty(fund))
                .Select(s => Convert.ToInt64(s.Trim()))
                .ToArray() ?? new long[] { };
        }
        public static (long SessionId, long Gateway) GetSession(this ClaimsPrincipal user)
        {
            var claims = user.Claims.Where(w => w.Type is "session-id" or "session-gateway")
                .Select(s => new { s.Value, s.Type })
                .ToList();

            var sessionId = claims.Where(c => c.Type == "session-id").Select(s => s.Value).FirstOrDefault() ?? "-1";

            var gateway = claims.Where(c => c.Type == "session-gateway")
                .Select(s => string.IsNullOrEmpty(s.Value) ? "0" : s.Value).FirstOrDefault() ?? "0";

            return (SessionId: long.Parse(sessionId), Gateway: long.Parse(gateway));

        }

        public static List<string> GetUserRoles(this ClaimsPrincipal user)
        {
            return user.Claims.Where(w => w.Type == "role")
                   .Select(s => s.Value).ToList();
        }

        public static string GetNationalCode(this ClaimsPrincipal user)
        {
            return user.Claims.Where(w => w.Type == "national-code")
                               .Select(s => s.Value)
                               .FirstOrDefault() ?? "";
        }


        public static string GetCurrentIP(this ClaimsPrincipal user)
        {
            return user.Claims.Where(w => w.Type == "ip")
                               .Select(s => s.Value)
                               .FirstOrDefault() ?? "";
        }
        public static bool GetPasswordExpired(this ClaimsPrincipal user)
        {
            var value = user.Claims.Where(w => w.Type == "password-expired")
                               .Select(s => s.Value)
                               .FirstOrDefault() ?? "false";
            _ = bool.TryParse(value, out var passwordExpired);
            return passwordExpired;
        }
        public static string GetClientId(this ClaimsPrincipal user)
        {
            var value = user.Claims.Where(w => w.Type == "client-id")
                               .Select(s => s.Value)
                               .FirstOrDefault() ?? "";
            return value;
        }
        public static (long Id, string Name) GetBranchInfo(this ClaimsPrincipal user)
        {
            var branchIdClaim = user.Claims
                             .Where(x => x.Type == "branch-id")
                             .Select(x => x.Value)
                             .FirstOrDefault() ?? "0";


            var branchName = user.Claims
                                 .Where(x => x.Type == "branch-name")
                                 .Select(x => x.Value)
                                 .FirstOrDefault() ?? string.Empty;


            _ = long.TryParse(branchIdClaim, out var branchId);

            return (branchId, branchName);
        }

        public static bool? GetTowFactorEnabled(this ClaimsPrincipal user)
        {
            var towFactorEnabled = user.Claims.Where(w => w.Type == "tow-factor-enabled")
                       .Select(s => s.Value)
                       .FirstOrDefault() ?? "";
            bool.TryParse(towFactorEnabled, out var result);
            return result;
        }

        public static string GetForgeryToken(this ClaimsPrincipal user)
        {
            return user.Claims.Where(x => x.Type == "forgery-token")
                 .Select(s => s.Value)
                 .FirstOrDefault() ?? "";


        }

    }
}
