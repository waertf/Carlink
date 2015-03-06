using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gurock.SmartInspect;

namespace Carlink
{
    class Program
    {
        static readonly Random random = new Random();
        static readonly string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        static readonly string distance = "0123456789";
        private static readonly byte[] status = new byte[]{10,11,12,13,20,30,41,42,51,52,53};

        private static void Main(string[] args)
        {
            /*
            BitArray yearBitArray = new BitArray(6,false);
            BitArray monthBitArray = new BitArray(4, false);
            BitArray dayBitArray = new BitArray(5, false);
            BitArray hourBitArray = new BitArray(5, false);
            BitArray minBitArray = new BitArray(6, false);
            BitArray secBitArray = new BitArray(6, false);
            bool[] timeBitArray= new bool[32];
            yearBitArray[0] = true;
            yearBitArray[1] = true;
            yearBitArray[2] = true;
            yearBitArray[3] = true;
            yearBitArray[4] = true;
            yearBitArray[5] = true;
            monthBitArray[1] = true;
            yearBitArray.CopyTo(timeBitArray,0);
            monthBitArray.CopyTo(timeBitArray,yearBitArray.Length);
            //PrintValues(timeBitArray);
            //BitArray resultBitArray= new BitArray(timeBitArray);
            //byte[] bytes= new byte[4];
            //resultBitArray.CopyTo(bytes,0);
            //foreach (var b in bytes)
            //{
            //    Console.WriteLine(b);
            //}
            //BitArray testBitArray= new BitArray(bytes);
            //PrintValues(testBitArray);
            Console.WriteLine(getIntFromBitArray(yearBitArray));
            string a = "+-+";
            byte[] bbBytes = Encoding.ASCII.GetBytes(a);
            foreach (var b in bbBytes)
            {
                Console.WriteLine(b);
            }
            */
            SiAuto.Si.Connections = @"file(filename=""" +
                                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                    "\\log.sil\",rotate=weekly,append=true,maxparts=5,maxsize=500MB)";
            SiAuto.Si.Enabled = true;

            Thread workerThread = new Thread(send);
            workerThread.Start();
            /*
            byte[] aBytes = new byte[]{1,2,3};
            byte[] bBytes = new byte[]{ 4,5,6 };
            byte[] cBytes = new byte[] { 7,8,9 };
            byte[] dBytes = new byte[] { 10,11,12 };
            byte[] combinBytes = Combine(aBytes, bBytes, cBytes, dBytes);
            foreach (var combinByte in combinBytes)
            {
                Console.WriteLine(combinByte);
            }
            Console.WriteLine(combinBytes.Length);
            */
        }

