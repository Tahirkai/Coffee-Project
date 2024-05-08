using System;
using System.Text;
namespace Chef_Spresso
{
    internal class Program
    {
        public static int GetInput(string question) // Class for getting number of cups with try catch
        {
            int userInput = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine(question);
                    userInput += int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format. Please try again using only numeric values.");
                    continue;
                }
                catch(Exception m)
                {
                    Console.WriteLine(m.Message);
                }
                return userInput;
            }
        }
        public static bool GetMilkInput(string question) // class for adding milk
        {
            bool milkAdd = false;
            string milk;
            while (true)
            {
                Console.WriteLine(question);
                milk = Console.ReadLine().ToLower();
                if (milk == "y" || milk == "yes")
                {
                    milkAdd = true;
                    return milkAdd;
                }
                else if (milk == "n" || milk == "no")
                {
                    milkAdd = false;
                    return milkAdd;
                }
                else
                {
                    Console.WriteLine("Incorrect format. Please try again using Y/Yes or N/No.");
                    continue;
                }
            }
        }
        static void Main()
        {
            while (true) // loop for another customer
            {
                StringBuilder receipt = new StringBuilder();
                double totalAmount = 0;
                int numMilk = 0;
                int numCups = 0;
                int totalCups = 0;
                // Initial greeting
                Console.WriteLine("-------------Welcome to Chef//Spresso Cafe!!!!------------");
                Console.WriteLine("May I know your name, coffee enthusiast?");
                try
                {
                    string name = Console.ReadLine().ToLower();
                    Console.WriteLine("Hello " + name + "! I am here to make your day caffeinated and delightful!\n");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter your name using alphabetical characters");
                }
                catch(Exception n)
                {
                    Console.WriteLine(n.Message);
                }
                Console.WriteLine("Please note that you will receive a 10% discount should you order 20 cups or more!!!");
                bool continueOrdering = true; //Declare boolen for continueOrdering as true
                while (continueOrdering) //Will keep executing as long as its true
                {
                    CoffeeMenu coffeeMenu = new CoffeeMenu();  // Instance for coffee menu
                    string orderedCoffee = coffeeMenu.DisplayMenu(); // Method for displaying coffee
                    SizeMenu sizeMenu = new SizeMenu(); // Instance for size menu
                    double price = sizeMenu.SelectSize(); // Method for calling size menu
                    try
                    {
                        // Asking user how many cups they would want
                        numCups = GetInput("Please enter the number of cups you would like to order: (e.g 5)");
                        totalCups += numCups;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid selection. Please try again using numerical values");
                    }
                    catch (Exception o)
                    {
                        Console.WriteLine(o.Message);
                    }
                    // Asking the user if they would like milk
                    bool milkChoice = GetMilkInput($"Would you like to add milk at an additional charge of R1.50 per cup?\nPlease select either Y/N :\n");
                    if (milkChoice)
                    {
                        // Prompt the user to enter the number of cups with milk
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"You have selected to add milk for R1.50 per cup.\n");
                                Console.WriteLine("How many cups with milk?\n");
                                numMilk = int.Parse(Console.ReadLine());
                                // Check if the number of milk cups is less than or equal to the total number of cups
                                if (numMilk <= numCups && numMilk>=1)
                                {
                                    break; // Exit the loop if the input is valid
                                }
                                else
                                {
                                    Console.WriteLine($"Milk cups must be less than or equal to the number of {orderedCoffee} cups ordered");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid selection. Please use the correct numerical format.");
                            }
                            catch (Exception p)
                            {
                                Console.WriteLine(p.Message);
                            }
                        }
                    }
                    else
                    {
                        // If the user chooses not to add milk, set numMilk to 0
                        numMilk = 0;
                        Console.WriteLine("You have not chosen to add any additional milk");
                    }
                    // Calculating subtotal for each order
                    double subTotal = (price * numCups) + (numMilk * 1.50);
                    totalAmount += subTotal;
                    double discount = 0.1;
                    // Append the current order details to the receipt
                    receipt.AppendLine($"{numCups} {orderedCoffee}, {numMilk} with milk, Subtotal: R {subTotal}");
                    // Loop for editing and adding orders
                    while (true)
                    {
                        Console.WriteLine("Would you like to order another coffee? (Y/N)");
                        string input = Console.ReadLine().ToLower();
                        if (input == "y" || input == "yes")
                        {
                            continueOrdering = true;
                            break;
                        }
                        if (input == "n" || input == "no")
                        {
                            //Print receipt and continue to payment
                            continueOrdering = false;
                            Console.WriteLine("Continue to payment");
                            Console.Clear();
                            // Append the current order details to the receipt
                            if (totalCups >= 20)
                            {
                                double roundedDiscount = Math.Round(totalAmount * discount, 2);
                                totalAmount = totalAmount - (totalAmount * discount);                               
                                receipt.AppendLine($"Discount: R {roundedDiscount}");
                            }
                            receipt.AppendLine($"Total Cost: R {totalAmount}");
                            // Reset totalAmount or perform other actions as needed
                            totalAmount = 0;
                            // Exit the iner size menu loop
                            break;
                        }
                        else if (input != "y" || input != "n" || input != "yes" || input != "no")
                        {
                            // Handle invalid input
                            Console.WriteLine("Invalid input. Please enter Y/Yes or N/No");
                            continue;
                        }
                    }
                    Console.Clear();
                }
                // Display the final receipt
                Console.WriteLine("\n*********** Receipt ***********");
                Console.WriteLine(receipt.ToString());
                Console.WriteLine("*********************************");
                Console.WriteLine($"Thank you for ordering with Chef/Spresso!\nHave a lovely day!!\nAnd remember to KEEP COOKING!!!!!!!!!!");
                //Coffee Logo
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ( (");
                Console.WriteLine("   ) )");
                Console.WriteLine(" ........");
                Console.WriteLine("|        |]");
                Console.WriteLine("\\        /");
                Console.WriteLine(" `------'");
                Console.ResetColor();
                // Countdown before starting a new order for the next customer
                Console.WriteLine("\nStarting a new order for the next customer in:");
                for (int i = 10; i >= 1; i--)
                {
                    Console.WriteLine(i);
                    System.Threading.Thread.Sleep(1000); // Pause for 1 second
                }
                Console.Clear(); // Clear the console for the new customer
            }
        }
    }
    public class CoffeeMenu // Coffee menu class
    {
        public string DisplayMenu() // Method for displaying menu
        {
            Console.WriteLine("***************************Coffee Menu***************************************");
            string[] coffeeMenu = { "Espresso", "Cappuccino", "Latte", "Mocha", "Americano" };
            // Displaying coffee menu
            for (int i = 0; i < coffeeMenu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {coffeeMenu[i]}");
            }
            // User inputs coffee selection
            Console.Write("Please select the coffee that you would like to order (1-5):\n ");
            int choice;
            bool isValidChoice;
            do //  do-while loop to repeatedly prompt the user until a valid choice is made
            {
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);
                if (!isValidChoice || choice < 1 || choice > coffeeMenu.Length) //Check if the choice is not valid
                {
                    Console.WriteLine("Invalid selection, please select a number between 1 and 5.");
                }
            }
            while (!isValidChoice || choice < 1 || choice > coffeeMenu.Length); // Continue looping as long as the choice is not valid
            return coffeeMenu[choice - 1]; // When a valid choice is made, return the coffeeMenu item that = users choice
        }
    }
    public class SizeMenu // Size menu class
    {
        public double SelectSize() // Method for size selection
        {
            string[] sizeOptions = { "Small R13.50", "Medium R18.50 ", "Large R23.50" };
            // Display available size options
            Console.WriteLine("\nThe following size options are currently available:\n ");
            Console.WriteLine("\nSize Options:");
            for (int i = 0; i < sizeOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {sizeOptions[i]}");
            }
            double price = 0;
            int size;
            bool isValidSize;
            do // Validating user selection within our parameters
            {
                Console.Write("Please select the number of your size that you would like to order (1-3): \n");
                isValidSize = int.TryParse(Console.ReadLine(), out size);
                if (!isValidSize || size < 1 || size > 3)
                {
                    Console.WriteLine("Invalid selection, please select from the available size options (1-3).\n");
                }
            }
            while (!isValidSize || size < 1 || size > 3);
            // Assign price based on size
            if (size == 1)
            {
                price = 13.50;
            }
            else if (size == 2)
            {
                price = 18.50;
            }
            else if (size == 3)
            {
                price = 23.50;
            }
            return price;
        }
    }
}