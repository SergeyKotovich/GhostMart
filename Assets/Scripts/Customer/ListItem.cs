using UnityEngine;

namespace Customer
{
    public class ListItem
    {
        public Vector3 Position { get; }
        public Stand StopPoint { get; }
        public int MaxCount { get; }
        public int CurrentCount { get; set; }

        public ListItem(Vector3 position, Stand stopPoint, int count)
        {
            Position = position;
            StopPoint = stopPoint;
            MaxCount = count;
        }
    }
}