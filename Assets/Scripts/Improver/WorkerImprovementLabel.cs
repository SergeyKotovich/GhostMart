using System;
using TMPro;
using UnityEngine;

[Serializable]
public class WorkerImprovementLabel
{
    [field:SerializeField]
    public TextMeshProUGUI Label { get; private set; }
    
    [field:SerializeField]
    public WorkerTypes WorkerType { get; private set; }
}