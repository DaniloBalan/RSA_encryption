using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] table = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ', ',', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            Console.WriteLine("If you want to Encrypt enter 1, Decrypt - enter 2");
            int answer = Convert.ToInt32(Console.ReadLine());
            

             bool simplequestion(long n)
            {
                if (n < 2) return false;
                if (n == 2) return true;
                for (long i=2;i<n;i++)
                {
                    if (n % i == 0) return false;
                      
                }
                return true;
            }

             long calculate_D(long m)
            {
                long d = m - 1;

                for (long i = 2; i <= m; i++)
                    if ((m % i == 0) && (d % i == 0)) 
                    {
                        d--;
                        i = 1;
                    }

                return d;
            }

            long calculate_E(long d,long m)
            {
                long e = 10;
                while (true)
                {
                    if ((e * d) % m == 1)
                        break;
                    else e++;
                }
                return e;
            }

            List<string> cyphering(string text,long e,long n)
            {
                List<string> newtext = new List<string>();
                BigInteger bi;
                for (int i = 0; i < text.Length; i++)
                {
                    int index = Array.IndexOf(table, text[i]);
                    bi = new BigInteger(index);
                    bi = BigInteger.Pow(bi, (int)e);
                    BigInteger n_ = new BigInteger((int)n);
                    bi = bi % n_;
                    newtext.Add(bi.ToString());
                }
                return newtext;
            }

            string Decoding(List <string> entering,long d, long n)
            {
                string result = "";
                BigInteger bi;
                foreach (string item in entering)
                {
                    bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);
                    BigInteger n_ = new BigInteger((int)n);
                    bi = bi % n_;
                    int index = Convert.ToInt32(bi.ToString());
                    result += table[index].ToString();
                }
                return result;
            }



            if (answer == 1)
            {
                Console.WriteLine("Enter the p");
                long p = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter the q");
                long q = Convert.ToInt64(Console.ReadLine());
                if (simplequestion(p) && simplequestion(q))
                {

                    string text = "";
                    StreamReader reader = new StreamReader("entering.txt");
                    while (!reader.EndOfStream)
                    {
                        text += reader.ReadLine();
                    }
                    reader.Close();

                    long n = p * q;
                    Console.WriteLine("n=" + n);
                    long m = (p - 1) * (q - 1);
                    long d = calculate_D(m);
                    Console.WriteLine("d="+d);
                    long e = calculate_E(d, m);
                    List<string> newtext = cyphering(text, e, n);

                    StreamWriter writer = new StreamWriter("cypher.txt");
                    foreach (string item in newtext)
                        writer.WriteLine(item);
                    writer.Close();

                    Console.WriteLine("Your cypher in 'cypher.txt'. If you want to dectrypt it close RSA and put your cypher in 'cypher.txt' and then start again RSA and choose Decoding.");


                }
                else Console.WriteLine("p or q not simple numbers!Please reastart RSA and write simple q and p.");
            }
            else
            {
                Console.WriteLine("Enter the d");
                long d = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter the n");
                long n = Convert.ToInt64(Console.ReadLine());
                List<string> entering = new List<string>();
                StreamReader reader = new StreamReader("cypher.txt");
                while (!reader.EndOfStream)
                    entering.Add(reader.ReadLine());
                reader.Close();

                string decoded = Decoding(entering, d, n);

                StreamWriter writer = new StreamWriter("decrypted.txt");
                writer.WriteLine(decoded);
                writer.Close();

                Console.WriteLine("Your text in decrypted.txt. Thx for using RSA!");


            }
            Console.ReadLine();
        }
    }
}
