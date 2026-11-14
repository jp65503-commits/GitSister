using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class administrador : MonoBehaviour
{

    public GameObject cover;
   public void SairDoJogo()
    {
        Application.Quit();
    }

    public void ComecarJogo()
    {
        cover.SetActive(true);
    }

    public void DisabilitarBotao()
    {
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        clickedButton.interactable = false;
    }


    public void Continuar()
    {
        SceneManager.LoadScene("1Fase");
    }
}
