﻿using System.Collections.Generic;
using HashKingsMiner.Enums;
using HashKingsMiner.Miners.Parsing;

namespace HashKingsMiner.Miners
{
    public class MinerEtherumCUDA : MinerEtherum
    {
        // reference to all MinerEtherumCUDA make sure to clear this after miner Stop
        // we make sure only ONE instance of MinerEtherumCUDA is running
        private static List<MinerEtherum> MinerEtherumCUDAList = new List<MinerEtherum>();

        public MinerEtherumCUDA()
            : base("MinerEtherumCUDA", "NVIDIA")
        {
            MinerEtherumCUDAList.Add(this);
        }

        ~MinerEtherumCUDA()
        {
            // remove from list
            MinerEtherumCUDAList.Remove(this);
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

            Helpers.ConsolePrint(MinerTag(), "Starting MinerEtherumCUDA, checking existing MinerEtherumCUDA to stop");
            base.Start(url, btcAddress, worker, MinerEtherumCUDAList);
        }

        protected override string GetStartCommandStringPart(string url, string username)
        {
            return " --cuda"
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.NVIDIA)
                + " -S " + url.Substring(14)
                + " -O " + username + ""
                + " --api-port " + ApiPort.ToString()
                + " --cuda-devices ";
        }

        protected override string GetBenchmarkCommandStringPart(Algorithm algorithm)
        {
            return " --benchmark-warmup 40 --benchmark-trial 20"
                + " "
                + ExtraLaunchParametersParser.ParseForMiningSetup(
                                                    MiningSetup,
                                                    DeviceType.NVIDIA)
                + " --cuda --cuda-devices ";
        }
    }
}