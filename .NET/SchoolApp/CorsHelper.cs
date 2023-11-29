using System.Text.RegularExpressions;

namespace SchoolApp;

public static class CorsHelper
{

    public static bool IsCorsOriginAllowed(string url, IEnumerable<string> allowedHosts)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            return false;
        }

        foreach (var host in allowedHosts)
        {
            if (Regex.IsMatch(uri.Host, host))
            {
                return true;
            }
        }

        return false;
    }
}