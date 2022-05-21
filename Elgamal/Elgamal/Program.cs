using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;


namespace ElGamalAlgorithm

{



    class GFG
    {
        // generates sqrt
        public static BigInteger Sqrt(BigInteger number)
        {
            if (number < 9)
            {
                if (number == 0)
                    return 0;
                if (number < 4)
                    return 1;
                else
                    return 2;
            }

            BigInteger n = 0, p = 0;
            var high = number >> 1;
            var low = BigInteger.Zero;

            while (high > low + 1)
            {
                n = (high + low) >> 1;
                p = n * n;
                if (number < p)
                {
                    high = n;
                }
                else if (number > p)
                {
                    low = n;
                }
                else
                {
                    break;
                }
            }
            return number == p ? n : low;
        }

        // Utility function to store prime factors of a number
        static void findPrimefactors(HashSet<BigInteger> s, BigInteger n)
        {
            // Print the number of 2s that divide n
            while (n % 2 == 0)
            {
                s.Add(2);
                n = n / 2;
            }

            // n must be odd at this point. So we can skip
            // one element (Note i = i +2)
            BigInteger sq = Sqrt(n);
            for (int i = 3; i <= sq; i = i + 2)
            {
                // While i divides n, print i and divide n
                while (n % i == 0)
                {
                    s.Add(i);
                    n = n / i;
                }
            }

            // This condition is to handle the case when
            // n is a prime number greater than 2
            if (n > 2)
            {
                s.Add(n);
            }
        }

        // Function to find smallest primitive root of n
        public static int findPrimitive(BigInteger n)
        {
            HashSet<BigInteger> s = new HashSet<BigInteger>();

            // Check if n is prime or not
            //if (isPrime(n) == false)
            //{
            //    return -1;
            //}

            // Find value of Euler Totient function of n
            // Since n is a prime number, the value of Euler
            // Totient function is n-1 as there are n-1
            // relatively prime numbers.
            BigInteger phi = n - 1;

            // Find prime factors of phi and store in a set
            findPrimefactors(s, phi);

            // Check for every number from 2 to phi
            for (int r = 2; r <= phi; r++)
            {
                // Iterate through all prime factors of phi.
                // and check if we found a power with value 1
                bool flag = false;
                foreach (BigInteger a in s)
                {

                    // Check if r^((phi)/primefactors) mod n
                    // is 1 or not
                    if (BigInteger.ModPow(r, phi / (a), n) == 1)
                    {
                        flag = true;
                        break;
                    }
                }

                // If there was no power with value 1.
                if (flag == false)
                {
                    return r;
                }
            }

            // If no primitive root found
            return -1;
        }


    }

    class ElGamal
    {
        public static BigInteger q = BigInteger.One;
        public static BigInteger a = BigInteger.One;
        public static BigInteger Private_Key = BigInteger.One;
        public static BigInteger Public_key = BigInteger.One;

        public static int prime_length = 100;

        class PrimeRandNume
        {
            
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

        public static BigInteger ModInverse(BigInteger a, BigInteger m) // a hya alinverse al m hy al mod
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
        





        // generates q and a
        public static void genearte_global_public_elements()
        {
            PrimeRandNume primeRandNume = new PrimeRandNume();
            // size is 10 should be 300 bas ana 3andy marara wa7da (╯°□°）╯︵ ┻━┻
            q = primeRandNume.generate(prime_length);
            a = GFG.findPrimitive(q);

           
        }

        public static void generate_key_pair()
        {
            RandomBigIntegerGenerator RBI = new RandomBigIntegerGenerator();
            BigInteger RandomNumber = RBI.NextBigInteger(prime_length -1);

            Private_Key = RandomNumber;
            Public_key = BigInteger.ModPow(a, Private_Key, q);

        }



        public static int Main(string[] args)
        {
            // step 1 generate global elements q and a
            // this takes FOREVER!! SAVE YOUR RESULTS!
            ElGamal.genearte_global_public_elements();
            Console.WriteLine(ElGamal.q);
            Console.WriteLine(ElGamal.a);

            // for every user 
            ElGamal.generate_key_pair();
            Console.WriteLine(ElGamal.Public_key);
            Console.WriteLine(ElGamal.Private_Key);

            // the public info is
            /// public key
            /// q
            /// a
            
            // sender and reciver must share public keys AND global elemnts


            return 0;
        }

    }



}
