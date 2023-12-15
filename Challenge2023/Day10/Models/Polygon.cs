namespace Challenge2023.Day10.Models
{
    internal class Polygon(List<Point> vertices)
    {
        private readonly List<Point> vertices = vertices;

        //implementation of Point-In-Polygon algorithm 
        public bool IsInside(Point p)
        {
            bool inside = false;

            for (int i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
            {
                if (((vertices[i].Y > p.Y) != (vertices[j].Y > p.Y)) &&
                    (p.X < (vertices[j].X - vertices[i].X) * (p.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) + vertices[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
