using Challenge2023.Day23.Enums;

namespace Challenge2023.Day23.Models
{
    internal record Entry
    {
        public int Row { get; init; }
        public int Column { get; init; }
    }

    internal class PathFinder : IEquatable<PathFinder>
    {
        public Guid Id { get; } = Guid.NewGuid();

        public List<Entry> Journal { get; private set; } = [];

        public HashSet<PathFinder> Ancestors { get; } = [];

        public PathFinderStatus Status { get; set; } = PathFinderStatus.Hiking;

        public int Row { get; set; }

        public int Column { get; set; }

        public Moves NextMove { get; set; }

        public Moves ArrivedFrom
        {
            get
            {
                return NextMove switch
                {
                    Moves.U => Moves.D,
                    Moves.D => Moves.U,
                    Moves.L => Moves.R,
                    Moves.R => Moves.L,
                    _ => Moves.N
                };
            }
        }

        //public void RecordEntry()
        //{
        //    Journal.Add(new Entry { Row = Row, Column = Column });
        //}

        //public void RecordAncestoralJournalHistory(PathFinder ancestor)
        //{
        //    foreach (var entry in ancestor.Journal)
        //    {
        //        Journal.Add(entry);
        //    }
        //}

        public bool Equals(PathFinder? other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as PathFinder);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
