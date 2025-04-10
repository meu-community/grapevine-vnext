using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEU.GV4.Data.Providers;

public class GrapevineProviderException : Exception
{
    public GrapevineProviderException(string message) : base(message) { }
    public GrapevineProviderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