        static void send()
        {
            while (true)
            {
                #region

                bool[] timeBitArray = new bool[32];
                byte[] uid = Encoding.ASCII.GetBytes(new string(
                    Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray()));
                byte[] totalGoDistance = Encoding.ASCII.GetBytes(new string(
                    Enumerable.Repeat(distance, 6)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray()));
                byte mystatus = status[random.Next(status.Length)];

                string now = DateTime.Now.ToString("yyMMddHHmmss");
                byte year = Convert.ToByte(now.Substring(0, 2));
                byte month = Convert.ToByte(now.Substring(2, 2));
                byte day = Convert.ToByte(now.Substring(4, 2));
                byte hr = Convert.ToByte(now.Substring(6, 2));
                byte min = Convert.ToByte(now.Substring(8, 2));
                byte sec = Convert.ToByte(now.Substring(10, 2));
                BitArray yearArray = new BitArray(BitConverter.GetBytes(year).ToArray());
                BitArray monArrayy = new BitArray(BitConverter.GetBytes(month).ToArray());
                BitArray dayArray = new BitArray(BitConverter.GetBytes(day).ToArray());
                BitArray hrArray = new BitArray(BitConverter.GetBytes(hr).ToArray());
                BitArray minArray = new BitArray(BitConverter.GetBytes(min).ToArray());
                BitArray secArray = new BitArray(BitConverter.GetBytes(sec).ToArray());

                for (int i = 0; i < 6; i++)
                {
                    timeBitArray[i] = yearArray[i];
                }
                for (int i = 6; i < 10; i++)
                {
                    timeBitArray[i] = monArrayy[i - 6];
                }
                for (int i = 10; i < 15; i++)
                {
                    timeBitArray[i] = dayArray[i - 10];
                }
                for (int i = 15; i < 20; i++)
                {
                    timeBitArray[i] = hrArray[i - 15];
                }
                for (int i = 20; i < 26; i++)
                {
                    timeBitArray[i] = minArray[i - 20];
                }
                for (int i = 26; i < 32; i++)
                {
                    timeBitArray[i] = secArray[i - 26];
                }

                //Console.WriteLine(now);
                //PrintValues(timeBitArray);
                BitArray resultBitArray = new BitArray(timeBitArray);
                byte[] time = new byte[4];
                resultBitArray.CopyTo(time, 0);
                //foreach (var b in time)
                //{
                //Console.WriteLine(b);
                //}

                #endregion

                byte longDeg, longMin, longSec, latDeg, latMin, latSec;
                longDeg = (byte)(random.Next(180));
                longMin = (byte)(random.Next(60));
                longSec = (byte)(random.Next(60));
                latDeg = (byte)(random.Next(90));
                latMin = (byte)(random.Next(60));
                latSec = (byte)(random.Next(60));
                Console.WriteLine(mystatus);
                switch (mystatus)
                {
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        {
                            byte engineCoolantTemperature = (byte)(random.Next(byte.MaxValue));
                            byte fuelPressure = (byte)(random.Next(byte.MaxValue));
                            byte intakeManifoldPressure = (byte)(random.Next(byte.MaxValue));
                            byte RPMHigh = (byte)(random.Next(byte.MaxValue));
                            byte RPMLow = (byte)(random.Next(byte.MaxValue));
                            byte vehicleSpeed = (byte)(random.Next(byte.MaxValue));
                            byte intakeAirTemperature = (byte)(random.Next(byte.MaxValue));
                            byte airFlowRate = (byte)(random.Next(byte.MaxValue));
                            byte throttlePosition = (byte)(random.Next(byte.MaxValue));
                            byte batteryVoltage = (byte)(random.Next(byte.MaxValue));
                            byte[] dataBytes = new byte[20]
                    {
                        time[0],
                        time[1],
                        time[2],
                        time[3],
                        longDeg,
                        longMin,
                        longSec,
                        latDeg,
                        latMin,
                        latSec,
                        engineCoolantTemperature,
                        fuelPressure,
                        intakeManifoldPressure,
                        RPMHigh,
                        RPMLow,
                        vehicleSpeed,
                        intakeAirTemperature,
                        airFlowRate,
                        throttlePosition,
                        batteryVoltage
                    };
                            byte[] sendBytes = new byte[35];
                            Buffer.BlockCopy(uid, 0, sendBytes, 0, uid.Length);
                            sendBytes.SetValue(mystatus, uid.Length);
                            Buffer.BlockCopy(totalGoDistance, 0, sendBytes, uid.Length + 1, totalGoDistance.Length);
                            Buffer.BlockCopy(dataBytes, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length, dataBytes.Length);
                            SiAuto.Main.LogArray("send status:"+mystatus.ToString(),sendBytes);
                        }
                        break;
                    case 20:
                        {
                            byte[] dataBytes = new byte[20]
                    {
                        time[0],
                        time[1],
                        time[2],
                        time[3],
                        longDeg,
                        longMin,
                        longSec,
                        latDeg,
                        latMin,
                        latSec,
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff))
                    };
                            byte[] sendBytes = new byte[35];
                            Buffer.BlockCopy(uid, 0, sendBytes, 0, uid.Length);
                            sendBytes.SetValue(mystatus, uid.Length);
                            Buffer.BlockCopy(totalGoDistance, 0, sendBytes, uid.Length + 1, totalGoDistance.Length);
                            Buffer.BlockCopy(dataBytes, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length, dataBytes.Length);
                            SiAuto.Main.LogArray("send status:" + mystatus.ToString(), sendBytes);
                        }
                        break;
                    case 30:
                        {
                            byte[] dataBytes = new byte[18]
                    {
                        time[0],
                        time[1],
                        time[2],
                        time[3],
                        longDeg,
                        longMin,
                        longSec,
                        latDeg,
                        latMin,
                        latSec,
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff)),
                        (byte) (random.Next(0xff))
                    };
                            byte[] sendBytes = new byte[33];
                            Buffer.BlockCopy(uid, 0, sendBytes, 0, uid.Length);
                            sendBytes.SetValue(mystatus, uid.Length);
                            Buffer.BlockCopy(totalGoDistance, 0, sendBytes, uid.Length + 1, totalGoDistance.Length);
                            Buffer.BlockCopy(dataBytes, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length, dataBytes.Length);
                            SiAuto.Main.LogArray("send status:" + mystatus.ToString(), sendBytes);
                        }
                        break;
                    case 41:
                    case 42:
                        {
                            byte[] dataBytes = new byte[10]
                    {
                        time[0],
                        time[1],
                        time[2],
                        time[3],
                        longDeg,
                        longMin,
                        longSec,
                        latDeg,
                        latMin,
                        latSec
                    };
                            now = DateTime.Now.ToString("yyyyMMddHHmm");
                            byte[] nowBytes = Encoding.ASCII.GetBytes(now);
                            byte[] sendBytes = new byte[45];
                            Buffer.BlockCopy(uid, 0, sendBytes, 0, uid.Length);
                            sendBytes.SetValue(mystatus, uid.Length);
                            Buffer.BlockCopy(totalGoDistance, 0, sendBytes, uid.Length + 1, totalGoDistance.Length);
                            Buffer.BlockCopy(dataBytes, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length, dataBytes.Length);
                            Buffer.BlockCopy(uid, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length + dataBytes.Length,
                                uid.Length);
                            Buffer.BlockCopy(nowBytes, 0, sendBytes,
                                uid.Length + 1 + totalGoDistance.Length + dataBytes.Length + uid.Length, nowBytes.Length);
                            SiAuto.Main.LogArray("send status:" + mystatus.ToString(), sendBytes);
                        }
                        break;
                    case 51:
                    case 52:
                    case 53:
                        {
                            byte[] dataBytes = new byte[10]
                    {
                        time[0],
                        time[1],
                        time[2],
                        time[3],
                        longDeg,
                        longMin,
                        longSec,
                        latDeg,
                        latMin,
                        latSec
                    };
                            byte[] sendBytes = new byte[25];
                            Buffer.BlockCopy(uid, 0, sendBytes, 0, uid.Length);
                            sendBytes.SetValue(mystatus, uid.Length);
                            Buffer.BlockCopy(totalGoDistance, 0, sendBytes, uid.Length + 1, totalGoDistance.Length);
                            Buffer.BlockCopy(dataBytes, 0, sendBytes, uid.Length + 1 + totalGoDistance.Length, dataBytes.Length);
                            SiAuto.Main.LogArray("send status:" + mystatus.ToString(), sendBytes);
                        }
                        break;
                }
                Thread.Sleep(1000);
            }
        }
        static int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
        static void PrintValues(IEnumerable myArr)
        {
            foreach (Object obj in myArr)
            {
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }
        static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
