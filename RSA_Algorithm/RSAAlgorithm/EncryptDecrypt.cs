using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace RSAAlgorithm
{
    internal class EncryptDecrypt
    {
        private List<BigInteger> Tochunks(string s, BigInteger kn)
        {
            List<BigInteger> list = new List<BigInteger>();
            string k = "";
            string temp = "";
            for (int i = 0; i < s.Length; i++)
            {
                temp = k;
                temp += s[i];

                if (BigInteger.Parse(temp) > kn)
                {
                    list.Add(BigInteger.Parse(k));
                    k = "";
                    k += s[i];
                }
                else
                {
                    k += s[i];
                    if (i == s.Length - 1)
                    {
                        list.Add(BigInteger.Parse(k));
                    }
                }
            }
            return list;
        }

        public List<BigInteger> EncryptString(string message, BigInteger e, BigInteger n)
        {

            byte[] byt = Encoding.ASCII.GetBytes(message);
            BigInteger number = new BigInteger(byt);
            message = number.ToString();
            List<BigInteger> m =this.Tochunks(message, n);

            List<BigInteger> cypher = new List<BigInteger>();
            for (int i = 0; i < m.Count; i++)
            {
                BigInteger result = BigInteger.ModPow(m[i], e, n);  /// C = m^e mod n
                cypher.Add(result);
            }

            return cypher;
        }
        public string DecryptString(List<BigInteger> cypher, BigInteger d, BigInteger n)
        {
            List<BigInteger> message = new List<BigInteger>();
          
            for (int i = 0; i < cypher.Count; i++)
            {
                BigInteger result = BigInteger.ModPow(cypher[i], d, n); /// M = c^d mod n
                message.Add(result);
            }
            string plain="";
            for (int i = 0; i < message.Count; i++)
            {
                plain += message[i].ToString();
            }
            byte[] bytes = BigInteger.Parse(plain).ToByteArray();
            ASCIIEncoding ascii = new ASCIIEncoding();
            plain = ascii.GetString(bytes);
            return plain;
        }
        public List<BigInteger> Encrypt(BigInteger message, BigInteger e, BigInteger n)
        {
            string plain = message.ToString();
            List<BigInteger> chunks = new List<BigInteger>();
            chunks = this.Tochunks(plain, n);
            List<BigInteger> cypher = new List<BigInteger>();
            for (int i = 0; i < chunks.Count; i++)
            {
                BigInteger result = BigInteger.ModPow(chunks[i], e, n);  /// C = m^e mod n
                cypher.Add(result);
            }

            return cypher;
        }
        public string Decrypt(List<BigInteger> cypher, BigInteger d, BigInteger n)
        {
            string message="";

            for (int i = 0; i < cypher.Count; i++)
            {
                BigInteger result = BigInteger.ModPow(cypher[i], d, n); /// M = c^d mod n
                message+=result.ToString();
            }
            return message;
        }
    }
}
