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
            string chunck = "";
            List<string> chunck_ = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                chunck += s[i];

                if (i + 1 != s.Length)
                {
                    string next = chunck + s[i + 1];
                    if (BigInteger.Parse(next) >= kn)
                    {
                        if (s[i + 1] == '0')
                        {

                            // find the first non zero 
                            // sting c is x000.. 
                            int index = chunck.Length - 1;
                            for (int j = chunck.Length - 1; j > 0; j--)
                            {
                                if (chunck[j] != '0')
                                {
                                    index = j;
                                    break;
                                }
                            }
                            string c = "";
                            for (int j = index; j < chunck.Length; j++)
                            {
                                c += chunck[j];
                            }

                            chunck = chunck.Remove(index);

                          
                            chunck_.Add(chunck);
                            chunck = "" + c;

                        }
                        else
                        {
                            
                            chunck_.Add(chunck);
                            chunck = "";
                        }

                    }
                }
                else
                {
             
                    chunck_.Add(chunck);
                    chunck = "";
                }

            }
            List<BigInteger> list = new List<BigInteger>();
            for (int i = 0; i < chunck_.Count; i++)
            {
                list.Add(BigInteger.Parse(chunck_[i]));
            }
            return list;
        }
        /*
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
                    ///////
                    ///

                    list.Add(BigInteger.Parse(k));
                    k = "";


                    k += s[i];
                }
                else
                {
                    if(k=="")
                    {
                        while (true)
                        {
                            if (s.Substring(i, 1) == "0")
                            {
                                string r = list[list.Count - 1].ToString();
                                r += "0";
                                list[list.Count - 1] = BigInteger.Parse(r);
                                i++;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    k += s[i];
                    if (i == s.Length - 1)
                    {
                        list.Add(BigInteger.Parse(k));
                    }
                }

            }
            List<BigInteger> list2 = new List<BigInteger>();
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i]>kn)
                {
                    string r = list[i].ToString();

                    var firstHalfLength = (int)(r.Length / 2);
                    var secondHalfLength = r.Length - firstHalfLength;
                    var split = new[]
                    {
                                 r.Substring(0, firstHalfLength),
                                 r.Substring(firstHalfLength, secondHalfLength)
                    };
                 
                    

                    list2.Add(BigInteger.Parse(split[0]));
                    list2.Add(BigInteger.Parse(split[1]));
                }
                else
                {
                    list2.Add(list[i]);
                }
            }
            for (int i = 0; i < list2.Count(); i++)
            {
                Console.WriteLine(list2[i]);
            }
            return list2;
        }
 */

        public List<BigInteger> EncryptString(string message, BigInteger e, BigInteger n)
        {

            byte[] byt = Encoding.ASCII.GetBytes(message);
            BigInteger number = new BigInteger(byt);
            message = number.ToString();
            Console.WriteLine("////");
            Console.WriteLine("plain");
            Console.WriteLine(message);
          
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
            Console.WriteLine("plain ");
            Console.WriteLine(plain);
            List<BigInteger> chunks = new List<BigInteger>();
            chunks = this.Tochunks(plain, n);
            List<BigInteger> cypher = new List<BigInteger>();
            for (int i = 0; i < chunks.Count; i++)
            {
              
               
                BigInteger result = BigInteger.ModPow(chunks[i], e, n);  /// C = m^e mod n
                cypher.Add(result);
            }
            Console.WriteLine("//////////////////");
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
