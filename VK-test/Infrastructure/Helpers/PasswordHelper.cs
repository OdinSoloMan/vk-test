using System.Text;

namespace Infrastructure.Helpers
{
    public static class PasswordHelper
    {
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка в base64Encode" + ex.Message);
            }
        }
    }
}
