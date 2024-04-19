﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HashKingsMiner;
using HashKingsMiner.Configs;
using HashKingsMiner.Devices;
using HashKingsMiner.Enums;
using HashKingsMiner.Miners;
using HashKingsMiner.Miners.Grouping;
using HashKingsMiner.Miners.Parsing;

namespace NiceHashMiner.Miners
{
    internal class lolMinerNvidia : Miner
    {
        private readonly int GPUPlatformNumber;
        private Stopwatch _benchmarkTimer = new Stopwatch();
        private int count;
        private double speed = 0;

        public lolMinerNvidia()
            : base("lolMiner_Nvidia")
        {
            GPUPlatformNumber = ComputeDeviceManager.Available.AmdOpenCLPlatformNum;
            IsKillAllUsedMinerProcs = true;
            IsNeverHideMiningWindow = true;
        }

        protected override int GetMaxCooldownTimeInMilliseconds() => 60 * 1000;

        protected override void _Stop(MinerStopType willswitch)
        {
            ProcessHandle.SendCtrlC((uint)Process.GetCurrentProcess().Id);
        }

        public override void Start(string url, string btcAddress, string worker)
        {
            var apiBind = " --apiport " + ApiPort;

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

            if (!IsInit)
            {
                Helpers.ConsolePrint(MinerTag(), "MiningSetup is not initialized exiting Start()");
                return;
            }

            var username = GetUsername(btcAddress, worker);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.karlsenhash)
                LastCommandLine = " --algo KARLSEN" + " --pool=" + url + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.pyrinhash)
                LastCommandLine = " --algo PYRIN" + " --pool=" + url + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.ethash)
                LastCommandLine = " --algo ETHASH" + " --pool=" + "stratum+tcp://ethash.mine.zergpool.com:9999" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.ethashb3)
                LastCommandLine = " --algo ETHASHB3" + " --pool=" + "stratum+tcp://ethashb3.mine.zergpool.com:9996" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.nexapow)
                LastCommandLine = " --algo NEXA" + " --pool=" + "stratum+tcp://nexapow.mine.zergpool.com:3004" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.sha512256d)
                LastCommandLine = " --algo RADIANT" + " --pool=" + "stratum+tcp://sha512256d.mine.zergpool.com:7086" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash144)
                LastCommandLine = " --algo EQUI144_5 --pers AUTO" + " --pool=" + "stratum+tcp://equihash144.mine.zergpool.com:2146" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash192)
                LastCommandLine = " --algo EQUI192_7 --pers AUTO" + " --pool=" + "stratum+tcp://equihash192.mine.zergpool.com:2144" + " --user=" + username + " --pass " + worker + " --devices NVIDIA --watchdog exit" + apiBind + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            LastCommandLine += GetDevicesCommandString();

            ProcessHandle = _Start();
        }

        // new decoupled benchmarking routines

        #region Decoupled benchmarking routines

        protected override string BenchmarkCreateCommandLine(Algorithm algorithm, int time)
        {
            var CommandLine = "";

            var url = Globals.GetLocationURL(algorithm.CryptoMiner937ID, Globals.MiningLocation[ConfigManager.GeneralConfig.ServiceLocation], ConectionType);
            var port = url.Substring(url.IndexOf(".com:") + 5, url.Length - url.IndexOf(".com:") - 5);
            // demo for benchmark
            var username = Globals.GetBitcoinUser();

            if (ConfigManager.GeneralConfig.WorkerName.Length > 0)
                username += ":" + ConfigManager.GeneralConfig.WorkerName.Trim();

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.karlsenhash)
                CommandLine = "--algo KARLSEN --pool " + url + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 120" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.pyrinhash)
                CommandLine = "--algo PYRIN --pool " + url + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 120" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.ethash)
                CommandLine = "--algo ETHASH --pool " + "stratum+tcp://ethash.mine.zergpool.com:9999" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 120" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.ethashb3)
                CommandLine = "--algo ETHASHB3 --pool " + "stratum+tcp://ethashb3.mine.zergpool.com:9996" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 120" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.nexapow)
                CommandLine = "--algo NEXA --pool " + "stratum+tcp://nexapow.mine.zergpool.com:3004" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 120" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.sha512256d)
                CommandLine = "--algo sha512256d --pool " + "stratum+tcp://sha512256d.mine.zergpool.com:7086" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --shortstats 120 --apihost 127.0.0.1 --apiport " + ApiPort + "" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash144)
                CommandLine = "--algo EQUI144_5 --pers AUTO --pool " + "stratum+tcp://equihash144.mine.zergpool.com:2146" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 110" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            if (MiningSetup.CurrentAlgorithmType == AlgorithmType.equihash192)
                CommandLine = "--algo EQUI192_7 --pers AUTO --pool " + "stratum+tcp://equihash192.mine.zergpool.com:2144" + " --user " + "DE8BDPdYu9LadwV4z4KamDqni43BUhGb66 --pass Benchmark " + "--devices NVIDIA --watchdog exit --apihost 127.0.0.1 --apiport " + ApiPort + " --shortstats 110" + " " + ExtraLaunchParametersParser.ParseForMiningSetup(MiningSetup, DeviceType.NVIDIA);

            CommandLine += GetDevicesCommandString(); //amd карты перечисляются первыми

            return CommandLine;
        }

        protected override bool BenchmarkParseLine(string outdata)
        {
            var hashSpeed = "";
            // Average speed (30s): 25.5 sol/s
            // GPU 3: Share accepted (45 ms)
            if (outdata.Contains("Average speed (120s):"))
            {
                var i = outdata.IndexOf("Average speed (120s):");
                var k = outdata.IndexOf("Mh/s");
                hashSpeed = outdata.Substring(i + 21, k - i - 22).Trim();

                try
                {
                    speed = speed * 1000000 * 1.2 + double.Parse(hashSpeed, CultureInfo.InvariantCulture);
                }
                catch
                {
                    MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    BenchmarkSignalFinnished = true;
                    return false;
                }

                count++;
            }

            if (outdata.Contains("Average speed (110s):"))
            {
                var i = outdata.IndexOf("Average speed (110s):");
                var k = outdata.IndexOf("sol/s");
                hashSpeed = outdata.Substring(i + 21, k - i - 22).Trim();

                try
                {
                    speed = speed + double.Parse(hashSpeed, CultureInfo.InvariantCulture);
                    BenchmarkAlgorithm.BenchmarkSpeed = speed / count;
                    BenchmarkSignalFinnished = true;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Unsupported miner version - " + MiningSetup.MinerPath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    BenchmarkSignalFinnished = true;
                    return false;
                }

                count++;
            }

            if (outdata.Contains("Share accepted") && speed != 0)
            {
                BenchmarkAlgorithm.BenchmarkSpeed = speed / count * 1000000;
                BenchmarkSignalFinnished = true;
                return true;
            }

            return false;
        }

        protected override void BenchmarkOutputErrorDataReceivedImpl(string outdata)
        {
            CheckOutdata(outdata);
        }

        #endregion Decoupled benchmarking routines

        public class lolResponse
        {
            public List<lolGpuResult> result { get; set; }
        }

        public class lolGpuResult
        {
            public double sol_ps { get; set; } = 0;
        }

        // TODO _currentMinerReadStatus
        public override async Task<ApiData> GetSummaryAsync()
        {
            var ad = new ApiData(MiningSetup.CurrentAlgorithmType);
            string ResponseFromlolMiner;

            try
            {
                var WR = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:" + ApiPort.ToString() + "/summary");
                WR.UserAgent = "GET / HTTP/1.1\r\n\r\n";
                WR.Timeout = 3 * 1000;
                WR.Credentials = CredentialCache.DefaultCredentials;
                var Response = WR.GetResponse();
                var SS = Response.GetResponseStream();
                SS.ReadTimeout = 2 * 1000;
                var Reader = new StreamReader(SS);
                ResponseFromlolMiner = await Reader.ReadToEndAsync();
                // Helpers.ConsolePrint("API: ", ResponseFromlolMiner);
                // if (ResponseFromlolMiner.Length == 0 || (ResponseFromlolMiner[0] != '{' && ResponseFromlolMiner[0] != '['))
                //    throw new Exception("Not JSON!");
                Reader.Close();
                Response.Close();
            }
            catch (Exception)
            {
                return null;
            }

            if (ResponseFromlolMiner == null)
            {
                CurrentMinerReadStatus = MinerApiReadStatus.NONE;
                return null;
            }

            try
            {
                dynamic resp = JsonConvert.DeserializeObject(ResponseFromlolMiner);
                var mult = 1;

                if (resp != null)
                {
                    int Num_Workers = resp.Num_Workers;
                    if (Num_Workers == 0) return null;
                    int Num_Algorithms = resp.Num_Algorithms;
                    // Helpers.ConsolePrint("API: ", "Num_Workers: " + Num_Workers.ToString());
                    // Helpers.ConsolePrint("API: ", "Num_Algorithms: " + Num_Algorithms.ToString());
                    var Total_Performance = new double[Num_Algorithms];
                    var hashrates = new double[Num_Workers];
                    var totals = 0.0d;

                    for (int alg = 0; alg < Num_Algorithms; alg++)
                    {
                        Total_Performance[alg] = resp.Algorithms[alg].Total_Performance * resp.Algorithms[alg].Performance_Factor;
                        string Algorithm = resp.Algorithms[alg].Algorithm;
                        totals = Total_Performance[alg];

                        for (int w = 0; w < Num_Workers; w++)
                        {
                            hashrates[w] = resp.Algorithms[alg].Worker_Performance[w] * resp.Algorithms[alg].Performance_Factor;
                            // Helpers.ConsolePrint("API: ", "hashrates: " + hashrates[w].ToString());
                        }
                    }

                    /*if (MiningSetup.CurrentAlgorithmType == AlgorithmType.ethash)
                    {
                        mult = 1000000;
                    }
                    else
                    {
                        mult = 1;
                    }*/

                    ad.Speed = totals;

                    if (Num_Workers > 0)
                    {
                        var dev = 0;
                        var sortedMinerPairs = MiningSetup.MiningPairs.OrderBy(pair => pair.Device.BusID).ToList();

                        if (ad.Speed == 0)
                        {
                            CurrentMinerReadStatus = MinerApiReadStatus.READ_SPEED_ZERO;
                        }
                        else
                        {
                            CurrentMinerReadStatus = MinerApiReadStatus.GOT_READ;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helpers.ConsolePrint(MinerTag(), e.ToString());
            }

            Thread.Sleep(100);
            return ad;
        }
    }
}