using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
