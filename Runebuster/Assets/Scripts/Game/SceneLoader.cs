using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level");
    } 
    
    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions");
    } 
    
    public void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }

}
