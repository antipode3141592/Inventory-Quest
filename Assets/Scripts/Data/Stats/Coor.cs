using System;

namespace Data
{
    public class Coor: IEquatable<Coor>
    {
        public int row;
        public int column;

        public Coor(int r, int c)
        {
            row = r;
            column = c;
        }

        public bool Equals(Coor other)
        {
            if (row == other.row && column == other.column) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Coor p = obj as Coor;
            if ((Object)p == null) return false;

            return (row == p.row && column == p.column);
        }

        public override int GetHashCode()
        {
            return row ^ column;
        }

        public override string ToString()
        {
            return $"{row}, {column}";
        }

        public static bool operator ==(Coor c1, Coor c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Coor c1, Coor c2)
        {
            return !c1.Equals(c2);
        }
    }
}
