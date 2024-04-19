using HashKingsMiner.Configs.Data;

namespace HashKingsMiner.Configs.ConfigJsonFile
{
    public class GeneralConfigFile : ConfigFile<GeneralConfig>
    {
        public GeneralConfigFile()
            : base(FOLDERS.CONFIG, "General.json", "General_old.json")
        {
        }
    }
}