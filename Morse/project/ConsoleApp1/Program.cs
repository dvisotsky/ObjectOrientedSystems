using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;



namespace ConsoleApp1
{
    class Program
    {
       

        static void Main(string[] args)
        {
            bool end = false;
            var path = Directory.GetCurrentDirectory()+"/Morse.txt";
            string output = "";
            Console.WriteLine("Path of key file: "+path);
            
            System.IO.StreamReader file = new System.IO.StreamReader(path);


            string[] key = File.ReadLines(path).ToArray();

            while (!end)
            {
                Console.WriteLine("Enter phrase: ");
                string phrase = Console.ReadLine();

                string cipher = phrase.ToUpper();
                

                for (int i = 0; i < phrase.ToCharArray().Length; i++)
                {
                    bool found = false;

                    for (int x = 0; x < key.Length; x++)
                    {
                        if (!found)
                        {
                            if (cipher[i].Equals(key[x][0]))
                            {
                                output = String.Concat(output + key[x].Remove(0, 2) + " ");
                                found = true;
                            }
                            else if (Char.IsWhiteSpace(cipher[i]))
                            {
                                output = String.Concat(output + " ");
                                found = true;
                            }
                        }
                        
                    }
                }

                Console.WriteLine("\nMorse code: " + output);
                output = "";
                Console.WriteLine("Again? (y/n)");
                if(Console.ReadLine() == "n")
                {
                    end = true;
                }
            }
            Console.WriteLine("Ending Session");
            Console.ReadLine();
        }
    }
}
