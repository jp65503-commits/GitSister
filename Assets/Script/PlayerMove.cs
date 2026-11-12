using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Refer ncias")]
    [SerializeField] private Rigidbody rigidBodyPlayer;
    [SerializeField] private BoxCollider groundedTrigger;
    [SerializeField] private TrailRenderer jumpTrail;
    [SerializeField] private GameObject landingParticles;

    [Header("Ajustes da Movimentação")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutOffForce;

    [Header("Ajustes do Coyote Time")]
    [SerializeField] private float setCoyoteTime;
    [SerializeField] private float currentCoyoteTime;

    [Header("Verificaçoes")]
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool canJump;
    [SerializeField] private bool hasLeftGround;

    // Guarda a dire  o horizontal desejada pelo jogador
    private float inputHorizontal;

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (!isGrounded && !hasLeftGround && currentCoyoteTime > 0f)
        {
            hasLeftGround = true;
        }

        // Detecta quando o jogador aperta o espa o (isso acontece apenas uma vez por click pq estamos utilizando o "Down")
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transform.SetParent(null);

            // Mantemos a velocidade no eixo X (ou seja esquerda e direita) e aplicamos um impulso no eixo Y (Horizontal) com base na variavel "jumpForce"
            rigidBodyPlayer.linearVelocity = new Vector2(rigidBodyPlayer.linearVelocity.x, jumpForce);

            // Aqui resetamos o pulo para o player n conseguir pular infinitamente depois de ja ter pulado
            currentCoyoteTime = setCoyoteTime;
            canJump = false;
        }

        // Detecta quando o jogador solta o Espa o e se o player ainda est  subindo com o pulo (basicamente, se essas duas condi  es forem verdadeiras, a velocidade vertical   reduzida)
        // Ou de maneira mais reconhecivel kkk segure para pul o clicke para pulinho
        if (Input.GetKeyUp(KeyCode.Space) && rigidBodyPlayer.linearVelocity.y > 0)
        {
            rigidBodyPlayer.linearVelocity = new Vector2(rigidBodyPlayer.linearVelocity.x, rigidBodyPlayer.linearVelocity.y * jumpCutOffForce);
        }

        // *C LCULOS PARA O "COYOTE TIME" ABAIXO*
        // Coyote time e um elemento bem importante para plataformers, ele basicamente deixa o player pular mesmo se ele tiver perdido a janela de pulo por um momento bem rapido
        // Isso acontece porque players tendem a querer pular no ultimo segundo e as vezes caem no ultimo frame kkk, basicamente deixa a experiencia menos frustrante

        if (!isGrounded && currentCoyoteTime < setCoyoteTime)
        {
            currentCoyoteTime += Time.deltaTime;

            currentCoyoteTime = Mathf.Clamp(currentCoyoteTime, 0f, setCoyoteTime);

            if (currentCoyoteTime >= setCoyoteTime)
            {
                canJump = false;
            }
        }

        // Aqui nos resetamos o tempo que o player tem para pular, eu estou usando o "currentCoyoteTime != 0" para o jogo n ficar reiniciando essa variavel todo frame

        if (isGrounded && rigidBodyPlayer.linearVelocity.y <= 0 && currentCoyoteTime != 0 && hasLeftGround)
        {
            currentCoyoteTime = 0;
            jumpTrail.emitting = false;
            Instantiate(landingParticles, groundedTrigger.transform.position, landingParticles.transform.rotation);
            hasLeftGround = false;
            canJump = true;
        }

        // Aqui estou ativando o trail caso o player esteja no ar

        if (!isGrounded && jumpTrail.emitting == false && hasLeftGround)
        {
            jumpTrail.emitting = true;
        }
    }

    // *C LCULOS DE MOVIMENTA  O ABAIXO*
    // *TAMB M   POSS VEL FAZER A MOVIMENTA  O NO UPDATE NORMAL ^^^, MAS PREFIRO USAR O FIXEDUPDATE POR SER MAIS ADEQUADO PARA OPERA  ES DE F SICA*
    // *DEPENDENDO DE COMO A MOVIMENTA  O FOR FEITA NO UPDATE,   NECESS RIO USAR TIME.DELTATIME PARA QUE ELA N O DEPENDA DO FPS :p*

    void FixedUpdate() // O FixedUpdate   parecido com o Update normal, a diferen a   que ele   chamado em intervalos fixos de tempo
    {
        // Esse   o c lculo base da movimenta  o, basicamente ele pega a dire  o, multiplica pela velocidade e aplica ao Rigidbody2D do player
        rigidBodyPlayer.linearVelocity = new Vector2(inputHorizontal * moveSpeed, rigidBodyPlayer.linearVelocity.y);

        // Essa verifica  o esta sendo feita para verificar se o p  do player "GroundedTrigger" ta encostando em alguma plataforma
        // Basicamente para verificar se o player pode pular ou n o
        isGrounded = Physics.CheckBox(groundedTrigger.bounds.center, groundedTrigger.bounds.extents, groundedTrigger.transform.rotation, LayerMask.GetMask("Plataforma"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlataformaMovel") && transform.parent != collision.transform && isGrounded)
        {
            transform.SetParent(collision.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlataformaMovel") && collision.gameObject.activeInHierarchy)
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Aqui tem uma verificação que basicamente fala se o player (dono do script) entrou em contato com um trigger com a tag "Moeda"
    /*    if (collision.CompareTag("Moeda"))
        {
            // O "++" aumenta o valor da variavel "coinAmount" em 1
            coinAmount++;

            // Aqui nós atualizamos o texto do contador de moedas, convertendo a quantidade de moedas coletadas em texto usando o "ToString()"
            coinCounterText.text = coinAmount.ToString();

            // Aqui sei que o "collision" tem a tag "Moeda", e tambem sei que ele possui o script "ScriptMoeda" então uso o "GetComponent" para pegar uma referencia de seu script e chamo a funçao de pegar a moeda
            // Se o objeto tiver a tag "Moeda" e não tiver o script "ScriptMoeda" vai dar um erro, mas nos temos certeza que ele tem, logo n é necessario fazer nenhuma verificação extra :p
            collision.GetComponent<ScriptMoeda>().CollectCoin();
        }*/

        // Aqui faço as verificaçoes para o pulo extra
     /*   if (collision.CompareTag("PuloExtra"))
        {
            // Aqui a logica e parecida com o que aconteceu acima ^^^ mas ao invez de so utilizar o script eu armazeno ele temporariamente
            ScriptPuloExtra puloExtra = collision.GetComponent<ScriptPuloExtra>();

            // Se o pulo extra estiver ativo e o player não estiver no chão e não pode mais pular, o player pode pular novamente
            if (puloExtra.active && !playerMove.isGrounded && !playerMove.canJump)
            {
                playerMove.canJump = true;
                puloExtra.DesativarPuloExtra();
            }
        }*/

     /*   if (collision.CompareTag("Dano"))
        {
            playerMorte.KillPlayer();
        }*/
    }
}