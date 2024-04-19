using System.Collections.Generic;
using HashKingsMiner.Configs;
using HashKingsMiner.Enums;
using HashKingsMiner.Miners.Grouping;
using HashKingsMiner.Miners.Parsing;

namespace HashKingsMiner.Miners
{
    public class ClaymoreNeoscryptMiner : ClaymoreBaseMiner
    {
        private bool isOld;

        private const string _LOOK_FOR_START = "NS - Total Speed:";
        private const string _LOOK_FOR_START_OLD = "hashrate =";

        public ClaymoreNeoscryptMiner()
            : base("ClaymoreNeoscryptMiner", _LOOK_FOR_START)
        {
        }

        protected override double DevFee() => 5.0;

        protected override string GetDevicesCommandString()
        {
            if (!isOld) return base.GetDevicesCommandString();

            var extraParams = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var deviceStringCommand = "";
            var ids = new List<string>();

            foreach (var mPair in MiningSetup.MiningPairs)
            {
                var id = mPair.Device.ID;
                ids.Add(id.ToString());
            }

            deviceStringCommand += string.Join("", ids);

            return deviceStringCommand + extraParams;
        }

        public override void Start(string url, string btcAddress, string worker)
        {
            var username = GetUsername(btcAddress, worker);

            if (MiningSession.DONATION_SESSION)
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("nicehash.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("zergpool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("blazepool.com"))
                {
                    btcAddress = Globals.DemoUser;
                    worker = "c=DOGE,ID=Donation";
                }

                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = "cryptominer.Devfee";
                    worker = "x";
                }
                else
                {
                    btcAddress = Globals.DemoUser;
                }
            }
            else
            {
                if (url.Contains("zpool.ca"))
                {
                    btcAddress = HashKingsMiner.Globals.GetzpoolUser();
                    worker = HashKingsMiner.Globals.GetzpoolWorker();
                }

                if (url.Contains("ahashpool.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GetahashUser();
                    worker = HashKingsMiner.Globals.GetahashWorker();
                }

                if (url.Contains("hashrefinery.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GethashrefineryUser();
                    worker = HashKingsMiner.Globals.GethashrefineryWorker();
                }

                if (url.Contains("nicehash.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GetnicehashUser();
                    worker = HashKingsMiner.Globals.GetnicehashWorker();
                }

                if (url.Contains("zergpool.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GetzergUser();
                    worker = HashKingsMiner.Globals.GetzergWorker();
                }

                if (url.Contains("minemoney.co"))
                {
                    btcAddress = HashKingsMiner.Globals.GetminemoneyUser();
                    worker = HashKingsMiner.Globals.GetminemoneyWorker();
                }

                if (url.Contains("blazepool.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GetblazepoolUser();
                    worker = HashKingsMiner.Globals.GetblazepoolWorker();
                }

                if (url.Contains("blockmasters.co"))
                {
                    btcAddress = HashKingsMiner.Globals.GetblockmunchUser();
                    worker = HashKingsMiner.Globals.GetblockmunchWorker();
                }

                if (url.Contains("miningpoolhub.com"))
                {
                    btcAddress = HashKingsMiner.Globals.GetMPHUser();
                    worker = HashKingsMiner.Globals.GetMPHWorker();
                }
            }

            url = Globals.GetLocationURL(AlgorithmType.NeoScrypt, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], NHMConectionType.STRATUM_TCP);

            LastCommandLine = " " + GetDevicesCommandString() + " -mport -" + ApiPort + " -pool " + url +
                              " -wal " + btcAddress + " -psw " + worker + " -dbg -1 -ftime 10 -retrydelay 5";

            ProcessHandle = _Start();
        }

        // benchmark stuff
        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            benchmarkTimeWait = time; // Takes longer as of v10
                                      // network workaround
            var url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], NHMConectionType.STRATUM_TCP);
            var btcAddress = "";
            var worker = "";

            if (url.Contains("zpool.ca"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("ahashpool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("hashrefinery.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("nicehash.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("zergpool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("blockmasters.co"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("blazepool.com"))
            {
                btcAddress = Globals.DemoUser;
                worker = "c=DOGE,ID=Donation";
            }

            if (url.Contains("miningpoolhub.com"))
            {
                btcAddress = "cryptominer.Devfee";
                worker = "x";
            }

            // demo for benchmark
            var username = Globals.DemoUser;

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += "." + ConfigManager.GeneralConfig.WorkerName.Trim();

            string ret;

            ret = " " + GetDevicesCommandString() + " -mport -" + ApiPort + " -pool " + url + " -wal " +
                         btcAddress + " -psw " + worker;

            return ret;
        }
    }
}