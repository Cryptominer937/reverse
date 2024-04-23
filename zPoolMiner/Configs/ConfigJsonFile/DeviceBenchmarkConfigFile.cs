
using System;
using HashKingsMiner.Configs.Data;

namespace HashKingsMiner.Configs.ConfigJsonFile
{
    public class DeviceBenchmarkConfigFile : ConfigFile<DeviceBenchmarkConfig>
    {
        private const string BENCHMARK_PREFIX = "benchmark_";

        public DeviceBenchmarkConfigFile(string DeviceUUID)
            : base(FOLDERS.CONFIG, GetName(DeviceUUID), GetName(DeviceUUID, "_OLD"))
        {
        }

        private static string GetName(string DeviceUUID, string old = "")
        {
            // make device name
            var invalid = new char[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
            var fileName = BENCHMARK_PREFIX + DeviceUUID.Replace(' ', '_');

            foreach (var c in invalid)
                fileName = fileName.Replace(c.ToString(), string.Empty);

            const string extension = ".json";
            return fileName + old + extension;
        }
    }
}