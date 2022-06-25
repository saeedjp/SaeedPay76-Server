namespace SaeedPay76.Common.Helper
{
    public class Utilities
    {
        public static void CreatePassWordHash(string passWord, out byte[] PassWordHash, out byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hamc.Key;
                PassWordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
            }
        }
        public static bool VerifyPasswordHash(string passWord, byte[] PassWordHash, byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passWord));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PassWordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
