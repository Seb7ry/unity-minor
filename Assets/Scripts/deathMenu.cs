using UnityEngine;

public class deathMenu : MonoBehaviour
{
    [SerializeField] private pauseMenu pauseMenuScript;

    public void RestartGame()
    {
        pauseMenuScript.Reiniciar();
    }

    public void ReturnToMainMenu()
    {
        pauseMenuScript.VolverMenu();
    }

    public void ShowDeathMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; 
    }
}
