using System.Security.Cryptography;
using System.Text;

namespace TFE.Tools
{
    //x <summary>
    //! Classe regroupant les actions de cryptage
    //x </summary>
    public class Encryption
    {
        //x <summary>
        //! Méthode permettant de convertir le mot de passe avec le SHA256
        //x </summary>
        //! <param name="input">Donnée à crypter</param>
        public static string GetSHA256(string input)
        {
            try
            {
                using (SHA256 Sha256 = SHA256.Create())
                {
                    byte[] h = Sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < h.Length; i++)
                        sb.Append(h[i].ToString("x2"));
                    return sb.ToString();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
