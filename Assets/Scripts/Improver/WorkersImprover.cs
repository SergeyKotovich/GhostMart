using UnityEngine;

public class WorkersImprover : MonoBehaviour
{
    public void ImproveBasket(GameObject player)
    {
        var worker = player.GetComponent<IWorker>();
        var improver = worker.AbilitiesController;
        improver.ImproveBasket();
    }
}