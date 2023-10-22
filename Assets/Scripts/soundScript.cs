using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class soundScript : MonoBehaviour
{
    private static soundScript instance;
    public Slider soundBar;
    public float gameVolume = .50f;
    public AudioSource music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            if(soundBar == null)
            {
                soundBar = FindAnyObjectByType<Slider>();
                soundBar.value = gameVolume;
            }
            if(soundBar != null && soundBar.value != gameVolume)
            {
                gameVolume = soundBar.value;
            }
        }
        if(music.volume != gameVolume)
        {
            music.volume = gameVolume;
        }
    }
}