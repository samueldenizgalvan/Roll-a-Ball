using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winMenuUI; // El Panel con los botones de reiniciar y siguiente mapa
    public Button restartButton; // Botón para reiniciar el mapa
    public Button nextMapButton; // Botón para ir al siguiente mapa

    void Start()
    {
        winMenuUI.SetActive(false); // Asegúrate de que el menú está desactivado al inicio
    }

    public void ShowWinMenu()
    {
        winMenuUI.SetActive(true); // Activa el panel cuando ganas
    }

    private IEnumerator ShowWinMenuAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        winMenuUI.SetActive(true); // Muestra el menú con los botones
    }

    public void RestartGame()
    {
        // Reinicia el nivel actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextMap()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Cargar el siguiente mapa si existe
        }
        else
        {
            Debug.Log("No hay más mapas.");
        }
    }
}
