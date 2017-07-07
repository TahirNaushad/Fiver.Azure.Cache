using System;

namespace Fiver.Azure.Cache
{
    public class AzureCacheSettings
    {
        public AzureCacheSettings(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("ConnectionString");

            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
