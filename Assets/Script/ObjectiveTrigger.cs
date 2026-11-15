using UnityEngine;

// Collider (Is Trigger) no fim do castelo: troca o objetivo ao jogador passar.
public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField, TextArea] private string newObjective = "Atravesse o bosque";
    private bool done;

    void OnTriggerEnter(Collider other)
    {
        if (done || !other.CompareTag("Player")) return;
        done = true;
        ObjectiveHUD.Instance.SetObjective(newObjective);
    }
}
