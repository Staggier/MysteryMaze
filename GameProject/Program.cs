using System;
using System.Diagnostics;
using System.Threading;

namespace GameProject
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      MainMenu();
      Console.CursorVisible = false;
      Console.SetWindowSize(100, 34);
      int steps = 0;
      Stopwatch timer = new Stopwatch();
      Console.Clear();
      Console.Write("Would you like to skip the tutorial? (Type 'Yes' to skip)\n> ");
      if (Console.ReadLine() != "Yes")
      {
        Console.Clear();
        Console.SetCursorPosition(0, 2);
        Program.MainGame(Mazes.MazeZero(), timer, steps);
      }
      Func<string[]>[] funcArray = new Func<string[]>[4]
      {
        new Func<string[]>(Mazes.MazeOne),
        new Func<string[]>(Mazes.MazeTwo),
        new Func<string[]>(Mazes.MazeThree),
        new Func<string[]>(Mazes.MazeFour)
      };
      foreach (Func<string[]> func in funcArray)
      {
        Console.Clear();
        timer.Start();
        Console.ForegroundColor = ConsoleColor.White;
        Program.UpdateTime(timer, steps);
        Console.SetCursorPosition(0, 2);
        steps = Program.MainGame(func(), timer, steps);
      }
      timer.Stop();
      Program.Highscore(timer, steps);
      Program.Credits();
    }

    public static void UpdateTime(Stopwatch timer, int steps)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.SetCursorPosition(0, 0);
      TimeSpan elapsed = timer.Elapsed;
      Console.Write("Time: " + string.Format("{0:00}:{1:00}", (object) elapsed.Minutes, (object) elapsed.Seconds) + "   Steps: " + (object) steps);
    }

    public static int MainGame(string[] maze, Stopwatch timer, int steps)
    {
      Position player = new Position(0, 0);
      Position position = new Position(0, 0);
      Position other = new Position(0, 0);
      Console.CursorVisible = false;
      GameComponents.PrintMaze(maze);
      for (int index1 = 0; index1 < maze.Length; ++index1)
      {
        for (int index2 = 0; index2 < maze[0].Length; ++index2)
        {
          if (maze[index1][index2] == 's')
          {
            position = new Position(index2, index1);
            player = new Position(index2, index1);
            Console.SetCursorPosition(index2, index1);
            break;
          }
        }
      }
      for (int setY = 0; setY < maze.Length; ++setY)
      {
        for (int index = 0; index < maze[0].Length; ++index)
        {
          if (maze[setY][index] == 'e')
          {
            other = new Position(index, setY);
            break;
          }
        }
      }
      do
      {
        Program.UpdateTime(timer, steps);
        steps = GameComponents.Movement(player, maze, steps);
        Console.SetCursorPosition(player.x, player.y + 2);
        Program.PrintPlayer(player);
      }
      while (!player.Equals(other));
      return steps;
    }

    public static void PrintPlayer(Position player) => Console.Write('@');

    public static void MainMenu()
    {
      string[] strArray1 = new string[21]
      {
        "   ____        ____  __        __   ______     _________     _______      ______     __        __                    \n",
        "  |? ? \\      / ? ?| \\?\\      /?/  /? ___?\\   |? ? ? ? ?|   |? ?__? \\    |? ?_ ?\\    \\?\\      /?/                      \n",
        "  |? ? ?\\    /? ? ?|  \\?\\    /?/  / ?/   \\?\\  |___ ? ___|   |? |  \\__|   |? | \\? |    \\?\\    /?/                  \n",
        "  |? ? ? \\  / ? ? ?|   \\?\\  /?/   |?|    /_/      |?|       |? |         |? |_/ ?|     \\?\\  /?/             \n",
        "  |? |\\ ? \\/ ? /| ?|    \\?\\/?/    | ?\\__          |?|       |? ?\\__/|    |? ? ? /       \\?\\/?/                \n",
        "  |? | \\? ? ? / | ?|     \\? /      \\__ ?\\_        |?|       |? ? __?|    |? ? ?\\         \\? /            \n",
        "  |? |  \\ ? ?/  | ?|     /?/          \\? ?\\       |?|       |? ?/  \\|    |? |\\? \\        /?/               \n",
        "  |? |   \\__/   | ?|    /?/       ___  \\? ?|      |?|       |? |   ___   |? | \\? \\      /?/               \n",
        "  |? |          | ?|   /?/        \\ ?\\_/? ?/      |?|       |? |__/? ?|  |? |  \\? \\    /?/               \n",
        "  |__|          |__|  /_/          \\______/       |_|       |________/   |__|   \\__\\  /_/                     \n",
        "\n",
        "                ____        ____        ____       ____________     _______         \n",
        "               |? ? \\      / ? ?|      /? ? \\     |? ? ? ? ? ?/    |? ?__? \\   \n",
        "               |? ? ?\\    /? ? ?|     /? _ ? \\    |_____  ? ?/     |? |  \\__|       \n",
        "               |? ? ? \\  / ? ? ?|    /? / \\? ?\\         /? ?/      |? |             \n",
        "               |? |\\?  \\/ ? /|? |   |? ?|_|? ? |       /? ?/       |? ?\\__/|        \n",
        "               |? | \\? ? ? / |? |   |? ? __ ? ?|      /? ?/        |? ? __?|          \n",
        "               |? |  \\ ? ?/  |? |   |? ?/  \\? ?|     /? ?/         |? ?/  \\|        \n",
        "               |? |   \\__/   |? |   |? |    |? |    /? ?/_____     |? |   ___         \n",
        "               |? |          |? |   |? |    |? |   /? ? ? ? ? |    |? |__/? ?|         \n",
        "               |__|          |__|   |__|    |__|  /___________|    |________/          \n"
      };
      foreach (string str in strArray1)
      {
        Console.Write(str);
        Thread.Sleep(30);
      }
      string[] strArray2 = new string[7]
      {
        "\n\n                                 Welcome to Mystery Maze!\n\n",
        "    The 'tutorial maze' which will help you get prepared for what's in store for this game.\n",
        "           Complete the mazes as fast as possible to get the highest score possible.\n",
        "        The mazes consist of '?' that will become uncovered as you walk along the path.\n",
        "\n                     Note: The tutorial maze will not add onto your time.\n",
        "                 Use 'WASD' to move, each maze will start at 's' and end at 'e'.\n",
        "\n                                 Press enter to continue!"
      };
      foreach (string str in strArray2)
      {
        foreach (char ch in str)
        {
          Console.Write(ch);
          Thread.Sleep(30);
        }
      }
      Console.ReadLine();
    }

    public static void Highscore(Stopwatch timer, int steps)
    {
      Console.Clear();
      if (timer.ElapsedMilliseconds / 1000L < 5L)
      {
        foreach (char ch in "\n\n                                            Surprise!")
        {
          Console.Write(ch);
          Thread.Sleep(30);
        }
        Thread.Sleep(10000);
      }
      else
      {
        Console.WriteLine("Enter your name!");
        string str1 = Console.ReadLine();
        Console.Clear();
        int num = steps * 1000 / (Convert.ToInt32(timer.ElapsedMilliseconds) / 1000);
        string[] strArray = new string[6]
        {
          "                                           Highscores                                              ",
          "                                         Parity: 4260                                             ",
          "                                         Jordan: 4140                                             ",
          "                                           Ben: 3870                                              ",
          "                                          " + str1 + ": " + (object) num + "                          ",
          "     Note: try entering the complete string of the maze from maze 1 to 4 for a surprise ;)         "
        };
        foreach (string str2 in strArray)
        {
          Console.WriteLine(str2);
          Thread.Sleep(30);
        }
        Thread.Sleep(10000);
      }
    }

    public static void Credits()
    {
      int num = 1;
      Console.Clear();
      Console.ForegroundColor = ConsoleColor.White;
      string[] strArray = new string[10]
      {
        "\n\n                                      The Development Team                                          ",
        "                                        Jordan McIntyre                                            ",
        "                                 Producer and Project Director                                     ",
        "                                        Jordan McIntyre                                            ",
        "                             Lead Programmer and Assistant Director                                ",
        "                                        Jordan McIntyre                                            ",
        "                                         Lead Designer                                             ",
        "                                        Jordan McIntyre                                            ",
        "                                     Thank you for playing!                                        ",
        "                                                                                                   "
      };
      foreach (string str in strArray)
      {
        foreach (char ch in str)
        {
          Console.Write(ch);
          Thread.Sleep(15);
          ++num;
          if (num == str.Length)
            Console.WriteLine("\n");
        }
        num = 1;
        Thread.Sleep(20);
      }
    }
  }
}
