using UnityEngine;
using UnityEngine.SceneManagement;

public class startButtonScript : MonoBehaviour
{
    public void StartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
