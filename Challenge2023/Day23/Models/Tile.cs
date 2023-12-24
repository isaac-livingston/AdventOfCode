using Challenge2023.Day23.Enums;

namespace Challenge2023.Day23.Models
{
    internal class Tile(int row, int column, char c, char? up = null, char? down = null, char? left = null, char? right = null) : IEquatable<Tile>
    {
        public int Row { get; } = row;

        public int Column { get; } = column;

        public bool StartTile { get; } = SetStartingPoint(c, up, down, left, right);

        public bool EndTile { get; } = SetEndingPoint(c, up, down, left, right);

        public Moves PossibleMoves { get; } = SetPossibleMoves(c, up, down, left, right);

        private static bool SetStartingPoint(char c, char? up, char? down, char? left, char? right)
        {
            return c == '.' && up == null && down != '#' && left == '#' && right == '#';
        }

        private static bool SetEndingPoint(char c, char? up, char? down, char? left, char? right)
        {
            return c == '.' && up != '#' && down == null && left == '#' && right == '#';
        }

        private static Moves SetPossibleMoves(char c, char? up, char? down, char? left, char? right)
        {
            if (c == '#')
            {
                return Moves.N;
            }
            else if (c == '^')
            {
                return Moves.U;
            }
            else if (c == 'v')
            {
                return Moves.D;
            }
            else if (c == '<')
            {
                return Moves.L;
            }
            else if (c == '>')
            {
                return Moves.R;
            }

            Moves? moves = null;

            if (left != null && left != '#' && left != '>')
            {
                moves ??= Moves.L;
            }

            if (right != null && right != '#' && right != '<')
            {
                if (moves == null)
                {
                    moves = Moves.R;
                }
                else
                {
                    moves |= Moves.R;
                }
            }

            if (up != null && up != '#' && up != 'v')
            {
                if (moves == null)
                {
                    moves = Moves.U;
                }
                else
                {
                    moves |= Moves.U;
                }
            }

            if (down != null && down != '#' && down != '^')
            {
                if (moves == null)
                {
                    moves = Moves.D;
                }
                else
                {
                    moves |= Moves.D;
                }
            }

            return moves.GetValueOrDefault();
        }

        public readonly HashSet<PathFinder> PathFinders = [];

        public bool PathFinderAllowed(PathFinder pathFinder)
        {
            if (VisitedByPathFinder(pathFinder))
            {
                return false;
            }

            if (PossibleMoves == Moves.N)
            {
                return false;
            }

            if (PossibleMoves == Moves.D && pathFinder.ArrivedFrom == Moves.D && !StartTile)
            {
                return false;
            }

            if (PossibleMoves == Moves.U && pathFinder.ArrivedFrom == Moves.U && !EndTile)
            {
                return false;
            }

            if (PossibleMoves == Moves.L && pathFinder.ArrivedFrom == Moves.L)
            {
                return false;
            }

            if (PossibleMoves == Moves.R && pathFinder.ArrivedFrom == Moves.R)
            {
                return false;
            }

            return true;

            //return PossibleMoves != Moves.N && !VisitedByPathFinder(pathFinder);
        }

        public void BackfillPathFinder(PathFinder pathFinder)
        {
            PathFinders.Add(pathFinder);
        }

        public bool VisitedByPathFinder(PathFinder pathFinder)
        {
            return PathFinders.Contains(pathFinder);
        }

        public List<Moves> EnterTile(PathFinder pathFinder, Moves arrivedFrom)
        {
            PathFinders.Add(pathFinder);

            var moves = new List<Moves>();

            var pathFinderMoves = PossibleMoves ^ arrivedFrom;

            if (pathFinderMoves.HasFlag(Moves.U))
            {
                moves.Add(Moves.U);
            }

            if (pathFinderMoves.HasFlag(Moves.D))
            {
                moves.Add(Moves.D);
            }

            if (pathFinderMoves.HasFlag(Moves.L))
            {
                moves.Add(Moves.L);
            }

            if (pathFinderMoves.HasFlag(Moves.R))
            {
                moves.Add(Moves.R);
            }

            return moves.Count == 0 ? [Moves.N] : moves;
        }

        public override string ToString()
        {
            return $"[{Row},{Column}]";
        }

        public bool Equals(Tile? other)
        {
            if (other == null)
            {
                return false;
            }

            return Row == other.Row && Column == other.Column;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as Tile);
        }

        public static bool operator ==(Tile? t1, Tile? t2)
        {
            if (t1 is null)
            {
                return t2 is null;
            }

            return t1.Equals(t2);
        }

        public static bool operator !=(Tile? t1, Tile? t2)
        {
            return !(t1 == t2);
        }
    }
}
