namespace SaeedPay76.Common.Helper
{
    public class Utilities
    {
        public static void CreatePassWordHash(string passWord,out byte[] PassWordHash,out byte[] passwordSalt)
        {
            using(var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hamc.Key;
                PassWordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
            }
        }
    }
}
