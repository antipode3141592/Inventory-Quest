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

        public override bool Equals(object obj) => this.Equals(obj as Coor);

        public bool Equals(Coor p)
        {
            if (p is null)
            {
                return false;
            }
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }
            if (this.GetType() != p.GetType())
            {
                return false;
            }
            return (row == p.row) && (column == p.column);
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
            if (c1 is null)
            {
                if (c2 is null)
                {
                    return true;
                }
                return false;
            }
            return c1.Equals(c2);
        }

        public static bool operator !=(Coor c1, Coor c2)
        {
            return !(c1 == c2);
        }
    }
}
