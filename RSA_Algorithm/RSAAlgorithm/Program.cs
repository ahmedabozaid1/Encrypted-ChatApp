using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
namespace RSAAlgorithm;
class RSA
{
    
   public List<BigInteger> Tochunks(string s,BigInteger kn)
   {
        List<BigInteger> list = new List<BigInteger>();
        string k = "";
        string temp = "";
        for (int i = 0; i < s.Length; i++)
        {
            temp = k;
            temp += s[i];
            BigInteger n = BigInteger.Parse(temp);
            if (n > kn)
            {
                n = BigInteger.Parse(k);
                if (k[0] == '0')
                {
                    Console.WriteLine("ahhhhhhh");
                }
                list.Add(n);
                k = "";
            }
            else
            {
                k += s[i];
                if (k[0] == '0')
                {
                    Console.WriteLine("ahhhhhhh");
                }
                if (i == s.Length-1)
                {
                    n = BigInteger.Parse(k);
                    list.Add(n);
                }
            }
        }
        return list;
   } 


    
    public string numberInput(BigInteger number)
    {
        string s = number.ToString();
        return s;
    }
    static public void Main(String[] args)
    {
        KeyGeneration key = new KeyGeneration();
        RSA rsa = new RSA();
         string ins = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaazdsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
          RandomNumberGenerator rng = RandomNumberGenerator.Create();
         byte[] bytes = new byte[256];
          BigInteger a;
          rng.GetBytes(bytes);
          a = new BigInteger(bytes);
          a = BigInteger.Abs(a);

        Console.WriteLine(a);

        EncryptDecrypt encryptDecrypt = new EncryptDecrypt();


///        List<BigInteger> cypher = encryptDecrypt.Encrypt(a, key.e, key.n);

   ///     string plain = encryptDecrypt.Decrypt(cypher, key.d, key.n);
      //  Console.WriteLine("decrypted");

        //Console.WriteLine(plain);
        List<BigInteger> cypher = encryptDecrypt.EncryptString(ins, key.e, key.n);
    
        string plain = encryptDecrypt.DecryptString(cypher, key.d, key.n);
        Console.WriteLine("decrypted");


        Console.WriteLine("");

        Console.WriteLine(plain);
    
        if (plain == ins)
        {

            
            Console.WriteLine("similar");
        }
        else
        {
            Console.WriteLine("different");
        }
    
    }
}

