using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange: MonoBehaviour
{
    // Load scene by name & index 
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
