namespace FillWords.Gameplay.Game.Root.Cell
{
    public struct CellPos 
    {
        public readonly byte X;
        public readonly byte Y;
        public CellPos(byte posX, byte posY)
        {
            X = posX;
            Y = posY;
        }
        public override bool Equals(object obj)
        {
            if (obj is CellPos otherCellPos)
            {
                return X == otherCellPos.X && Y == otherCellPos.Y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ((X * 4 + Y * 31)).GetHashCode();
        }
        public static bool operator == (CellPos pos1, CellPos pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }
        public static bool operator !=(CellPos pos1, CellPos pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }

    }
}
