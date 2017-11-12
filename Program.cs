using System;

namespace TicTacToe
{
    class Program
    {
        // Tic Tac Toe game tutorial code 
        // written by Jim Armstrong (www.jimwritescode.com)

        // First let's initialize the board - we do this here so it will be in scope and easily used by the rest of our program
        static char[] boardState = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        // Main program entry function - we will use this to call our other functions
        static void Main(string[] args)
        {
            // Let's ask the user if they want to play... if not, exit
            if (AskUserToPlayGame() == false)
            {
                Environment.Exit(0);
            }

            // Randomize which side the user will play
            char userSide = RandomizeUserSide();

            Console.WriteLine($"You will be playing as {userSide}");

            // Start the game!
            RunGame(userSide);
        }

        // This function queries the user to see if they want to play
        static bool AskUserToPlayGame()
        {
            // Loop until a return condition is met... this allows us to repeat the question if invalid input is given
            while (true)
            {
                Console.WriteLine("Welcome to Tic Tac Toe! Do you want to challenge me? (Hit Y for Yes, N for No):");

                // Convert the user's input to uppercase -- this way we only have to check for "Y" and "N" and not lowercase variants
                string playGame = Console.ReadLine().ToUpper();

                switch (playGame)
                {
                    case "Y": // Great, they want to play! Return true
                        Console.WriteLine("Great, let's begin!");
                        return true;
                    case "N": // Maybe next time... Return false
                        Console.WriteLine("Too bad, I was looking forward to beating you...");
                        return false;
                    default: // Input was not "Y" or "N" -- repeat the question
                        Console.WriteLine("You entered an invalid choice");
                        continue;
                }
            }
        }

        // This function randomizes if the user starts as X or O
        static char RandomizeUserSide()
        {
            // This is a quick way to get what is essentially a random bool
            Random randomizer = new Random();
            return randomizer.Next(2) == 0 ? 'X' : 'O';
        }

        // This is our main game logic
        static void RunGame(char userSide)
        {
            // Make sure the user is ready to go, then clear the screen
            Console.WriteLine("Hit Enter when you are ready to start...");
            Console.ReadLine();
            Console.Clear();

            // X goes first... if userSide is X, user goes first
            bool playerMove = userSide.Equals('X');

            // Set our won condition to false
            bool gameWon = false;

            // While gameWon is false, flip between user and computer moves
            while (!gameWon)
            {
                if (playerMove)
                {
                    UserMove(userSide);
                    playerMove = false;
                }
                else
                {
                    ComputerMove(userSide);
                    playerMove = true;
                }

                // Check for end condition
                gameWon = CheckForWin(userSide);
            }
        }

        // User move logic
        static void UserMove(char userSide)
        {
            Console.Clear();
            Console.WriteLine("Your move...choose the number for the spot you want:");
            PrintBoard();

            while (true)
            {
                // Try and convert user input to an integer
                bool result = int.TryParse(Console.ReadLine(), out int userMove);

                // Check if conversion was successful and number is between 1 and 9
                if (result && userMove >= 1 && userMove <= 9)
                {
                    // Check if spot is already taken...
                    if (!boardState[userMove - 1].Equals('X') && !boardState.Equals('O'))
                    {
                        boardState[userMove - 1] = userSide;
                        return;
                    }

                    Console.WriteLine("Sorry, that spot is already taken...");
                }

                Console.WriteLine("Invalid choice, try again...");
            }
        }

        // Computer move logic
        // The computer does not use any logic in picking its spot...it just tries random numbers until it gets a valid move. For a later project we could look at making this a bit smarter.
        static void ComputerMove(char userSide)
        {
            // Set the computer's side (oppposite of user's side) so we can mark the spot correctly
            char computerSide = userSide.Equals('X') ? 'O' : 'X';

            // Set valid move to false so we can loop if randomizer hits an invalid spot
            bool validMove = false;

            Console.Clear();
            Random randomizer = new Random();

            while (!validMove)
            {
                // Get a random number between 0 and 8
                int proposedMove = randomizer.Next(9);

                // Check if that spot is taken -- if so, try again
                if (!boardState[proposedMove].Equals('X') && !boardState[proposedMove].Equals('O'))
                {
                    // Spot was not taken, mark the spot and exit the loop
                    boardState[proposedMove] = computerSide;
                    validMove = true;

                    // Now we know the result of the move, print a status update and the current board state
                    Console.WriteLine($"Now I move... I choose spot # {proposedMove + 1}");
                    PrintBoard();
                    Console.WriteLine("Hit Enter to continue...");
                    Console.ReadLine();
                }

            }
        }


