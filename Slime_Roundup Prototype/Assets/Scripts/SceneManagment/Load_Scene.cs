using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene : MonoBehaviour
{
    public string sceneName;
    public LoadSceneMode loadSceneMode;
    
    public void LoadScene(){
        SceneManager.LoadScene(sceneName, loadSceneMode);
    }
}
