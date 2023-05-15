﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Authentification
{
    public class ForgotPasswordEmailResponseDTO
    {
        public string EncryptedToken { get; set; }

        public string UserId { get; set; }
        public byte[] Key { get; set; } 
        public byte[] Iv { get; set; }
    }
}
