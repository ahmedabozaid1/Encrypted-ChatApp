﻿using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
namespace RSAAlgorithm;
class RSA
{
    static public void Main(String[] args)
    {
        KeyGeneration key = new KeyGeneration();
        RSA rsa = new RSA();
         string ins = "Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles // Hello its Me I was Wondering if after all this years you'd like to meet to go over everything. Hellooooooo can you hear me I'm in California dreaming about who we used to be When we were younger and free I've forgotten how it felt before the world fell at our feet There's such a difference between us And a million miles";
          RandomNumberGenerator rng = RandomNumberGenerator.Create();
         byte[] bytes = new byte[256];
          BigInteger a;
          rng.GetBytes(bytes);
          a = new BigInteger(bytes);
          a = BigInteger.Abs(a*a*a*a*a*a*a*a*a*a*a*a*a*a);

        Console.WriteLine(a);

        EncryptDecrypt encryptDecrypt = new EncryptDecrypt();


          List<BigInteger> cypher = encryptDecrypt.Encrypt(a, key.e, key.n);

          string plain = encryptDecrypt.Decrypt(cypher, key.d, key.n);
        //  Console.WriteLine("decrypted");

        //Console.WriteLine(plain);
        ///     List<BigInteger> cypher = encryptDecrypt.EncryptString(ins, key.e, key.n);

        //string plain = encryptDecrypt.DecryptString(cypher, key.d, key.n);

       
        Console.WriteLine("decrypted");


        Console.WriteLine("");

        Console.WriteLine(plain);
    
    
    }
}

