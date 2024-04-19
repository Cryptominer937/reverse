using HashKingsMiner.Enums;
using HashKingsMiner.Miners.Parsing;

namespace HashKingsMiner.Miners
{
    public class Eqm : NheqBase
    {
        public Eqm()
            : base("eqm")
        {
            ConectionType = NHMConectionType.LOCKED;
            IsNeverHideMiningWindow = true;
        }

        public override void Start(string url, string btcAddress, string worker)
        {
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
                    worker = HashKingsMiner.Globals.GetzergWorker() + "";
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

            LastCommandLine = GetDevicesCommandString() + " -a " + ApiPort + " -l " + url + " -u " + btcAddress + " -w " + worker + "";
            ProcessHandle = _Start();
        }

        protected override string GetDevicesCommandString()
        {
            var deviceStringCommand = " ";

            if (CPU_Setup.IsInit)
            {
                deviceStringCommand += "-p " + CPU_Setup.MiningPairs.Count;
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(CPU_Setup, DeviceType.CPU);
            }
            else
            {
                // disable CPU
                deviceStringCommand += " -t 0 ";
            }

            if (NVIDIA_Setup.IsInit)
            {
                deviceStringCommand += " -cd ";

                foreach (var nvidia_pair in NVIDIA_Setup.MiningPairs)
                {
                    if (nvidia_pair.CurrentExtraLaunchParameters.Contains("-ct"))
                    {
                        for (int i = 0; i < ExtraLaunchParametersParser.GetEqmCudaThreadCount(nvidia_pair); ++i)
                            deviceStringCommand += nvidia_pair.Device.ID + " ";
                    }
                    else
                    { // use default 2 best performance
                        for (int i = 0; i < 2; ++i)
                            deviceStringCommand += nvidia_pair.Device.ID + " ";
                    }
                }

                // no extra launch params
                deviceStringCommand += " " + ExtraLaunchParametersParser.ParseForMiningSetup(NVIDIA_Setup, DeviceType.NVIDIA);
            }

            return deviceStringCommand;
        }

        // benchmark stuff
        private const string TOTAL_MES = "Total measured:";

        protected override bool BenchmarkParseLine(string outdata)
        {
            if (outdata.Contains(TOTAL_MES) && outdata.Contains(Iter_PER_SEC))
            {
                curSpeed = GetNumber(outdata, TOTAL_MES, Iter_PER_SEC) * SolMultFactor;
            }

            if (outdata.Contains(TOTAL_MES) && outdata.Contains(Sols_PER_SEC))
            {
                var sols = GetNumber(outdata, TOTAL_MES, Sols_PER_SEC);

                if (sols > 0)
                {
                    BenchmarkAlgorithm.BenchmarkSpeed = curSpeed;
                    return true;
                }
            }

            return false;
        }
    }
}