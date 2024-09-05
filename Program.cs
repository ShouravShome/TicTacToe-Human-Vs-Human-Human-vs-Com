using System;
using System.Text.RegularExpressions;

namespace ConsoleApp3
{
    abstract class Game
    {
        static char gameType;
        static int winFlag = 0;
        static int playerNumber=1;
        static int position;
        static int numericValue;
        static int[] moveArr = new int[10];
        static string[] stringArray = new string[moveArr.Length];
        static bool saveExpired = false;
        static string time = "";
        public static int Position
        {            
            get { return position; }
            set { position = value; }
        }
        public static int NumericValue
        {      
            get { return numericValue; }
            set
            {
                numericValue = value;
                moveArr[position] = value;
            }
        }
        public int[] Arr()
        {
            return moveArr;          
        }
        public static string[] StringArr()
        {
            for (int i = 1; i < moveArr.Length; i++)
            {
                stringArray[i] = moveArr[i].ToString();
            }
            return stringArray;
        }
        public static int PlayerNumber
        {
            get { return playerNumber; }
            set { playerNumber = value; }
        }
        public static int WinFlag
        {
            get { return winFlag; }
            set { winFlag = value; }
        }
        public static char GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        public static bool SaveExpired
        {
            get { return saveExpired; }
            set { saveExpired = value; }
        }
        public static string Time
        {
            get { return time; }
            set { time = value; }
        }
        public virtual void displayBoard()
        {
            Console.WriteLine("");
        }
        public virtual void makeMove()
        {
            Console.WriteLine("");
        }
        public virtual void giveValue()
        {
            Console.WriteLine("");
        }
        public virtual void selectPlayer(char check)
        {
            Console.WriteLine("");
        }
        public virtual int checkWin()
        {
            return 0;
        }
        public virtual void computerMakeMove()
        {
            Console.WriteLine();
        }
        public virtual void undoPlayer()
        {
            Console.WriteLine();
        }
        public virtual void redoPlayer()
        {
            Console.WriteLine();
        }
        public virtual void undoComputer()
        {
            Console.WriteLine("  ");
        }
        public virtual void redoComputer()
        {
            Console.WriteLine("  ");
        }
        public virtual void saveGame()
        {
            Console.WriteLine();
        }
        public virtual void loadGame()
        {
            Console.WriteLine();
        }
        public virtual void viewHelp()
        {
            Console.WriteLine();
        }
    }
    class Board : Game
    {
        string[] stringArray;
        public Board()
        {
            this.stringArray = StringArr();
        }
        public override void displayBoard() 
        {
            for (int i = 1; i < stringArray.Length; i++)
            {
                if (stringArray[i].Equals("0"))
                {
                    stringArray[i] = " ";
                }
            }
            Console.WriteLine("  ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", stringArray[1], stringArray[2], stringArray[3]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", stringArray[4], stringArray[5], stringArray[6]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", stringArray[7], stringArray[8], stringArray[9]);
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  ");
        }

    }
    class Move : Game
    {
        int output;
        int[] arr;
        
