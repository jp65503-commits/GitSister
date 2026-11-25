using UnityEngine;

public class Caixa : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().sleepThreshold = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlataformaMovel"))
        {
            foreach (ContactPoint contato in collision.contacts)
            {
                if (contato.normal.y > 0.7f)
                {
                    transform.SetParent(collision.transform.parent, true);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlataformaMovel") && transform.parent == collision.transform.parent)
        {
            transform.SetParent(null, true);
        }
    }
}