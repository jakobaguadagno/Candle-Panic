using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    //Game health
    private int darknessCounter = 0;
    public Color darknessOverlayColor;
    public Color ghostColor;
    public Image darknessOverlay;
    public SpriteRenderer[] ghosts;
    private bool lightTransition = false;
    private float lightLevel = 0;
    private float elapsedTimer = 0;
    private float originalAlpha;
    private int tempDarkness = 0;
    private float gameTimer = 0;
    public Text gameTimerText;
    private bool gameOver = false;
    private float gameOverTimer = 0;
    public GameObject gameOverPanel;
    [SerializeField] private float darknessTimer = 2f;

    void Start()
    {
        // Making the ghost invisible at the start.
        foreach (SpriteRenderer ghost in ghosts)
        {
            ghost.color = ghostColor;
        }
    }

    void Update()
    {
        
        if(darknessCounter == 3 && !gameOver)
        {
            GameOver();
        }
        if(lightTransition)
        {
            SmoothVisuals();
        }
        if(gameOver)
        {
            gameOverTimer += Time.deltaTime;
            if(gameOverTimer >= 5)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
        else
        {
            gameTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Function to add darkness.
    /// </summary>
    public void AddDarkness()
    {
        darknessCounter += 1;
        Debug.Log("Darkness Count: " + darknessCounter);
    }

    /// <summary>
    /// Function to subtract darkness.
    /// </summary>
    public void SubtractDarkness()
    {
        darknessCounter -= 1;
        Debug.Log("Darkness Count: " + darknessCounter);
    }

    /// <summary>
    /// Function to add temporary darkness for transition.
    /// </summary>
    public void AddTemporaryDarkness()
    {
        tempDarkness += 1;
        //Debug.Log("Temporary Darkness Count: " + tempDarkness);
    }

    /// <summary>
    /// Function to temporary darkness for transition.
    /// </summary>
    public void SubtractTemporaryDarkness()
    {
        tempDarkness-= 1;
        //Debug.Log("Temporary Darkness Count: " + tempDarkness);
    }

    /// <summary>
    /// Function to call when the game ends.
    /// </summary>
    public void GameOver()
    {
        //Debug.Log("Game Over!");
        gameTimerText.text = gameTimer + " seconds!";
        gameOverPanel.SetActive(true);
        gameOver = true;
    }

    /// <summary>
    /// Function to set the dimming effect when darkness changes.
    /// </summary>
    public void DarknessOverlaySwitch()
    {
        // Sets visablity for darkness overlay, ghosts, and clouds.
        switch(tempDarkness)
        {
            case 0:
                StartTransition(0f);
                break;
            case 1:
                StartTransition(0.50f);
                break;
            case 2:
                StartTransition(0.75f);
                break;
            case 3:
                StartTransition(1f);
                break;
            
        }
    }

    /// <summary>
    /// Function to handle the alpha changing over time.
    /// </summary>
    private float SmoothTransition(float originalV, float currentV, float targetV, float elapsedTime, float duration)
    {
        currentV = Mathf.Lerp(originalV, targetV, elapsedTime / duration);
        if (currentV == targetV)
        {
            return targetV;
        }
        return currentV;
    }

    /// <summary>
    /// Function to start the transition and set/reset values.
    /// </summary>
    private void StartTransition(float light)
    {
        originalAlpha = darknessOverlayColor.a;
        lightLevel = light;
        elapsedTimer = 0;
        lightTransition = true;
    }

    /// <summary>
    /// Function to smooth each of the objects color.
    /// </summary>
    private void SmoothVisuals()
    {
        elapsedTimer += Time.deltaTime;
        ghostColor.a = SmoothTransition(originalAlpha, ghostColor.a, lightLevel, elapsedTimer, darknessTimer);
        darknessOverlayColor.a = SmoothTransition(originalAlpha, darknessOverlayColor.a, lightLevel, elapsedTimer, darknessTimer);
        foreach (SpriteRenderer ghost in ghosts)
        {
            ghost.color = ghostColor;
        }
        darknessOverlay.color = darknessOverlayColor;
        if(darknessOverlayColor.a == lightLevel && ghostColor.a == lightLevel)
        {
            lightTransition = false;
        }
    }

}
