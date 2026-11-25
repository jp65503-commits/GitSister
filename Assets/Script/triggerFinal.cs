using UnityEngine;

public class triggerFinal : MonoBehaviour

{
    [Header("Verificacoes")]
    [SerializeField] private bool interagiu;
    [Header("Referencias")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private GameObject canvasFalaIrmao;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !interagiu)
        {
            Debug.Log("Interagiu");
            interagiu = true;
            playerMove.cutscene = true;
            canvasFalaIrmao.gameObject.SetActive(true);
        }
    }
}
