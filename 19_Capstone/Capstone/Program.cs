using Capstone.Models;
using Capstone.Views;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find input file
            string inputFileName = "vendingmachine.csv";
            string inputFileDirectory = Directory.GetCurrentDirectory();
            string inputFilePath = "";
            try
            {

                bool fileExists = false;
                while (!fileExists)
                {
                    Directory.SetCurrentDirectory(inputFileDirectory);
                    inputFilePath = Path.Combine(inputFileDirectory, inputFileName);
                    fileExists = File.Exists(inputFilePath);
                    if (!fileExists)
                    {
                        try
                        {
                            if (Directory.Exists(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()))
                            {
                                inputFileDirectory = Directory.GetParent(inputFileDirectory).ToString();
                            }
                            else
                            {
                                Console.WriteLine("Input file not found. Please provide complete path to file:");
                                inputFilePath = Console.ReadLine();
                                inputFileName = Path.GetFileName(inputFilePath);
                                inputFileDirectory = Path.GetDirectoryName(inputFilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            while (!fileExists)
                            {
                                try
                                {
                                    Console.WriteLine("Input file not found. Please provide complete path to file:");
                                    inputFilePath = Console.ReadLine();
                                    inputFileName = Path.GetFileName(inputFilePath);
                                    inputFileDirectory = Path.GetDirectoryName(inputFilePath);
                                    Directory.SetCurrentDirectory(inputFileDirectory);
                                    fileExists = File.Exists(inputFilePath);
                                }
                                catch (DirectoryNotFoundException)
                                {

                                }
                                catch (ArgumentException)
                                {

                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding input file: {ex.Message}");
                Console.ReadKey();
                return;
            }

            VendingMachine vendingMachine = new VendingMachine();

            bool isStocked = vendingMachine.StockFromFile(inputFilePath);
            if (!isStocked)
            {
                Console.WriteLine("Error: Machine not stocked properly.");
                Console.ReadKey();
                return;
            }

            MainMenu mainMenu = new MainMenu(vendingMachine);

            mainMenu.Run();
        }
    }
}
