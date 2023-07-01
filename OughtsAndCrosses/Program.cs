using System.Globalization;

namespace OughtsAndCrosses
{
    internal class Program
    {
        /// <summary>
        /// Checks if the value is between 0 and 2.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsCorrect(int x)
        {
            return 0 <= x && x <= 2;
        }
        /// <summary>
        /// Displays the field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool Display(Field field)
        {
            
            if (IsDraw(field))
            {
                Console.Clear();
                Console.WriteLine($"+-----+-----+-----+");
                for (int i = 0; i < 3; ++i)
                {
                    Console.Write("|");
                    for (int j = 0; j < 3; ++j)
                    {
                        Console.Write($"  {field[i, j]}  ");
                        Console.Write("|");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"+-----+-----+-----+");
                }
                Console.WriteLine("DRAW");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
                Console.WriteLine("A");
                return true;
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"+-----+-----+-----+");
                for (int i = 0; i < 3; ++i)
                {
                    Console.Write("|");
                    for (int j = 0; j < 3; ++j)
                    {
                        if (field.ActiveRow == i && field.ActiveColumn == j)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                        Console.Write($"  {field[i, j]}  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("|");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"+-----+-----+-----+");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Select an empty cell with arrows, then\npress Enter to place {0} in the cell\n(Esc to return to the main menu).", field.IsPlayersMove ? field.PlayersSign : field.BotsSign);
                return false;
            }
        }

        /// <summary>
        /// Prints the final screen after someone won.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void FinalDisplay(Field field, int[] rows, int[] columns)
        {
            if (rows.Length != 3 || columns.Length != 3)
            {
                throw new ArgumentException("Bad size of arrays");
            }
            if (!(IsCorrect(rows[0]) && IsCorrect(rows[1]) && IsCorrect(rows[2]) &&
                IsCorrect(columns[0]) && IsCorrect(columns[1]) && IsCorrect(columns[2])))
            {
                throw new ArgumentException("Bas content of arrays");
            }
            Console.Clear();
            Console.WriteLine($"+-----+-----+-----+");
            for (int i = 0; i < 3; ++i)
            {
                Console.Write("|");
                for (int j = 0; j < 3; ++j)
                {
                    if (rows[0] == i && columns[0] == j ||
                        rows[1] == i && columns[1] == j ||
                        rows[2] == i && columns[2] == j)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    }
                    Console.Write($"  {field[i, j]}  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.WriteLine();
                Console.WriteLine($"+-----+-----+-----+");
            }
            Console.WriteLine($"{field[rows[0], columns[0]]} WON THE GAME");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            Console.WriteLine("A");
            return;
        }

        /// <summary>
        /// Checks if it is a draw positing on the field.
        /// </summary>
        /// <param name="field"></param>
        /// <returns>True if draw is, false otherwise.</returns>
        public static bool IsDraw(Field field)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (field[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Starts a game.
        /// </summary>
        public static void PlayGame()
        {
            Console.Clear();
            Field field = new Field('X', 'O');
            Display(field);
            while (true)
            {
                string command = Console.ReadKey().Key.ToString();
                try
                {
                    switch (command)
                    {
                        case "UpArrow":
                            --field.ActiveRow;
                            break;
                        case "DownArrow":
                            ++field.ActiveRow;
                            break;
                        case "LeftArrow":
                            --field.ActiveColumn;
                            break;
                        case "RightArrow":
                            ++field.ActiveColumn;
                            break;
                        case "Enter":
                            field[field.ActiveRow, field.ActiveColumn] = ' ';
                            break;
                        case "Escape":
                            Console.WriteLine("A");
                            return;
                        default:
                            Console.Write("\b \b");
                            continue;
                    }
                    int[] x, y;
                    (x, y) = field.Check();
                    if (x[0] != -1)
                    {
                        FinalDisplay(field, x, y);
                        return;
                    }
                    if (Display(field))
                    {
                        return;
                    }
                }
                catch (ArgumentException e)
                {

                }
                catch (Exception e)
                {

                }
            }
        }

        /// <summary>
        /// Prints the main menu screen.
        /// </summary>
        public static void PrintGreetings()
        {
            Console.Clear();
            Console.WriteLine("""
                 _   _                   _     _                          _  _____                             
                | \ | |                 | |   | |         /\             | |/ ____|                            
                |  \| | ___  _   _  __ _| |__ | |_ ___   /  \   _ __   __| | |     _ __ ___  ___ ___  ___  ___ 
                | . ` |/ _ \| | | |/ _` | '_ \| __/ __| / /\ \ | '_ \ / _` | |    | '__/ _ \/ __/ __|/ _ \/ __|
                | |\  | (_) | |_| | (_| | | | | |_\__ \/ ____ \| | | | (_| | |____| | | (_) \__ \__ \  __/\__ \
                |_| \_|\___/ \__,_|\__, |_| |_|\__|___/_/    \_\_| |_|\__,_|\_____|_|  \___/|___/___/\___||___/
                                    __/ |                                                                      
                                   |___/                                                                       

                This is the game "Noughts and Crosses" for two players.
                    Press Enter to start a new game.
                    Press Esc to exit.
                """);
        }
        static void Main(string[] args)
        {
            PrintGreetings();
            while (true)
            {
                string command = Console.ReadKey().Key.ToString();
                switch (command)
                {
                    case "Enter":
                        PlayGame();
                        PrintGreetings();
                        break;
                    case "Escape":
                        Console.WriteLine("A");
                        return;
                    default:
                        Console.Write("\b \b");
                        continue;

                }
            }
        }
    }
}