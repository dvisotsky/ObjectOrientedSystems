//David Visotsky Midterm ITCS-3112 
//last updated 3/18/2019

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp1
{
    
    public class Inventory
    {

        string name = "";
        public void setName(string nAme)
        {
            name = nAme;
        }
        public string getName()
        {
            return name;
        }
        //array to store values for 5 different parts
        public int[] parts = new int[5];


        //change inv amount for specific part
        public void editParts(int partNum, int amount)
        {
            parts[partNum] += amount;
        }

        public int getValue(int partNum)
        {
            return parts[partNum];
        }


        public void getValues()
        {
            Console.WriteLine("Part 102: {0}", parts[0]);
            Console.WriteLine("Part 215: {0}", parts[1]);
            Console.WriteLine("Part 410: {0}", parts[2]);
            Console.WriteLine("Part 525: {0}", parts[3]);
            Console.WriteLine("Part 711: {0}", parts[4]);
        }

        
    }

    class Warehouses
    {
         //convert txt file part nums to ints from 0 to 4
        static int partNumConvert(int partNum)
        {
            int value = 0;
            switch (partNum)
            {
                case 102: 
                    break;
                case 215: value = 1;
                    break;
                case 410: value = 2;
                    break;
                case 525: value = 3;
                    break;
                case 711: value = 4;
                    break;
            }
            return value;
        }

        static void Main(string[] args)
        {
           
            //paths to info text files
            var InvPath = Directory.GetCurrentDirectory() + "/Inventory.txt";
            var TransPath = Directory.GetCurrentDirectory() + "/Transactions.txt";

            Console.WriteLine("Path of Inv file: " + InvPath);
            Console.WriteLine("Path of Transactions file: " + TransPath);


            System.IO.StreamReader Invfile = new System.IO.StreamReader(InvPath);
            System.IO.StreamReader Transfile = new System.IO.StreamReader(TransPath);

            //create and initialize 6 warehouses
            Inventory[] houses = new Inventory[6];
            for(int i = 0; i < houses.Length; i++)
            {
                houses[i] = new Inventory();
            }
            houses[0].setName("Atlanta");
            houses[1].setName("Baltimore");
            houses[2].setName("Chicago");
            houses[3].setName("Denver");
            houses[4].setName("Ely");
            houses[5].setName("Fargo");



            Console.WriteLine("\nInitial status of each inventory:");

            // this will be the entire inventory for each warehouse, to be split into separate parts later
             string[] stringoParts = new string[6];

            //iterate through every warehouse
            for (int i = 0; i < stringoParts.Length; i++)
            {
                
                stringoParts[i] = Invfile.ReadLine();                
                
                //iterate through every part
                for (int x = 0; x< houses[i].parts.Length; x++)
                {  
                    string[] partsForWarehouseS = stringoParts[i].Split(',');
                    houses[i].editParts(x, Int32.Parse(partsForWarehouseS[x]));
                  
                }
                
            }

            for(int i =0; i < houses.Length; i++)
            {
                Console.WriteLine("{0} Warehouse stock: ", houses[i].getName());
                houses[i].getValues();
            }
           
            

            //below is the transactions code---------------
            Console.WriteLine("Warehouses initialized, beginning transactions");
            var logFile = File.ReadAllLines(TransPath);
            var transactionList = new List<string>(logFile);
            string[] transactionsLine = transactionList.ToArray();
            string[] transactionSplit = new string[transactionsLine.Length];

            //iterate through every line in Tranactions file
            for (int i =0; i<transactionsLine.Length; i++)
            {
                Console.WriteLine("___Transaction {0}___", i+1);
                string currentPartNumS = transactionsLine[i].Substring(3, 3);

                string rawPartNum = transactionsLine[i].Substring(3, 3);
                int currentPartNum = partNumConvert(Int32.Parse(transactionsLine[i].Substring(3, 3)));
                int currentPartAmount = Int32.Parse(transactionsLine[i].Substring(8, transactionsLine[i].Length-8));
                

                //determine what to do based on first char in each line
                char whatsTheSitch = transactionsLine[i][0];

                if (whatsTheSitch == 'P')
                {
                    int fromHere = 0;
                    int lowestPartCount = 9999;

                    //find house with least amount of current part
                    for (int x = 0; x < houses.Length; x++)
                    {
                        if(houses[x].parts[currentPartNum] < lowestPartCount)
                        {
                            lowestPartCount = houses[x].parts[currentPartNum];
                            fromHere = x;
                        }
                    }

                    houses[fromHere].editParts(currentPartNum, currentPartAmount);
                    Console.WriteLine("Bought {0} of part {1} for {2} warehouse ", currentPartAmount, rawPartNum, houses[fromHere].getName());
                    Console.WriteLine("Current Value for {0} warehouse part {1}: {2}", houses[fromHere].getName(), rawPartNum, houses[fromHere].getValue(currentPartNum));
                }
                else if (whatsTheSitch == 'S')
                {
                    int fromHere = 0;
                    int highestPartCount = 0; 
                    //find ware house with highest amount of current part
                    for (int x = 0; x < houses.Length; x++)
                    {
                        if (houses[x].parts[currentPartNum] > highestPartCount)
                        {
                            highestPartCount = houses[x].parts[currentPartNum];
                            fromHere = x;
                        }
                    }
                    houses[fromHere].editParts(currentPartNum, currentPartAmount * -1);
                    Console.WriteLine("Sold {0} of part {1} from {2} warehouse", currentPartAmount, rawPartNum, houses[fromHere].getName());
                    Console.WriteLine("Current Value for {0} warehouse part {1}: {2}", houses[fromHere].getName(), rawPartNum, houses[fromHere].getValue(currentPartNum));
                }
                Console.WriteLine("");
            }

            Console.WriteLine("Final Inventory Count:");
            for (int i = 0; i < houses.Length; i++)
            {
                Console.WriteLine("{0} Warehouse stock: ", houses[i].getName());
                houses[i].getValues();
            }

            Console.WriteLine("Hit 'Enter' to terminate my existance");
            Console.ReadLine();
        }
    }
}
