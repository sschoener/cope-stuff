#region

using System.Security.Principal;

#endregion

namespace cope
{
    public static class User
    {
        /// <summary>
        /// Returns the current user's name or null if there's no current user.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserName()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
                return identity.Name;
            return null;
        }

        public static bool IsCurrentUserSystem()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
                return identity.IsSystem;
            return false;
        }

        public static bool IsCurrentUserAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }

        public static bool IsCurrentUserInRole(WindowsBuiltInRole role)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(role);
            }
            return false;
        }
    }
}