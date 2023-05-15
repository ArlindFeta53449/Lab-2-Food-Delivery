using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Authentification
{
    public interface IAuthentificationService
    {
        string EncryptString(string plainText, byte[] key, byte[] iv);
    }
}
