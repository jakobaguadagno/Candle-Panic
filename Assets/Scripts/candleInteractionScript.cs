using UnityEngine;

public class candleInteractionScript : MonoBehaviour
{
    [SerializeField] private float candleTimer = 10f;
    [SerializeField] private float darknessTime = 3f;
    private float darknessTimer = 3f;
    private bool candleLit = true;
    private bool addingDarkness = false;
    public gameManagerScript gameManager;
    private Animator animator;
    [SerializeField] private float maxCandleTimer = 10f;
    [SerializeField] private float minCandleTimer = 3f;

    void Start()
    {
        gameManager = FindObjectOfType<gameManagerScript>();
        animator = this.GetComponent<Animator>();
        darknessTimer = darknessTime;
        CandleTimeGenerator();
        candleLit = true;
    }
    void Update()
    {
        CandleTimer();
    }

    /// <summary>
    /// Function to call when candle is clicked on by the player to reset.
    /// </summary>
    public void LightCandle()
    {
        //Debug.Log("Candle Lit");
        CandleTimeGenerator();
        if(!candleLit)
        {
            animator.SetBool("out", false);
            gameManager.SubtractTemporaryDarkness();
            gameManager.DarknessOverlaySwitch();
            if(!addingDarkness)
            {
                gameManager.SubtractDarkness();
            }
        }
        darknessTimer = darknessTime;
        candleLit = true;
    }

    /// <summary>
    /// Handles the logic for the candle timer.
    /// </summary>
    private void CandleTimer()
    {
        if(candleTimer <= 0)
        {
            CandleOut();
        }
        if(candleLit)
        {
            candleTimer -= Time.deltaTime;
            animator.SetFloat("time", candleTimer);
        }
    }

    /// <summary>
    /// Handles the random time the candle can be lit for.
    /// </summary>
    private void CandleTimeGenerator()
    {
        candleTimer = Random.Range(minCandleTimer, maxCandleTimer);
    }

    /// <summary>
    /// Handles all the events when the candle goes out.
    /// </summary>
    private void CandleOut()
    {
        if(candleLit)
        {
            //Debug.Log("Candle Out!");
            addingDarkness = true;
            animator.SetBool("out", true);
            gameManager.AddTemporaryDarkness();
            gameManager.DarknessOverlaySwitch();
            candleLit = false;
        }
        if(!candleLit)
        {
            // Counts the time the candle has been out.
            if(addingDarkness && darknessTimer > 0)
            {
                darknessTimer -= Time.deltaTime;
            }
            // Adds darkness when time has passed.
            if(addingDarkness && darknessTimer <= 0)
            {
                gameManager.AddDarkness();
                addingDarkness = false;
            }
        }
    }

    /// <summary>
    /// Checks if the candle is lit or not.
    /// </summary>
    public bool CandleCheck()
    {
        return candleLit;
    }
}
