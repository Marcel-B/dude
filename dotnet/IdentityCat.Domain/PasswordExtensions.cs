using System.Security.Cryptography;

namespace IdentityCat.Domain;

public static class PasswordExtensions
{
    internal static byte[] GenerateSalt(
        int size)
        => RandomNumberGenerator.GetBytes(size);

    public static string Hash(
        this string password,
        byte[] salt)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Array.Copy(passwordBytes, saltedPassword, passwordBytes.Length);
        Array.Copy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

        var hash = SHA256.HashData(saltedPassword);
        return Convert.ToBase64String(hash);
    }

    public static bool Verify(
        this string enteredPassword,
        byte[] salt,
        string storedHashedPassword)
    {
        var hashedEnteredPassword = enteredPassword.Hash(salt);
        return hashedEnteredPassword == storedHashedPassword;
    }

    public static bool Verify(
        this string enteredPassword,
        string salt,
        string storedHashedPassword)
    {
        var hashedEnteredPassword = enteredPassword.Hash(Convert.FromBase64String(salt));
        return hashedEnteredPassword == storedHashedPassword;
    }
}