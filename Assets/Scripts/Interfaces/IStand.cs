using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IStand
    {
        public List<StandCell> StandCells { get; }
    }
}