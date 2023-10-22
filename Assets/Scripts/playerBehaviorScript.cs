using UnityEngine;
using UnityEngine.UI;

public class playerBehaviorScript : MonoBehaviour
{

    public gameManagerScript gameManager;
    [SerializeField] private int playerHealth = 3;
    public Image[] pumpkinHealth;
    public Sprite pumpkinDeactive;

    void Start()
    {
        gameManager = FindObjectOfType<gameManagerScript>();
    }

    void Update()
    {
        //Game end.
        if(playerHealth <= 0)
        {
            gameManager.GameOver();
        }
    }

    /// <summary>
    /// Function to subtract health.
    /// </summary>
    public void SubtractHealth()
    {
        playerHealth -= 1;
        pumpkinHealth[playerHealth].sprite = pumpkinDeactive;
        Debug.Log("Player Health: " + playerHealth);
    }
}
