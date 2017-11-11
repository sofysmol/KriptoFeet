using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Utils
{
    public class LongRandomGenerator : ILongRandomGenerator
    {
        private Random rand = new Random();
        public long Next()
        {
            long min = 0;
            long max = 100000000000000000;
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}