        // This is a brute force method to check for a win. Tic Tac Toe has 8 possible win conditions -- three vertical, three horizontal, and two diagonal
        // We will just run a series of if statements to check for each condition, both for X and O
        // I'll leave it as a practice problem for you to see if you can find a more efficient way to do this
        static bool CheckForWin(char userSide)
        {
            if (
                // Horizontal
                (boardState[0].Equals('X') && boardState[1].Equals('X') && boardState[2].Equals('X')) ||
                (boardState[3].Equals('X') && boardState[4].Equals('X') && boardState[5].Equals('X')) ||
                (boardState[6].Equals('X') && boardState[7].Equals('X') && boardState[8].Equals('X')) ||
                
                //Vertical
                (boardState[0].Equals('X') && boardState[3].Equals('X') && boardState[6].Equals('X')) ||
                (boardState[1].Equals('X') && boardState[4].Equals('X') && boardState[7].Equals('X')) ||
                (boardState[2].Equals('X') && boardState[5].Equals('X') && boardState[8].Equals('X')) ||

                // Diagonal
                (boardState[0].Equals('X') && boardState[4].Equals('X') && boardState[8].Equals('X')) ||
                (boardState[2].Equals('X') && boardState[4].Equals('X') && boardState[6].Equals('X')))
            {
                // Win condition found
                Console.WriteLine("X wins!!!");
                Console.WriteLine(userSide.Equals('X') ? "You got lucky that time!" : "The power of Intel processors strikes again!");
                return true;
            }
            else if (
                // Horizontal
                (boardState[0].Equals('O') && boardState[1].Equals('O') && boardState[2].Equals('O')) ||
                (boardState[3].Equals('O') && boardState[4].Equals('O') && boardState[5].Equals('O')) ||
                (boardState[6].Equals('O') && boardState[7].Equals('O') && boardState[8].Equals('O')) ||

                // Vertical
                (boardState[0].Equals('O') && boardState[3].Equals('O') && boardState[6].Equals('O')) ||
                (boardState[1].Equals('O') && boardState[4].Equals('O') && boardState[7].Equals('O')) ||
                (boardState[2].Equals('O') && boardState[5].Equals('O') && boardState[8].Equals('O')) ||

                // Diagonal
                (boardState[0].Equals('O') && boardState[4].Equals('O') && boardState[8].Equals('O')) ||
                (boardState[2].Equals('O') && boardState[4].Equals('O') && boardState[6].Equals('O')))
            {
                // Win condition found
                Console.WriteLine("O wins!!!");
                Console.WriteLine(userSide.Equals('O') ? "You got lucky that time!" : "The power of Intel processors strikes again!");
                return true;

                // One last condition to check for -- are all the spots taken? We check to see if there are no spots with numerical values (free spots)
                // Again, this is very brute force...can you think of a better way?
            } else if (
                !boardState[0].Equals('1') &&
                !boardState[1].Equals('2') &&
                !boardState[2].Equals('3') &&
                !boardState[3].Equals('4') &&
                !boardState[4].Equals('5') &&
                !boardState[5].Equals('6') &&
                !boardState[6].Equals('7') &&
                !boardState[7].Equals('8') &&
                !boardState[8].Equals('9'))
            {
                // No spots left, print a tie condition
                Console.Clear();
                PrintBoard();
                Console.WriteLine("No moves left -- it's a tie!");
                return true;
            }

            // No one has won and there are open spots, we can continue on...
            return false;
        }

        // Quick function for printing the current board state
        static void PrintBoard()
        {
            Console.WriteLine($"\n\n {boardState[0]} | {boardState[1]} | {boardState[2] }  ");
            Console.WriteLine("-----------");
            Console.WriteLine($" {boardState[3]} | {boardState[4]} | {boardState[5]} ");
            Console.WriteLine("-----------");
            Console.WriteLine($" {boardState[6]} | {boardState[7]} | {boardState[8]} \n\n");
        }
    }
}
