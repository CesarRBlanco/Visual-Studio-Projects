﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServEx06
{
    class Program
    {
        static readonly private object l = new object();
        public static int displayNumber = 0;
        public static string displayText = displayNumber.ToString();
        public static bool finish = false;
        public static bool displayStop = false;
        public static bool stopColor = false;

        static void Main(string[] args)
        {
            Thread player1 = new Thread(add);
            player1.Start(1);
            Thread player2 = new Thread(add);
            player2.Start(2);
            Thread _display = new Thread(display);
            _display.Start();
        }

        public static void add(object code)
        {
            int turn, sleepTIme;
            while (!finish)
            {
                lock (l)
                {
                    Random r = new Random();
                    Random rS = new Random();
                    turn = r.Next(1, 11);
                    sleepTIme = rS.Next(100, 100 * turn);
                    Thread.Sleep(sleepTIme);

                    switch (code)
                    {
                        case 1:
                            Console.SetCursorPosition(0, 1);
                            Console.WriteLine(String.Format("{0,2}", turn));
                            if (turn == 5 || turn == 7)
                            {

                                if (displayStop)
                                {
                                    displayNumber += 5;
                                }
                                else
                                {
                                    displayNumber++;
                                    displayStop = true;
                                }
                                displayText = displayNumber.ToString();
                                displayText = String.Format("{0,3}", displayNumber.ToString());
                                Console.SetCursorPosition(1, 0);
                                Console.WriteLine(displayText);

                                if (displayNumber >= 20)
                                {
                                    finish = true;
                                }
                            }
                            break;
                        case 2:
                            Console.SetCursorPosition(3, 1);
                            Console.WriteLine(String.Format("{0,2}", turn));
                            if (turn == 5 || turn == 7)
                            {
                                if (displayStop)
                                {
                                    displayNumber--;
                                    displayStop = false;
                                }
                                else
                                {
                                    displayNumber -= 5;
                                }
                                displayText = String.Format("{0,3}", displayNumber.ToString());
                                Console.SetCursorPosition(1, 0);
                                Console.WriteLine(displayText);
                                if (displayNumber <= -20)
                                {
                                    finish = true;
                                }
                            }
                            break;
                    }
                }

            }
        }

        public static void display()
        {
       
            int consoleColor;
            ConsoleColor[] colorArray = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green };
            Random rC = new Random();
            while (!finish)
            {
                consoleColor = rC.Next(0, 3);
                Console.ForegroundColor = colorArray[consoleColor];

            }

            

        }
    }
}
