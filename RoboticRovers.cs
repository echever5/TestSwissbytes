using System;
using System.IO;

namespace Swissbytes
{
    public class Program
    {

        public static int ParseInt(string token)
        {
            int result;
            if (!int.TryParse(token, out result))
            {
                throw new FormatException();
            }
            return result;
        }

        /// <summary>
        /// Get new orientation base in curent orientation and instruction
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static char GetNewOrientation(char orientation, char instruction)
        {
            char result = ' ';
            switch (orientation)
            {
                case 'N':
                    if (instruction == 'L')
                        result = 'W';
                    else if (instruction == 'R')
                        result = 'E';
                    break;
                case 'E':
                    if (instruction == 'L')
                        result = 'N';
                    else if (instruction == 'R')
                        result = 'S';

                    break;
                case 'S':
                    if (instruction == 'L')
                        result = 'E';
                    else if (instruction == 'R')
                        result = 'W';

                    break;
                case 'W':
                    if (instruction == 'L')
                        result = 'S';
                    else if (instruction == 'R')
                        result = 'N';

                    break;
                default:
                    break;
            }
            return result;
        }


        public static void Main(string[] args)
        {
            string line;
            int count = 0;

            // Read the input file .
            StreamReader file = new StreamReader("D:\\Projects\\test.txt");

            // read the upper-right coordinates of the plateau
            line = file.ReadLine();
            string[] tokens = line.Split(' ');

            int upper = ParseInt(tokens[0]);
            int right = ParseInt(tokens[1]);

            // read rover initial coord and instruction one by one
            while ((line = file.ReadLine()) != null)
            {
                count++;
                string roverName = "Rover " + count;
                Console.WriteLine("Reading coords for rover {0}", roverName);

                int coordX, coordY;
                char orientation;
                tokens = line.Split(' ');
                if (tokens != null && tokens.Length != 3)
                    throw new FormatException();

                coordX = ParseInt(tokens[0]);
                coordY = ParseInt(tokens[1]);
                orientation = tokens[2][0];


                line = file.ReadLine();
                foreach (var instruction in line)
                {

                    if (instruction == 'L' || instruction == 'R')
                    {
                        orientation = GetNewOrientation(orientation, instruction);
                    }
                    else if (instruction == 'M')
                    {
                        switch (orientation)
                        {
                            case 'N':
                                coordY += 1;
                                break;
                            case 'E':
                                coordX += 1;
                                break;
                            case 'S':
                                coordY -= 1;
                                break;
                            case 'W':
                                coordX -= 1;
                                break;
                            default:
                                break;
                        }
                    }

                    if (coordY > upper || coordX > right)
                        Console.WriteLine("Rover {0} is out of the plateau ({1},{2})",roverName, coordX, coordY);
                }

                Console.WriteLine("Rover {0} final coordinates ({1},{2}) {3}", roverName, coordX,coordY,orientation);


                // Suspend the screen.
                Console.ReadLine();
            }
            file.Close();

        }
    }
}
