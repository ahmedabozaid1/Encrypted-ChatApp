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
        public  BigInteger q = BigInteger.One;
        public  BigInteger a = BigInteger.One;
        public BigInteger Private_Key = BigInteger.One;
        public BigInteger Public_key = BigInteger.One;
        public BigInteger Cypher_1 = BigInteger.One;
        public List<BigInteger> Cypher_2 = new List<BigInteger>();

        // size is 60 should be 300 bas ana 3andy marara wa7da (╯°□°）╯︵ ┻━┻
        public static int prime_length = 60;

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
        // called by sender
        public void genearte_global_public_elements()
        {
            PrimeRandNume primeRandNume = new PrimeRandNume();
            
            this.q = primeRandNume.generate(prime_length);
            this.a = GFG.findPrimitive(q);

           
        }
        // recives q and a 
        // called by reciver
        public void set_global_public_elements(BigInteger bigPrimeNumber , BigInteger primetiveRoot)
        {
            this.q = bigPrimeNumber;
            this.a = primetiveRoot;
        }

        public void generate_key_pair()
        {
            RandomBigIntegerGenerator RBI = new RandomBigIntegerGenerator();
            BigInteger RandomNumber = RBI.NextBigInteger(prime_length -1);

            Private_Key = RandomNumber;
            Public_key = BigInteger.ModPow(a, Private_Key, q);

        }


        public void Encrypt(string s, BigInteger Receiver_PublicKey)
        {

            byte[] byt = Encoding.ASCII.GetBytes(s);
            BigInteger plian_number = new BigInteger(byt);
            string plain_string = plian_number.ToString();
            Console.WriteLine("encrypt : \n" + plain_string);

            RandomBigIntegerGenerator RBI = new RandomBigIntegerGenerator();
            BigInteger k = RBI.NextBigInteger(prime_length - 1);

            BigInteger K = BigInteger.ModPow(Receiver_PublicKey, k, q);
            this.Cypher_1 = BigInteger.ModPow(a, k, q);

            string chunck="";
            for (int i = 0; i < plain_string.Length; i++)
            {
                chunck += plain_string[i];
                if(i+1 != plain_string.Length)
                {
                    string next = chunck + plain_string[i + 1];
                    if (BigInteger.Parse(next) >= q)
                    {
                        BigInteger Cyper2 = BigInteger.ModPow(K * BigInteger.Parse(chunck), 1, q);
                        this.Cypher_2.Add(Cyper2);
                        chunck = "";
                    }

                }
                else
                {
                    BigInteger Cyper2 = BigInteger.ModPow(K * BigInteger.Parse(chunck), 1, q);
                    this.Cypher_2.Add(Cyper2);
                    chunck = "";
                }

            }
            if(chunck !="")
            {
                BigInteger Cyper2 = BigInteger.ModPow(K * BigInteger.Parse(chunck), 1, q);
                this.Cypher_2.Add(Cyper2);
            }
        }

        public string Decrypt(BigInteger Cypher1 , List<BigInteger> Cypher2)
        {
            string s;
            string plain_string="";
            BigInteger K = BigInteger.ModPow(Cypher1, this.Private_Key, q);
            BigInteger inverse = ModInverse(K, q);
            
            for (int i = 0;i<Cypher2.Count;i++)
            {
                BigInteger M = BigInteger.ModPow(Cypher2[i] * inverse, 1, q);
                plain_string+= M.ToString();
            }

            Console.WriteLine("Decrypted :  \n" + plain_string);

            byte[] bytes = BigInteger.Parse(plain_string).ToByteArray();
            ASCIIEncoding ascii = new ASCIIEncoding();
            s = ascii.GetString(bytes);
            return s;
        }

        public static int Main(string[] args)
        {

            /// our two peers
            ElGamal Sender = new ElGamal();
            ElGamal Receiver = new ElGamal();
            // sender and reciver must share public keys AND global elemnts
            // the public info is
            /// public key
            /// q
            /// a


            // step 1 sender generate global elements q and a
            // this takes FOREVER!! 

            Sender.genearte_global_public_elements();
            Console.WriteLine("sender q " +Sender.q);
            Console.WriteLine("sender a "+ Sender.a);

            //sender sends the q and a to the reciver
            // some connection magic
            Receiver.set_global_public_elements(Sender.q,Sender.a);

            // for every user generate keys

            //sender
            Sender.generate_key_pair();
            Console.WriteLine("sender public key " + Sender.Public_key);
            Console.WriteLine("sender private key " +Sender.Private_Key);
            //reciver
            Receiver.generate_key_pair();
            Console.WriteLine("Reciver public key " + Receiver.Public_key);
            Console.WriteLine("Reciver private key " + Receiver.Private_Key);



            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");


            string s = "Hello There My name is eriny and this took to loong to do Hello There My name is eriny and this took to loong to do Hello There My name is eriny and this took to loong to do";
            Sender.Encrypt(s, Receiver.Public_key);

            string M = Receiver.Decrypt(Sender.Cypher_1,Sender.Cypher_2);
            if(M == s )
            {
                Console.WriteLine("sucsses");
            }
            else
            {
                Console.WriteLine("something went wrong");
            }
          



            return 0;
        }

    }



}
