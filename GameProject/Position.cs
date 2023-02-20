namespace GameProject
{
  internal class Position
  {
    public int x;
    public int y;

    public Position(int setX, int setY)
    {
      this.x = setX;
      this.y = setY;
    }

    public bool Equals(Position other) => this.x == other.x && this.y == other.y;
  }
}
