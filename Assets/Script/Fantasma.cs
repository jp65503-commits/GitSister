using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject olhos;
    [SerializeField] private GameObject fantasma;

    [Header("Configuracoes")]
    [SerializeField] private float velocidade = 3f;
    [SerializeField] private LayerMask plataformaLayer;

    private Transform player;
    private Vector3 posicaoInicial;

    private void Start()
    {
        posicaoInicial = fantasma.transform.position;
        olhos.SetActive(false);
    }

    private void Update()
    {
        bool podeSeguir = false;

        if (player != null)
        {
            Vector3 direcao = player.position - olhos.transform.position;
            float distancia = direcao.magnitude;

            bool paredeNaFrente = Physics.Raycast(olhos.transform.position, direcao.normalized, distancia, plataformaLayer);

            Debug.DrawRay(olhos.transform.position, direcao, paredeNaFrente ? Color.red : Color.green);

            podeSeguir = !paredeNaFrente;
        }

        if (podeSeguir)
        {
            fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, player.position, velocidade * Time.deltaTime);
        }
        else
        {
            fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, posicaoInicial, velocidade * Time.deltaTime);
        }

        olhos.SetActive(podeSeguir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entrou na visao");
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Saiu da visao");
            player = null;
        }
    }
}