using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segments
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public Tuple<Segment, Segment> GetRes(List<Segment> segments)
        {
            var queue = new Queue<Point>();
            var tree = new SortedSet<int>();
            Tuple<Segment, Segment> res = null;
            foreach (var e in segments.SelectMany(x => new[] { x.start, x.end }).OrderBy(a => a.x))
                queue.Enqueue(e);
            while (queue.Count != 0)
            {
                var curPoint = queue.Dequeue();
                if (curPoint.IsStart)
                {
                    // res = checkNeighbors();
                    tree.Add(curPoint.y);
                }
                else
                {
                    //res = checkNeighbors();
                    tree.Remove(curPoint.y);
                }

            }
            return res;
        }
    }

    class Point
    {
        public int x;
        public int y;
        Segment segment;
        public bool IsStart => segment.start == this;
        public bool IsEnd => segment.end == this;
    }

    class Segment
    {
        public Point start, end;
    }
}



