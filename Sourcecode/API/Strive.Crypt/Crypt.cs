using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("தமிழ்*"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var encryptingCreds = new EncryptingCredentials(key1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            return new CryptoDetails() { EnCredentials = encryptingCreds, SignCredentials = creds };
        }

        public CryptoDetails GetDecryptionStuff(string gSecretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(gSecretKey));
            var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("தமிழ்*"));
            return new CryptoDetails() { SignKey = key, DecryptKey = key1 };
        }
    }
}
