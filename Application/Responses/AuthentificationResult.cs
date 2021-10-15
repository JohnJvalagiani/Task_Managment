using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Responses
{
   public  class AuthentificationResult
    {
        private DateTime expirationDate;

        public AuthentificationResult(string token, DateTime expirationDate,bool success)
        {
            this.Success = success;
            Token = token;
            this.expirationDate = expirationDate;
        }

        public AuthentificationResult()
        {

        }

        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
