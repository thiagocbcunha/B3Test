using System.Text;
using B3.Test.Library.Contracts;
using System.Security.Cryptography;

namespace B3.Test.Library.Security;

public class EnterpriseSecurity : IEnterpriseSecurity
{
    public string GetHash(string value)
    {
        var tmpSource = Encoding.ASCII.GetBytes(value);
        return ByteArrayToString(MD5.HashData(tmpSource));
    }

    private string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length - 1; i++)
            sOutput.Append(arrInput[i].ToString("X2"));

        return sOutput.ToString();
    }
}
