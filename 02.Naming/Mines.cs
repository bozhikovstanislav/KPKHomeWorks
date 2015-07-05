namespace mini4ki
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class MinesGame
    {
        public class PlayerPoints
        {
            string name;
            int points;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public int Points
            {
                get { return points; }
                set { points = value; }
            }

            public PlayerPoints() { }

            public PlayerPoints(string name, int points)
            {
                this.name = name;
                this.points = points;
            }
        }

        static void Main(string[] arguments)
        {
            string command = string.Empty;
            char[,] field = createGameField();
            char[,] mines = ArangeBombs();
            int counter = 0;
            bool enterAMine = false;
            List<PlayerPoints> champions = new List<PlayerPoints>(6);
            int row = 0;
            int col = 0;
            bool startOfGame = true;
            const int MAX_POINTS = 35;
            bool isPlayerWins = false;

            do
            {
                if (startOfGame)
                {
                    Console.WriteLine("Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki." +
                    " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    CreatBoard(field);
                    startOfGame = false;
                }
                Console.Write("Daj red i kolona : ");
                command = Console.ReadLine().Trim();
                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out row) &&
                    int.TryParse(command[2].ToString(), out col) &&
                        row <= field.GetLength(0) && col <= field.GetLength(1))
                    {
                        command = "turn";
                    }
                }
                switch (command)
                {
                    case "top":
                        CalculatPointsOfPlayer(champions);
                        break;
                    case "restart":
                        field = createGameField();
                        mines = ArangeBombs();
                        CreatBoard(field);
                        enterAMine = false;
                        startOfGame = false;
                        break;
                    case "exit":
                        Console.WriteLine("4a0, 4a0, 4a0!");
                        break;
                    case "turn":
                        if (mines[row, col] != '*')
                        {
                            if (mines[row, col] == '-')
                            {
                                NextMove(field, mines, row, col);
                                counter++;
                            }
                            if (MAX_POINTS == counter)
                            {
                                isPlayerWins = true;
                            }
                            else
                            {
                                CreatBoard(field);
                            }
                        }
                        else
                        {
                            enterAMine = true;
                        }
                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }
                if (enterAMine)
                {
                    CreatBoard(mines);
                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " +
                        "Daj si niknejm: ", counter);
                    string niknejm = Console.ReadLine();
                    PlayerPoints pointsOfPlayer = new PlayerPoints(niknejm, counter);
                    if (champions.Count < 5)
                    {
                        champions.Add(pointsOfPlayer);
                    }
                    else
                    {
                        for (int i = 0; i < champions.Count; i++)
                        {
                            if (champions[i].Points < pointsOfPlayer.Points)
                            {
                                champions.Insert(i, pointsOfPlayer);
                                champions.RemoveAt(champions.Count - 1);
                                break;
                            }
                        }
                    }
                    champions.Sort(

                        (PlayerPoints firstPlayer, PlayerPoints secondPlayer) => secondPlayer.Name.CompareTo(firstPlayer.Name)
                        
                        );
                    champions.Sort(

                        (PlayerPoints firstPlayer, PlayerPoints secondPlayer) => secondPlayer.Points.CompareTo(firstPlayer.Points)

                        );
                    CalculatPointsOfPlayer(champions);

                    field = createGameField();
                    mines = ArangeBombs();
                    counter = 0;
                    enterAMine = false;
                    startOfGame = true;
                }
                if (isPlayerWins)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    CreatBoard(mines);
                    Console.WriteLine("Daj si imeto, batka: ");
                    string imeee = Console.ReadLine();
                    PlayerPoints points = new PlayerPoints(imeee, counter);
                    champions.Add(points);
                    CalculatPointsOfPlayer(champions);
                    field = createGameField();
                    mines = ArangeBombs();
                    counter = 0;
                    isPlayerWins = false;
                    startOfGame = true;
                }
            }
            while (command != "exit");
            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
        }

        private static void CalculatPointsOfPlayer(List<PlayerPoints> listofPoints)
        {
            Console.WriteLine("\nTo4KI:");
            if (listofPoints.Count > 0)
            {
                for (int i = 0; i < listofPoints.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii",
                        i + 1, listofPoints[i].Name, listofPoints[i].Points);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("prazna klasaciq!\n");
            }
        }

        private static void NextMove(char[,] field,
            char[,] bombs, int row, int col)
        {
            char kolkoBombi = CalculatesBombsCoordinats(bombs, row, col);
            bombs[row, col] = kolkoBombi;
            field[row, col] = kolkoBombi;
        }

        private static void CreatBoard(char[,] board)
        {
            int boardCol = board.GetLength(0);
            int boardRow = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < boardCol; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < boardRow; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine("   ---------------------\n");
        }
     
        private static char[,] createGameField()
        {
            int boardRows = 5;
            int boardColumns = 10;
            
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] ArangeBombs()
        {
            int rows = 5;
            int cols = 10;
            char[,] gamePlayerField = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gamePlayerField[i, j] = '-';
                }
            }

            List<int> randomNumbers = new List<int>();
            while (randomNumbers.Count < 15)
            {
                Random random = new Random();
                int nextNumber = random.Next(50);
                if (!randomNumbers.Contains(nextNumber))
                {
                    randomNumbers.Add(nextNumber);
                }
            }

            foreach (int item in randomNumbers)
            {
                int col = (item / cols);
                int row = (item % cols);
                if (row == 0 && item != 0)
                {
                    col--;
                    row = cols;
                }
                else
                {
                    row++;
                }
                gamePlayerField[col, row - 1] = '*';
            }

            return gamePlayerField;
        }

        private static void smetki(char[,] field)
        {
            int col = field.GetLength(0);
            int row = field.GetLength(1);

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (field[i, j] != '*')
                    {
                        char bombsNumbers = CalculatesBombsCoordinats(field, i, j);
                        field[i, j] = bombsNumbers;
                    }
                }
            }
        }

        private static char CalculatesBombsCoordinats(char[,] field, int rowIndex, int colIndex)
        {
            int countOfBombs = 0;
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            if (rowIndex - 1 >= 0)
            {
                if (field[rowIndex - 1, colIndex] == '*')
                {
                    countOfBombs++;
                }
            }
            if (rowIndex + 1 < rows)
            {
                if (field[rowIndex + 1, colIndex] == '*')
                {
                    countOfBombs++;
                }
            }
            if (colIndex - 1 >= 0)
            {
                if (field[rowIndex, colIndex - 1] == '*')
                {
                    countOfBombs++;
                }
            }
            if (colIndex + 1 < cols)
            {
                if (field[rowIndex, colIndex + 1] == '*')
                {
                    countOfBombs++;
                }
            }
            if ((rowIndex - 1 >= 0) && (colIndex - 1 >= 0))
            {
                if (field[rowIndex - 1, colIndex - 1] == '*')
                {
                    countOfBombs++;
                }
            }
            if ((rowIndex - 1 >= 0) && (colIndex + 1 < cols))
            {
                if (field[rowIndex - 1, colIndex + 1] == '*')
                {
                    countOfBombs++;
                }
            }
            if ((rowIndex + 1 < rows) && (colIndex - 1 >= 0))
            {
                if (field[rowIndex + 1, colIndex - 1] == '*')
                {
                    countOfBombs++;
                }
            }
            if ((rowIndex + 1 < rows) && (colIndex + 1 < cols))
            {
                if (field[rowIndex + 1, colIndex + 1] == '*')
                {
                    countOfBombs++;
                }
            }
            return char.Parse(countOfBombs.ToString());
        }
    }
}