using Meteorite.Data.Models;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Meteorite.Jobs.Extensions
{
    public static class HashingExtensions
    {
        public static IEnumerable<MeteoriteDb> SetHashCodes(this IEnumerable<MeteoriteDb> meteorites)
        {
            foreach (var meteorite in meteorites)
            {
                meteorite.HashCode = meteorite.GetSha256Hash();
            }

            return meteorites;
        }

        public static string GetSha256Hash(this object instance)
        {
            var cryptoServiceProvider = SHA256.Create();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm
        {
            DataContractSerializer serializer = new DataContractSerializer(instance.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }
    }
}
