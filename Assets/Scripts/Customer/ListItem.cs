using Interfaces;
using UnityEngine;

namespace Customer
{
    public class ListItem
    {
        public Vector3 Position { get; }
        public IInteractable StopPoint { get; }
        public int MaxCount { get; }
        public int CurrentCount { get; private set; }

        public ListItem(Vector3 position, IInteractable stopPoint, int count)
        {
            Position = position;
            StopPoint = stopPoint;
            MaxCount = count;
        }

        public void OnGotProduct()
        {
            CurrentCount++;
        }
    }
}