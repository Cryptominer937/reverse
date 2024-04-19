using System;
using System.Collections.Generic;
using HashKingsMiner.Enums;

namespace HashKingsMiner.Miners.Parsing
{
    public class MinerOptionPackage
    {
        public string Name;
        public MinerType Type;
        public List<MinerOption> GeneralOptions;
        public List<MinerOption> TemperatureOptions;

        public MinerOptionPackage(MinerType iType, List<MinerOption> iGeneralOptions, List<MinerOption> iTemperatureOptions)
        {
            Type = iType;
            GeneralOptions = iGeneralOptions;
            TemperatureOptions = iTemperatureOptions;
            Name = Enum.GetName(typeof(MinerType), iType);
        }
    }
}