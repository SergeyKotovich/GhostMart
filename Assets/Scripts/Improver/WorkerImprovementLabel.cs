using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WorkerImprovementLabel
{
    [field:SerializeField]
    public TextMeshProUGUI Label { get; private set; }
    [field:SerializeField]
    public Button ImproveButton { get; private set; }
    
    [field:SerializeField]
    public WorkerTypes WorkerType { get; private set; }
}