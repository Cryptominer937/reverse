using HashKingsMiner.Configs.Data;

namespace HashKingsMiner.Configs.ConfigJsonFile
{
    public class ApiCacheFile : ConfigFile<ApiCache>
    {
        public ApiCacheFile()
            : base(FOLDERS.CONFIG, "ApiCache.json", "ApiCache_old.json")
        {
        }
    }
}