using NiceHashMinerLegacy.Common.Enums;
using System;

namespace NiceHashMiner
{
    /// <summary>
    /// AlgorithmNiceHashNames class is just a data container for mapping NiceHash JSON API names to algo type
    /// </summary>
    public static class AlgorithmNiceHashNames
    {
        public static string GetName(AlgorithmType type)
        {
            return Enum.GetName(typeof(AlgorithmType), type);
        }
    }
}