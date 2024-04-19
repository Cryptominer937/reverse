using System.Collections.Generic;
using HashKingsMiner.Devices;
using HashKingsMiner.Enums;
using HashKingsMiner.Miners.Parsing;

namespace HashKingsMiner.Miners
{
    // TODO for NOW ONLY AMD
    // AMD or TODO it could be something else
    public class MinerEtherumOCL : MinerEtherum
    {
        // reference to all MinerEtherumOCL make sure to clear this after miner Stop
        // we make sure only ONE instance of MinerEtherumOCL is running
        private static List<MinerEtherum> MinerEtherumOCLList = new List<MinerEtherum>();

        private readonly int GPUPlatformNumber;

        public MinerEtherumOCL()
            : base("MinerEtherumOCL", "AMD OpenCL")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
            MinerEtherumOCLList.Add(this);
        }

        ~MinerEtherumOCL()
        {
            // remove from list
            MinerEtherumOCLList.Remove(this);
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

            Helpers.ConsolePrint(MinerTag(), "Starting MinerEtherumOCL, checking existing MinerEtherumOCL to stop");
            base.Start(url, btcAddress, worker, MinerEtherumOCLList);
        }

        protected override string GetStartCommandStringPart(string url, string username)
        {
            return " --opencl --opencl-platform " + GPUPlatformNumber
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.AMD)
                + " -S " + url.Substring(14)
                + " -O " + username + ""
                + " --api-port " + ApiPort.ToString()
                + " --opencl-devices ";
        }

        protected override string GetBenchmarkCommandStringPart(Algorithm algorithm)
        {
            return " --opencl --opencl-platform " + GPUPlatformNumber
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.AMD)
                + " --benchmark-warmup 40 --benchmark-trial 20"
                + " --opencl-devices ";
        }
    }
}