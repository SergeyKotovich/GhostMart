using UnityEngine;

namespace DefaultNamespace.Corn
{
    public class CornStand : MonoBehaviour, IStand
    {
        public bool IsAvailable { get; private set; }
    }
}