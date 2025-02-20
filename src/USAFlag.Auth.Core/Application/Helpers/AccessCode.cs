using System;

namespace USAFlag.Auth.Core.Application.Helpers
{
    public class AccessCode
    {
        public static int GenerateSecurityCode(int startDigit = 100000, int endDigit = 999999)
        {
            Random random = new Random();
            return random.Next(startDigit, endDigit);
        }

    }
    public enum EnumEmailTemplate
    {
        Access_Code = 1,
        Forgot_Password = 2,
        Reset_Password = 3
    }
}
