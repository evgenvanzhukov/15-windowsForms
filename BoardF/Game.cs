using System;


namespace BoardF
{
    public class Game
    {
        int size;

        Map map;
        Coord space;

        public int Moves { get; private set; }

        public Game(int _size)
        {
            size = _size;
            map = new Map(size);
        }

        public void Start (int seed = 0)
        {
            int digit = 0;

            foreach (Coord xy in new Coord().YieldCoord(size))
            {
                map.Set(xy, ++digit);
            }

            space = new Coord(size);

            if (seed > 0) Shuffle(seed);
            Moves = 0;
        }

        void Shuffle (int seed)
        {
            Random random = new Random(seed);
            for (int j = 0; j < seed; j++)
                PressAt(random.Next(size), random.Next(size));
        }

        public int PressAt (int x,int y)
        {
            return PressAt(new Coord (x,y));
        }

        int PressAt(Coord xy)
        {
            if (space.Equals(xy)) return 0;

            if (xy.x != space.x && xy.y != space.y) return 0;

            int steps = Math.Abs(xy.x - space.x) + 
                        Math.Abs(xy.y - space.y);

            while (xy.x!=space.x)
                Shift(Math.Sign(xy.x - space.x), 0);

            while (xy.y != space.y)
                Shift(0,Math.Sign(xy.y - space.y));

            Moves += steps;
            return steps;
        }

        void Shift (int sx,int sy)
        {
            Coord next = space.Add(sx, sy);
            map.Copy(next,space);
            space = next;
            map.Set(space, 0);
        }

        public int GetDigitAt(int x,int y)
        {
            return GetDigitAt(new Coord(x,y));
        }

        int GetDigitAt(Coord xy)
        {
            if (space.Equals(xy)) return 0;
            return map.Get(xy);
        }

        public bool Solved()
        {
            if (!space.Equals(new Coord(size))) return false;
            int digit = 0;

            foreach (Coord xy in new Coord().YieldCoord(size))
                if (map.Get(xy) != ++digit) return space.Equals(xy);

            return true;
        }
    }
}
