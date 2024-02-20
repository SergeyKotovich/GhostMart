using DefaultNamespace;
using UnityEngine;

namespace Customer
{
    public class ListItem
    {
        public Vector3 Position { get; }
        public Stand Stand { get; }
        public int ProductsCount { get; }

        public ListItem(Vector3 position, Stand stand, int count)
        {
            Position = position;
            Stand = stand;
            ProductsCount = count;
        }
    }
}