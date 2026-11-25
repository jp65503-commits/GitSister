using UnityEngine;

public class ScriptPlataformaMovel : MonoBehaviour
{
    [Header("Ajustes da Plataforma")]
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 secondPosition;
    [SerializeField] private float speed;
    [SerializeField] public bool ativo;

    private float timer;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (ativo)
        {
            timer += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(initialPosition, secondPosition, Mathf.PingPong(timer, 1f));
        }
    }
}