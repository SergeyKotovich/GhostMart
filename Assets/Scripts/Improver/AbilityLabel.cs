using System;
using TMPro;
using UnityEngine;

namespace Improver
{
    [Serializable]
    public class AbilityLabel
    {
        [field: SerializeField] public TextMeshProUGUI[] TextLabel { get; private set; }
        [field: SerializeField] public AbilityTypes[] AbilityTypes { get; private set; }
        [field: SerializeField] public WorkerTypes WorkerType { get; private set; }
    }
}