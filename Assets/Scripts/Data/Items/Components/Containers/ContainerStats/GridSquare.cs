namespace Data.Items

{
    public class GridSquare
    {
        public string storedItemId;
        public bool IsOccupied;

        public GridSquare(string storedItemId = null, bool isOccupied = false)
        {
            this.storedItemId = storedItemId;
            IsOccupied = isOccupied;
        }
    }

}
