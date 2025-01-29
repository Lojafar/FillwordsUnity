namespace FillWords.Gameplay.Game.Root.Cell
{
    public class CellData
    {
        public readonly char Letter;
        public readonly CellPos Position;
        public CellData(char letter, CellPos pos)
        {
            Letter = letter;
            Position = pos;
        }
        public CellData(char letter, byte posX, byte posY)
        {
            Letter = letter;
            Position = new CellPos(posX, posY);
        }
    }
}
