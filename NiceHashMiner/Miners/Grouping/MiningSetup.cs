using NiceHashMinerLegacy.Common.Enums;
using System.Collections.Generic;

namespace NiceHashMiner.Miners.Grouping
{
    public class MiningSetup
    {
        public List<MiningPair> MiningPairs { get; }
        public string MinerPath { get; }
        public string MinerName { get; }
        public string AlgorithmName { get; }
        public AlgorithmType CurrentAlgorithmType { get; }
        public AlgorithmType CurrentSecondaryAlgorithmType { get; }
        public bool IsInit { get; }

        public MiningSetup(List<MiningPair> miningPairs)
        {
            IsInit = false;
            CurrentAlgorithmType = AlgorithmType.NONE;
            if (miningPairs == null || miningPairs.Count <= 0) return;
            MiningPairs = miningPairs;
            MiningPairs.Sort((a, b) => a.Device.ID - b.Device.ID);
            MinerName = miningPairs[0].Algorithm.MinerBaseTypeName;
            AlgorithmName = miningPairs[0].Algorithm.AlgorithmName;
            CurrentAlgorithmType = miningPairs[0].Algorithm.NiceHashID;
            CurrentSecondaryAlgorithmType = miningPairs[0].Algorithm.SecondaryNiceHashID;
            MinerPath = miningPairs[0].Algorithm.MinerBinaryPath;
            IsInit = MinerPaths.IsValidMinerPath(MinerPath);
        }
    }
}
