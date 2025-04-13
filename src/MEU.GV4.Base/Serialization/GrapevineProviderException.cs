namespace MEU.GV4.Base.Serialization;

public class GrapevineProviderException : Exception
{
    public GrapevineProviderException(string message) : base(message) { }
    public GrapevineProviderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
