using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace RSAAlgorithm;

class PrimeRandNume {
    public BigInteger generate(int size)
    {
        while (true)
        {
            int bitlen = size;
            RandomBigIntegerGenerator RBI = new RandomBigIntegerGenerator();
            BigInteger RandomNumber = RBI.NextBigInteger(bitlen);


            BigIntegerPrimeTest BIPT = new BigIntegerPrimeTest();
            if (BIPT.IsProbablePrime(RandomNumber, 10) == true)
            {
                return RandomNumber;

            }
        }
    }
}
    


class RandomBigIntegerGenerator
{
    public BigInteger NextBigInteger(int bitLength)
    {
        if (bitLength < 1) return BigInteger.Zero;

        int bytes = bitLength / 8;
        int bits = bitLength % 8;

        // Generates enough random bytes to cover our bits.
        Random rnd = new Random();
        byte[] bs = new byte[bytes + 1];
        rnd.NextBytes(bs);

        // Mask out the unnecessary bits.
        byte mask = (byte)(0xFF >> (8 - bits));
        bs[bs.Length - 1] &= mask;

        return new BigInteger(bs);
    }


}


// Miller-Rabin primality test as an extension method on the BigInteger type.
// Based on the Ruby implementation on this page.
class BigIntegerPrimeTest
{
    public bool IsProbablePrime(BigInteger source, int certainty)
    {
        if (source == 2 || source == 3)
            return true;
        if (source < 2 || source % 2 == 0)
            return false;

        BigInteger d = source - 1;
        int s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            s += 1;
        }

        // There is no built-in method for generating random BigInteger values.
        // Instead, random BigIntegers are constructed from randomly generated
        // byte arrays of the same length as the source.
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[source.ToByteArray().LongLength];
        BigInteger a;

        for (int i = 0; i < certainty; i++)
        {
            do
            {
                // This may raise an exception in Mono 2.10.8 and earlier.
                // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                rng.GetBytes(bytes);
                a = new BigInteger(bytes);
            }
            while (a < 2 || a >= source - 2);

            BigInteger x = BigInteger.ModPow(a, d, source);
            if (x == 1 || x == source - 1)
                continue;

            for (int r = 1; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, source);
                if (x == 1)
                    return false;
                if (x == source - 1)
                    break;
            }

            if (x != source - 1)
                return false;
        }

        return true;
    }
}


