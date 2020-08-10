using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Crypto
{    
    public class CryptoDetails
    {
        public SigningCredentials SignCredentials { get; set; }
        public EncryptingCredentials EnCredentials { get; set; }

        public SymmetricSecurityKey DecryptKey { get; set; }
        public SymmetricSecurityKey SignKey { get; set; }
    }

    public class Crypt
    {
        public CryptoDetails GetEncryptionStuff(string gSecretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(gSecretKey));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var encryptingCreds = new EncryptingCredentials(key1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            return new CryptoDetails() { EnCredentials = encryptingCreds, SignCredentials = creds };
        }

        public CryptoDetails GetDecryptionStuff(string gSecretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(gSecretKey));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return new CryptoDetails() { SignKey = key, DecryptKey = key1 };
        }

        public static string Encrypt(string plainText)
        {
            string encryptString = string.Empty;
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new
                    Rfc2898DeriveBytes(secretKey, new byte[]
                    { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;

            //byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            //SecureRandom random = new SecureRandom();
            //byte[] iv = new byte[16];
            //random.NextBytes(iv);

            //string keyStringBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(secretKey));

            ////Set up
            //AesEngine engine = new AesEngine();
            //CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
            //PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine), new Pkcs7Padding());
            //KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(keyStringBase64));
            //ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 16);

            //// Encrypt
            //cipher.Init(true, keyParamWithIV);
            //byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            //int length = cipher.ProcessBytes(inputBytes, outputBytes, 0);
            //cipher.DoFinal(outputBytes, length); //Do the final block
            //return Convert.ToBase64String(outputBytes);
        }

        public static string Decrypt(string input)
        {
            string decryptString = string.Empty;
            byte[] cipherBytes = Convert.FromBase64String(input);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new
                    Rfc2898DeriveBytes(secretKey, new byte[]
                    { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    decryptString = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return decryptString;

            //string keyStringBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(secretKey));
            //byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            ////byte[] iv = Convert.FromBase64String(iv_base64);
            //byte[] iv = Convert.FromBase64String(input);
            ////Set up
            //AesEngine engine = new AesEngine();
            //CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
            //PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine), new Pkcs7Padding());
            //KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(keyStringBase64));
            //ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 16);

            ////Decrypt            
            //byte[] outputBytes = Convert.FromBase64String(input);
            //cipher.Init(false, keyParamWithIV);
            //byte[] comparisonBytes = new byte[cipher.GetOutputSize(outputBytes.Length)];
            //int length = cipher.ProcessBytes(outputBytes, comparisonBytes, 0);
            //cipher.DoFinal(comparisonBytes, length); //Do the final block
            //return Encoding.UTF8.GetString(comparisonBytes, 0, comparisonBytes.Length);
        }

        public const string secretKey = "தமிழ்*";
    }
}
