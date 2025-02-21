namespace USAFlag.Auth.Core.Application.Extension;

public static class GuidExtensions
{
    public static string GenerateUniqueId(this Guid guid)
    {
        long i = 1;
        foreach (byte b in guid.ToByteArray())
        {
            i *= b + 1;
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }
}
