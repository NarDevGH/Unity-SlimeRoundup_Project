using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    Scene scene;

    private void Awake() {
        scene = SceneManager.GetActiveScene();
    }

    public void LoadScene(){
        SceneManager.LoadScene(scene.name,LoadSceneMode.Single);
    }
}
