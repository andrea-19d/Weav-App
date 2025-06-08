using System.Security.Cryptography;
using System.Text;

namespace Weav_App.Helpers;

public class NanoidGenerator
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public static string Generate(int length)
    {
        var buffer = new byte[length];
        _rng.GetBytes(buffer);

        var nanoid = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            nanoid.Append(Alphabet[buffer[i] & 63]);
        }

        return nanoid.ToString();
    }
}