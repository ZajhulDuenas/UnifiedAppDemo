using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ClientApi
{
    public class ClientToken
    {
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }

        public DateTime TokenExpiresIn
        { get { return DateTime.Now.AddSeconds(ExpiresIn).AddSeconds(-3); } }
    }
}
