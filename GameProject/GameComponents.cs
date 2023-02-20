using System;

namespace GameProject
{
  internal class GameComponents
  {
    public static bool Path(char c) => c != '?';

    public static int Movement(Position player, string[] map, int steps)
    {
      string str = Console.ReadKey(true).Key.ToString();
      Console.SetCursorPosition(player.x, player.y + 2);
      Console.Write(' ');
      if (str[0] == 'W' && player.y - 1 >= 0 && GameComponents.Path(map[player.y - 1][player.x]))
      {
        --player.y;
        ++steps;
      }
      else if (str[0] == 'A' && player.x - 1 >= 0 && GameComponents.Path(map[player.y][player.x - 1]))
      {
        --player.x;
        ++steps;
      }
      else if (str[0] == 'S' && player.y + 1 < map.Length && GameComponents.Path(map[player.y + 1][player.x]))
      {
        ++player.y;
        ++steps;
      }
      else if (str[0] == 'D' && str.Length == 1 && player.x + 1 < map[player.y].Length && GameComponents.Path(map[player.y][player.x + 1]))
      {
        ++player.x;
        ++steps;
      }
      return steps;
    }

    public static void PrintMaze(string[] maze)
    {
      for (int index1 = 0; index1 < maze.Length; ++index1)
      {
        for (int index2 = 0; index2 < maze[0].Length; ++index2)
        {
          if (maze[index1][index2] != 's' && maze[index1][index2] != 'e')
            Console.Write('?');
          else if (maze[index1][index2] == 's')
            Console.Write('s');
          else
            Console.Write('e');
        }
        Console.WriteLine();
      }
    }
  }
}
