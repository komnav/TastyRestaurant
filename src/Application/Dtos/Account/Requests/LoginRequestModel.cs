using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Account.Requests
{
    public class LoginRequestModel
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
