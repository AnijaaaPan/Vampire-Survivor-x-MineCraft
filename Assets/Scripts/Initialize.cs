using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    private const string InitializeSceneName = "Title";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInitializeApplication()
    {
        if (!SceneManager.GetSceneByName(InitializeSceneName).IsValid())
        {
            SceneManager.LoadScene(InitializeSceneName);
        }
    }
}