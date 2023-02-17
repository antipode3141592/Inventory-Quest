namespace Data.Items

{
    public class GridSquare
    {
        public string storedItemGuId;
        public bool IsOccupied;

        public GridSquare(string storedItemGuId = null, bool isOccupied = false)
        {
            this.storedItemGuId = storedItemGuId;
            IsOccupied = isOccupied;
        }
    }

}
