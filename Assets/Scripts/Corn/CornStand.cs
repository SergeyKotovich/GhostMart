using Interfaces;
using UnityEngine;

namespace Corn
{
    public class CornStand : MonoBehaviour, IStand
    {
        public bool IsAvailable { get; private set; }
    }
}