        public Move()
        {
            this.arr = Arr(); 
        }
        public override void makeMove() // select position in board
        {
            if (Game.PlayerNumber % 2 == 0)
            {
                bool isValid = false;
                string checkString;
                checkString = Console.ReadLine();
                while (isValid == false)
                {
                    if (checkString == "Undo2")
                    {
                        Tracker tracker = new Tracker();
                        tracker.undoPlayer();
                        isValid = true;
                    }
                    else if (checkString == "Redo2")
                    {
                        Tracker tracker = new Tracker();
                        tracker.redoPlayer();
                        isValid = true;
                    }
                    else if(checkString == "Save")
                    {
                        Save save = new Save();
                        save.saveGame();
                        isValid = true;
                    }
                    else if(checkString == "0")
                    {
                        Environment.Exit(0);
                    }
                    else if (int.TryParse(checkString, out output) == true && (((output <= 9) && (output >= 1))) && arr[output] == 0)
                    {
                        Position = output;
                        Tracker tracker = new Tracker();
                        tracker.addPositions();
                        giveValue();
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("XXXXXXXXXXXXXXXXXX");
                        Console.Write("Please select correct input: ");
                        checkString = Console.ReadLine();
                        isValid = false;
                    }

                }
            }
            else
            {
                bool isValid = false;
                string checkString;
                checkString = Console.ReadLine();
                while (isValid == false)
                {
                    if (checkString == "Undo1")
                    {
                        if (GameType == 'C')
                        {
                            Tracker tracker = new Tracker();
                            tracker.undoComputer();
                            isValid = true;
                        }
                        else
                        {
                            Tracker tracker = new Tracker();
                            tracker.undoPlayer();
                            isValid = true; ;
                        }
                    }
                    else if (checkString == "Save")
                    {
                        Save save = new Save();
                        save.saveGame();
                        isValid = true;
                    }
                    else if (checkString == "0")
                    {
                        Environment.Exit(0);
                    }
                    else if (checkString == "Redo1")
                    {
                        if(GameType == 'C')
                        {
                            Tracker tracker = new Tracker();
                            tracker.redoComputer();
                            isValid = true;
                        }
                        else
                        {
                            Tracker tracker = new Tracker();
                            tracker.redoPlayer();
                            isValid = true;
                        }
                    }
                    else if (int.TryParse(checkString, out output) == true && (((output <= 9) && (output >= 1))) && arr[output] == 0)
                    {
                        Position = output;
                        Tracker tracker = new Tracker();
                        tracker.addPositions();
                        giveValue();
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("XXXXXXXXXXXXXXXXXX");
                        Console.Write("Please select correct input: ");
                        checkString = Console.ReadLine();
                    }

                }
            }
        }
        public override void computerMakeMove()
        {
            Random random = new Random();
            int randomPosition = random.Next(1,9);
            while (arr[randomPosition]!= 0)
            {
                randomPosition = random.Next(1, 9);
            }
            Position = randomPosition;
            int randomValue = random.Next(1,5) * 2;
            while (arr.Contains(randomValue))
            {
                randomValue = random.Next(1, 5) * 2;
            }
            NumericValue = randomValue;
            PlayerNumber++;
            Tracker tracker = new Tracker();
            tracker.addPositionsComputer(randomPosition);
            tracker.addValues();
            Board b = new Board();
            b.displayBoard();
        }
        public override void giveValue() // gives values in the selected positions
        {
            if (PlayerNumber % 2 == 0)
            {
                Console.WriteLine("####################");
                Console.Write("Give a value according to player turn: Even Number(2,4,6,8): ");
                while (int.TryParse(Console.ReadLine(), out output) != true || (output % 2 != 0) || !((output <= 9) && (output >= 1)) || arr.Contains(output))
                {
                    Console.WriteLine("XXXXXXXXXXXXXXXXXX");
                    Console.Write("Please give correct value: ");
                }
                NumericValue = output;
                Tracker tracker = new Tracker();
                tracker.addValues();
                PlayerNumber++;
                Board b = new Board();
                b.displayBoard();
            }
            else
            {
                Console.WriteLine("####################");
                Console.Write("Give a value according to player turn: Odd Number(1,3,5,7,9): ");
                while (int.TryParse(Console.ReadLine(), out output) != true || (output % 2 == 0) || !((output <= 9) && (output >= 1)) || arr.Contains(output))
                {
                    Console.WriteLine("XXXXXXXXXXXXXXXXXX");
                    Console.Write("Please give correct value: ");
                }
                NumericValue = output;
                Tracker tracker = new Tracker();
                tracker.addValues();
                PlayerNumber++;
                Board b = new Board();
                b.displayBoard();
            }
        }
    }
    class Player : Game
    {
        char output;
        bool valid = false;
        int[] arr1;
        public Player()
        {
            this.arr1 = Arr();
        }
        public void callChildPlayer1Move()
        {
            player1Move();
        }
        public void callChildPlayer2Move()
        {
            player2Move();
        }
        public void callChildPlayer1MovePvP()
        {
            player1MoveInPvP();
        }
        public void callChildComputerMove()
        {
            computerMove();
        }
        public virtual void player1Move()
        {
            Console.WriteLine();
        }
        public virtual void player2Move()
        {
            Console.WriteLine();
        }
        public virtual void computerMove()
        {
            Console.WriteLine();
        }
        public virtual void player1MoveInPvP()
        {
            Console.WriteLine();
        }
        public override void selectPlayer(char check)
        {
            if (check.Equals('C'))
            {
                GameType = 'C';
                while (WinFlag == 0)
                {
                    if (PlayerNumber % 2 != 0)
                    {
                        Player1 player1 = new Player1();
                        player1.callChildPlayer1Move();
                    }
                    else
                    {
                        ComputerPlayer computerPlayer = new ComputerPlayer();
                        computerPlayer.callChildComputerMove();
                     }
                }
            }
            else
            {
                GameType = 'P';
                while (WinFlag == 0)
                {
                    if (PlayerNumber % 2 == 0)
                    {
                        Player2 player2 = new Player2();
                        player2.callChildPlayer2Move();
                    }
                    else
                    {
                        Player1 player1 = new Player1();
                        player1.callChildPlayer1MovePvP();
                    }
                }
            }
        }
    }
    class Player2 : Player
    {
        public override void player2Move()
        {
            NumericTicTacToe ticTacToe = new NumericTicTacToe();
            WinFlag = ticTacToe.checkWin();
            if (WinFlag == 0)
            {
                Console.WriteLine("############");
                Console.WriteLine("Player 2 turn -> Please select valid position (1 to 9 assuming first box is 1), Or 'Undo2'/'Redo2/Save' to undo redo or save the game.... or 0 to exit application");
                Console.Write("Please Select empty boxes: ");
                Move move = new Move();
                move.makeMove();
            }
            else if (WinFlag == -1)
            {
                Console.WriteLine("**************");
                Console.WriteLine("**Match Draw**");
                Console.WriteLine("**************");
            }
            else
            {
                Game.PlayerNumber--;
                if (PlayerNumber % 2 == 0)
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 2**");
                    Console.WriteLine("******************************");
                }
                else
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 1**");
                    Console.WriteLine("******************************");
                }
            }
        }
    }
    class ComputerPlayer : Player
    {
        public override void computerMove()
        {
            NumericTicTacToe ticTacToe = new NumericTicTacToe();
            WinFlag = ticTacToe.checkWin();
            if (WinFlag == 0)
            {
                Console.WriteLine("Computer turn -> ");
                Move move = new Move();
                move.computerMakeMove();
            }
            else if (WinFlag == -1)
            {
                Console.WriteLine("**************");
                Console.WriteLine("**Match Draw**");
                Console.WriteLine("**************");
            }
            else
            {
                Game.PlayerNumber--;
                if (PlayerNumber % 2 != 0)
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 1**");
                    Console.WriteLine("******************************");
                }
                else
                {
                    Console.WriteLine("*****************************");
                    Console.WriteLine("**Winner is Computer Player**");
                    Console.WriteLine("*****************************");
                }
            }
        }
    }
    class Player1 : Player
    {
        public override void player1Move()
        {
            NumericTicTacToe ticTacToe = new NumericTicTacToe();
            WinFlag = ticTacToe.checkWin();
            if (WinFlag == 0)
            {
                Console.WriteLine("############");
                Console.WriteLine("Player 1 turn -> Please select valid position (1 to 9 assuming first box is 1), Or 'Undo1'/'Redo1/Save' to undo redo or save.... or 0 to exit application");
                Console.Write("Please select input: ");
                Move move = new Move();
                move.makeMove();
            }
            else if (WinFlag == -1)
            {
                Console.WriteLine("**************");
                Console.WriteLine("**Match Draw**");
                Console.WriteLine("**************");
            }
            else
            {
                Game.PlayerNumber--;
                if (PlayerNumber % 2 != 0)
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 1**");
                    Console.WriteLine("******************************");
                }
                else
                {
                    Console.WriteLine("*****************************");
                    Console.WriteLine("**Winner is Computer Player**");
                    Console.WriteLine("*****************************");
                }
            }
        }
        public override void player1MoveInPvP() 
        {
            NumericTicTacToe ticTacToe = new NumericTicTacToe();
            WinFlag = ticTacToe.checkWin();
            if (WinFlag == 0)
            {
                Console.WriteLine("############");
                Console.WriteLine("Player 1 turn -> Please select valid position (1 to 9 assuming first box is 1), Or 'Undo1'/'Redo1/Save' to undo redo or save the game.... or 0 to exit application");
                Console.Write("Please Select empty boxes: ");
                Move move = new Move();
                move.makeMove();
            }
            else if (WinFlag == -1)
            {
                Console.WriteLine("**************");
                Console.WriteLine("**Match Draw**");
                Console.WriteLine("**************");
            }
            else
            {
                Game.PlayerNumber--;
                if (PlayerNumber % 2 == 0)
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 2**");
                    Console.WriteLine("******************************");
                }
                else
                {
                    Console.WriteLine("******************************");
                    Console.WriteLine("**Winner is Player number: 1**");
                    Console.WriteLine("******************************");
                }

            }
        }
    }
    class NumericTicTacToe : Game
    {
        int[] arr;
        public NumericTicTacToe()
        {
            this.arr = Arr();
        }
        public override int checkWin()
        {
            if (((arr[1] + arr[2] + arr[3] == 15) && (arr[1]!=0 && arr[2]!=0 && arr[3]!=0)) || ((arr[4] + arr[5] + arr[6] == 15) && (arr[4] != 0 && arr[5] != 0 && arr[6] != 0)) || ((arr[7] + arr[8] + arr[9] == 15) && (arr[7] != 0 && arr[8] != 0 && arr[9] != 0))) // vertical checks
            {
                return 1;
            }
            else if (((arr[1] + arr[4] + arr[7] == 15) && (arr[1] != 0 && arr[4] != 0 && arr[7] != 0)) || ((arr[2] + arr[5] + arr[8] == 15) && (arr[2] != 0 && arr[5] != 0 && arr[8] != 0)) || ((arr[3] + arr[6] + arr[9] == 15) && (arr[3] != 0 && arr[6] != 0 && arr[9] != 0))) // horizontal checks
            {
                return 1;
            }
            else if (((arr[1] + arr[5] + arr[9] == 15) && (arr[1] != 0 && arr[5] != 0 && arr[9] != 0)) || ((arr[7] + arr[5] + arr[3] == 15) && (arr[7] != 0 && arr[5] != 0 && arr[3] != 0))) // diagonal checks
            {
                return 1;
            }
            else if (arr[1]!=0 && arr[2]!=0 && arr[3]!=0 && arr[4]!=0 && arr[5]!=0 && arr[6]!=0 && arr[7]!=0 && arr[8]!=0 && arr[9] != 0) // draw
            {
                return -1;
            }
            else
                return 0; // game continues 
        }
    }

    class Tracker : Game
    {
        static List<int> undoChosenPositions = new List<int>();// holds position selected by the players
        static List<int> undoChosenValues = new List<int>(); // holds values selected by the players
        static List<int> redoChosenPositions = new List<int>();// holds positions that is undoed
        static List<int> redoChosenValues = new List<int>();// holds values that is redoed 
        int[] arr;
        public Tracker()
        {
            this.arr = Arr();
        }
        public void addPositions()
        {
            undoChosenPositions.Add(Position);
            redoChosenPositions.Clear();
            redoChosenValues.Clear();
        }
        public void addPositionsComputer(int computerPosition)
        {
            undoChosenPositions.Add(computerPosition);
        }
        public void addValues()
        {
            undoChosenValues.Add(NumericValue);
        }
        public override void undoPlayer()
        {
            if (undoChosenPositions.Count <= 1)
            {
                Console.WriteLine("No turns to undo");
            }
            else 
            {
                int lastPosition = undoChosenPositions.Last();
                int lastValue = undoChosenValues.Last();
                if(lastValue%2 == 0)
                {
                    arr[lastPosition] = 0;
                    undoChosenPositions.Remove(undoChosenPositions.Last());
                    undoChosenValues.Remove(undoChosenValues.Last());
                    redoChosenPositions.Add(lastPosition);
                    redoChosenValues.Add(lastValue);
                    int secondLastPosition = undoChosenPositions.Last();
                    int secondLastValue = undoChosenValues.Last();
                    arr[secondLastPosition] = 0;
                    undoChosenPositions.Remove(undoChosenPositions.Last());
                    undoChosenValues.Remove(undoChosenValues.Last());
                    redoChosenPositions.Add(secondLastPosition);
                    redoChosenValues.Add(secondLastValue);
                    PlayerNumber = 1;
                    Board b = new Board();
                    b.displayBoard();
                }
                else
                {
                    arr[lastPosition] = 0;
                    undoChosenPositions.Remove(undoChosenPositions.Last());
                    undoChosenValues.Remove(undoChosenValues.Last());
                    redoChosenPositions.Add(lastPosition);
                    redoChosenValues.Add(lastValue);
                    int secondLastPosition = undoChosenPositions.Last();
                    int secondLastValue = undoChosenValues.Last();
                    arr[secondLastPosition] = 0;
                    undoChosenPositions.Remove(undoChosenPositions.Last());
                    undoChosenValues.Remove(undoChosenValues.Last());
                    redoChosenPositions.Add(secondLastPosition);
                    redoChosenValues.Add(secondLastValue);
                    PlayerNumber = 2;
                    Board b = new Board();
                    b.displayBoard();
                }
            }            
        }

        public override void redoPlayer()
        {
            if (redoChosenPositions.Count <= 1)
            {
                Console.WriteLine("No turns to redo");
            }
            else
            {
                int lastPosition = redoChosenPositions.Last();
                int lastValue = redoChosenValues.Last();
                if (lastValue % 2 == 0)
                {
                    arr[lastPosition] = lastValue;
                    redoChosenPositions.Remove(redoChosenPositions.Last());
                    redoChosenValues.Remove(redoChosenValues.Last());
                    undoChosenPositions.Add(lastPosition);
                    undoChosenValues.Add(lastValue);
                    int secondLastPosition = redoChosenPositions.Last();
                    int secondLastValue = redoChosenValues.Last();
                    arr[secondLastPosition]= secondLastValue;
                    redoChosenPositions.Remove(redoChosenPositions.Last());
                    redoChosenValues.Remove(redoChosenValues.Last());
                    undoChosenPositions.Add(secondLastPosition);
                    undoChosenValues.Add(secondLastValue);
                    PlayerNumber = 2;
                    Board b = new Board();
                    b.displayBoard();
                }
                else 
                {
                    arr[lastPosition] = lastValue;
                    redoChosenPositions.Remove(redoChosenPositions.Last());
                    redoChosenValues.Remove(redoChosenValues.Last());
                    undoChosenPositions.Add(lastPosition);
                    undoChosenValues.Add(lastValue);
                    int secondLastPosition = redoChosenPositions.Last();
                    int secondLastValue = redoChosenValues.Last();
                    arr[secondLastPosition] = secondLastValue;
                    redoChosenPositions.Remove(redoChosenPositions.Last());
                    redoChosenValues.Remove(redoChosenValues.Last());
                    undoChosenPositions.Add(secondLastPosition);
                    undoChosenValues.Add(secondLastValue);
                    PlayerNumber = 1;
                    Board b = new Board();
                    b.displayBoard();
                }
            }
        }
        public override void undoComputer()
        {
            if(undoChosenPositions.Count == 0)
            {
                Console.WriteLine("No turns to undo");
            }
            else
            {
                int lastPosition = undoChosenPositions.Last();
                int lastValue = undoChosenValues.Last();
                arr[lastPosition] = 0;
                redoChosenPositions.Add(lastPosition);
                redoChosenValues.Add(lastValue);
                undoChosenPositions.Remove(undoChosenPositions.Last());
                undoChosenValues.Remove(undoChosenValues.Last());
                lastPosition = undoChosenPositions.Last();
                lastValue = undoChosenValues.Last();
                arr[lastPosition] = 0;
                undoChosenPositions.Remove(undoChosenPositions.Last());
                undoChosenValues.Remove(undoChosenValues.Last());
                redoChosenPositions.Add(lastPosition);
                redoChosenValues.Add(lastValue);
                PlayerNumber = 1;
                Board b = new Board();
                b.displayBoard();
            }
        }
        public override void redoComputer()
        {
            if (redoChosenPositions.Count == 0)
            {
                Console.WriteLine("No turns to redo");
            }
            else
            {
                int lastPosition = redoChosenPositions.Last();
                int lastValue = redoChosenValues.Last();
                arr[lastPosition] = lastValue;
                redoChosenPositions.Remove(redoChosenPositions.Last());
                redoChosenValues.Remove(redoChosenValues.Last());
                undoChosenPositions.Add(lastPosition);
                undoChosenValues.Add(lastValue);
                lastPosition = redoChosenPositions.Last();
                lastValue = redoChosenValues.Last();
                arr[lastPosition] = lastValue;
                redoChosenPositions.Remove(redoChosenPositions.Last());
                redoChosenValues.Remove(redoChosenValues.Last());
                undoChosenPositions.Add(lastPosition);
                undoChosenValues.Add(lastValue);
                PlayerNumber = 1;
                Board b = new Board();
                b.displayBoard();
            }
        }
    }
    class Save: Game
    {
        int[] arr;
        public Save()
        {
            this.arr = Arr();
        }
        
        public override void saveGame()
        {
            bool checkZero = Array.TrueForAll(arr, x => x == 0);
            if(checkZero == true)
            {
                Console.WriteLine("");
                Console.WriteLine("XXXXXXXXXXX");
                Console.WriteLine("Sorry can not save game! No moves were found");
                Console.WriteLine("XXXXXXXXXXX");
            }
            else
            {
                while (SaveExpired == false)
                {
                    DateTime currentTime = DateTime.Now;
                    SaveExpired = true;
                    Time = currentTime.ToString();
                    string[] substrings = Time.Split(' ');
                    Time = substrings[1];
                    string pattern = "[^a-zA-Z0-9]";
                    Time = Regex.Replace(Time, pattern,"_") +"_" + GameType + ".txt"; // taking time and removing special characters and using it a name of the saved file
                }
                string currentDirectory = Directory.GetCurrentDirectory();
                string folderPath = Path.Combine(currentDirectory, "Saved_files");
                string FILENAME = Path.Combine(folderPath, Time);
                FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);
                foreach (int element in arr)
                {
                    writer.WriteLine(element);
                }
                writer.Close();
                outFile.Close();
                Console.WriteLine("");
                Console.WriteLine("Saved Succesfully!!");
                Console.WriteLine("");
            }
            Board board = new Board(); 
            board.displayBoard();
        }    
        public override void loadGame()
        {
            string[] files;
            string currentDirectory = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(currentDirectory, "Saved_files");
            files = Directory.GetFiles(folderPath);
            if (files.Length == 0)
            {
                Console.WriteLine(folderPath);
                Console.WriteLine("There are no files in Saved Files");
                Console.WriteLine("");
                Console.WriteLine("Please Restart the Application");
                Console.WriteLine("");
            }
            
            else
            {
                Console.WriteLine("Save contains the following files");
                foreach (string file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
                bool exists = false;
                while (exists == false)
                { 
                    Console.WriteLine("");
                    Console.Write("Copy and Paste the name of the file you want to reload (Including Extension)(The character is insensitive): ");
                    string fileName = Console.ReadLine();
                    string filePathCombine = Path.Combine(folderPath, fileName);
                    if (File.Exists(filePathCombine))
                    {
                        string[] substrings = fileName.Split('_');
                        string result = substrings[3];
                        substrings = result.Split('.');
                        char getGameType = Convert.ToChar(substrings[0]);
                        getGameType = char.ToUpper(getGameType);
                        FileStream inFile = new FileStream(filePathCombine, FileMode.Open, FileAccess.Read);
                        StreamReader reader = new StreamReader(inFile);
                        string line;
                        int value;
                        List<int> lines = new List<int>();
                        while ((line = reader.ReadLine()) != null)
                        {
                            value = Convert.ToInt32(line);
                            lines.Add(value);
                        }
                        reader.Close();
                        inFile.Close();
                        int[] newArray = lines.ToArray();
                        Array.Copy(newArray, arr, newArray.Length);
                        exists = true;
                        Console.WriteLine("");
                        Console.WriteLine("Succesfully Loaded Game!");
                        Console.WriteLine("");
                        Board board = new Board();
                        board.displayBoard();
                        if (getGameType == 'C')
                        {                            
                            Player player = new Player();
                            player.selectPlayer(getGameType);
                        }
                        else
                        {
                            int count = 0;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (arr[i] != 0)
                                {
                                    count++;
                                }
                            }
                            if(count % 2 == 0)
                            {
                                PlayerNumber = 1;
                                Player player = new Player();
                                player.selectPlayer(getGameType);
                            }
                            else
                            {
                                PlayerNumber = 2;
                                Player player = new Player();
                                player.selectPlayer(getGameType);
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Sorry No File Found. Try Again");
                        Console.WriteLine("");
                        exists = false;
                    }
                }
            }
        }
    }
    class ShowHelp: Game
    {
        public override void viewHelp()
        {
            Console.WriteLine("");
            Console.WriteLine("To begin the game select 1 or to load a previous game select 3");
            Console.WriteLine("The game board looks like follwing");
            Console.WriteLine("  ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("   1 |  2  |  3   ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("   4 |  5  |  6   ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("   7 |  8  |  9   ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  ");
            Console.WriteLine("The number on each box corresponds to the position user want to place the Numeric Value");
            Console.WriteLine("Player 1 always gets the first chance to put a value on the board and it is always Odd.");
            Console.WriteLine("Player 2 or computer player always puts on Even values on the board");
            Console.WriteLine("The first player to complete a total of 15 horizontally, diagonally or vertically wins the game");
            Console.WriteLine("Player can undo and redo their previous moves.");
            Console.WriteLine("Suppose first move is made by player 1 and 2nd move by player 2 so if player 1 wants to undo his first move it will aumatically undo player 2's 2nd move");
            Console.WriteLine("To ensure fair gameplay");
            Console.WriteLine("Players can use 'Save' keyword to save the game");
            Console.WriteLine("The name of the save files are given by the time when they were saved. To load a game paste the exact name including extrension");
        }
    }
    class Program
    {
        
        private static void Main(string[] args)
        {
            bool keepAsking = true;
            while(keepAsking == true)
            {
                Console.WriteLine("*********************************************");
                Console.WriteLine("*****Welcome to Numeric Tic-Tac-Toe Game*****");
                Console.WriteLine("*********************************************");
                Console.WriteLine("");
                Console.WriteLine("Press 1 for Game");
                Console.WriteLine("Press 2 for Help");
                Console.WriteLine("Press 3 for Loading Saved Game");
                Console.WriteLine("Press anyother number to exit the game");
                int choice = 0;
                while (int.TryParse(Console.ReadLine(), out choice) != true)
                {
                    Console.WriteLine("Please Select one from the above");
                }
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please select player type. Player vs Computer or Player vs Player");
                        Console.WriteLine("         ");
                        Console.Write("Press 'C' for Player vs Computer or Press 'P' for Player vs Player: ");
                        char output;
                        while (char.TryParse(Console.ReadLine(), out output) != true || (!output.Equals('C') && !output.Equals('P')))
                        {
                            Console.WriteLine("XXXXXXXXXXXXXXXX");
                            Console.Write("Please press correct input: ");
                        }
                        Player player = new Player();
                        player.selectPlayer(output);
                        keepAsking = false;
                        break;
                    case 2:
                        ShowHelp help = new ShowHelp();
                        help.viewHelp();
                        keepAsking = true;
                        break;
                    case 3:
                        Save save = new Save();
                        save.loadGame();
                        keepAsking = false;
                        break;
                    default:
                        keepAsking = false;
                        break;
                }
            }
        }
    }
}