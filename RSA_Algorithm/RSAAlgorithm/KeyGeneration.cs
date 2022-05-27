using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
namespace RSAAlgorithm;

public class KeyGeneration
{
    public BigInteger p, q,n,f,e,d;


    public KeyGeneration()
    {
        PrimeRandNume primeRandNume = new PrimeRandNume();
        p = primeRandNume.generate(1024);
        q = primeRandNume.generate(1024);
        n = p * q;
        f=(p-1)*(q-1);
        e = generatePublicKey();
        d= generatePrivateKey();

    }

    public BigInteger generatePublicKey()
    {
        BigInteger result=0;
        BigIntegerPrimeTest BIPT = new BigIntegerPrimeTest();
  

        for (BigInteger i = f / 2; i < f; i++)
        {
            if (BIPT.IsProbablePrime(i, 10) == true)
            {
                if (BigInteger.GreatestCommonDivisor(i, f) == 1)
                {
                    result = i;
                    break;
                }
            }
         }

        return result;
    }
    public BigInteger generatePrivateKey()
    {
        BigInteger result = ModInverse(e, f);
        return result;
    }
    public static BigInteger ModInverse(BigInteger a, BigInteger m) 
    {
        if (m == 1) return 0;
        BigInteger m0 = m;
        (BigInteger x, BigInteger y) = (1, 0);

        while (a > 1)
        {
            BigInteger q = a / m;
            (a, m) = (m, a % m);
            (x, y) = (y, x - q * y);
        }
        return x < 0 ? x + m0 : x;
    }

}


