using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using HashKingsMiner.Devices;
using HashKingsMiner.Enums;
using HashKingsMiner.Miners.Grouping;
using HashKingsMiner.Miners.Parsing;

namespace HashKingsMiner.Miners.equihash
{
    public class OptiminerZcashMiner : Miner
    {
        public OptiminerZcashMiner()
            : base("OptiminerZcashMiner")
        {
            ConectionType = NHMConectionType.NONE;
        }

        private class Stratum
        {
            public string Target { get; set; }
            public bool Connected { get; set; }
            public int Connection_failures { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        }

        private class JsonApiResponse
        {
            public double uptime;
            public Dictionary<string, Dictionary<string, double>> solution_rate;
            public Dictionary<string, double> share;
            public Dictionary<string, Dictionary<string, double>> iteration_rate;
            public Stratum stratum;
        }

        // give some time or else it will crash
        private Stopwatch _startAPI;

        private bool _skipAPICheck = true;
        private int waitSeconds = 30;

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

            var username = GetUsername(btcAddress, worker);
            LastCommandLine = " " + GetDevicesCommandString() + " -m " + ApiPort + " -s " + url + " -u " + username + " -p " + worker;
            ProcessHandle = _Start();

            //
            _startAPI = new Stopwatch();
            _startAPI.Start();
        }

        protected override void _Stop(MinerStopType willswitch)
        {
            Stop_cpu_ccminer_sgminer_nheqminer(willswitch);
        }

        protected override int GetMaxCooldownTimeInMilliseconds()
        {
            return 60 * 1000 * 5; // 5 minute max, whole waiting time 75seconds
        }

        protected override string GetDevicesCommandString()
        {
            var extraParams = ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.AMD);
            var deviceStringCommand = " -c " + ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
            deviceStringCommand += " ";
            var ids = new List<string>();

            foreach (var mPair in MiningSetup.MiningPairs)
                ids.Add("-d " + mPair.Device.ID.ToString());

            deviceStringCommand += string.Join(" ", ids);

            return deviceStringCommand + extraParams;
        }

        public override async Task<ApiData> GetSummaryAsync()
        {
            CurrentMinerReadStatus = MinerApiReadStatus.NONE;
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);

            if (_skipAPICheck == false)
            {
                JsonApiResponse resp = null;

                try
                {
                    var DataToSend = GetHttpRequestNHMAgentStrin("");
                    var respStr = await GetAPIDataAsync(ApiPort, DataToSend, true);

                    if (respStr != null && respStr.Contains("{"))
                    {
                        var start = respStr.IndexOf("{");

                        if (start > -1)
                        {
                            var respStrJSON = respStr.Substring(start);
                            resp = JsonConvert.DeserializeObject<JsonApiResponse>(respStrJSON.Trim(), Globals.JsonSettings);
                        }
                    }
                    // Helpers.ConsolePrint("OptiminerZcashMiner API back:", respStr);
                }
                catch (Exception ex)
                {
                    Helpers.ConsolePrint("OptiminerZcashMiner", "GetSummary exception: " + ex.Message);
                }

                if (resp != null && resp.solution_rate != null)
                {
                    // Helpers.ConsolePrint("OptiminerZcashMiner API back:", "resp != null && resp.error == null");
                    const string total_key = "Total";
                    const string _5s_key = "5s";

                    if (resp.solution_rate.ContainsKey(total_key))
                    {
                        var total_solution_rate_dict = resp.solution_rate[total_key];

                        if (total_solution_rate_dict != null && total_solution_rate_dict.ContainsKey(_5s_key))
                        {
                            ad.Speed = total_solution_rate_dict[_5s_key];
                            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                        }
                    }

                    if (ad.Speed == 0)
                    {
                        CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                    }
                }
            }
            else if (_skipAPICheck && _startAPI.Elapsed.TotalSeconds > waitSeconds)
            {
                _startAPI.Stop();
                _skipAPICheck = false;
            }

            return ad;
        }

        // benchmark stuff

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            var t = time / 9; // sgminer needs 9 times more than this miner so reduce benchmark speed
            var ret = " " + GetDevicesCommandString() + " --benchmark " + t;
            return ret;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            const string FIND = "Benchmark:";

            if (outdata.Contains(FIND))
            {
                var start = outdata.IndexOf("Benchmark:") + FIND.Length;
                var itersAndVars = outdata.Substring(start).Trim();
                var ar = itersAndVars.Split(new char[] { ' ' });

                if (ar.Length >= 4)
                {
                    // gets sols/s
                    BenchmarkAlgorithm.BenchmarkSpeed = Helpers.ParseDouble(ar[2]);
                    return true;
                }
            }

            return false;
        }
    }
}