using UnityEngine;

public class ghostBehaviorScript : MonoBehaviour
{
    private float edgeDistance = 1.0f;
    private float screenHeight, screenWidth;
    private Vector3 screenPosition;

    [SerializeField] private float ghostSpeed = .5f;
    [SerializeField] private Vector2 ghostDir;
    private Vector2 currentPosition, newPosition;

    [SerializeField] private SpriteRenderer ghostSprite;
    

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        ghostDir = new Vector2(GhostRandomAngle(-5f,5f), GhostRandomAngle(-5f,5f));
        SpriteFlip();
    }

    void Update()
    {
        CheckEdgeCollision();
        GhostMovement();
    }

    /// <summary>
    /// Checks which edge the ghost collides with.
    /// </summary>
    private void CheckEdgeCollision()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < edgeDistance)
        {
            HandleEdgeCollision("Left");
        }
        else if (screenPosition.x > screenWidth - edgeDistance)
        {
            HandleEdgeCollision("Right");
        }
        if (screenPosition.y < edgeDistance)
        {
            HandleEdgeCollision("Bottom");
        }
        else if (screenPosition.y > screenHeight - edgeDistance)
        {
            HandleEdgeCollision("Top");
        }
    }

    /// <summary>
    /// Function to handdle how the ghost moves after colliding with an edge.
    /// </summary>
    private void HandleEdgeCollision(string s)
    {
        if(s == "Left")
        {
            ghostDir = new Vector2(GhostRandomAngle(0f,5f), GhostRandomAngle(-5f,5f));
            SpriteFlip();
        }
        if(s == "Right")
        {
            ghostDir = new Vector2(GhostRandomAngle(-5f,0f), GhostRandomAngle(-5f,5f));
            SpriteFlip();
        }
        if(s == "Bottom")
        {
            ghostDir = new Vector2(GhostRandomAngle(-5f,5f), GhostRandomAngle(0f,5f));
            SpriteFlip();
        }
        if(s == "Top")
        {
            ghostDir = new Vector2(GhostRandomAngle(-5f,5f), GhostRandomAngle(-5f,0f));
            SpriteFlip();
        }
    }

    /// <summary>
    /// Function to return the ghost angle with out it being 0.
    /// </summary>
    private float GhostRandomAngle(float min, float max)
    {
        float rnd = Random.Range(min,max);
        while (rnd == 0)
        {
            rnd = Random.Range(min,max);
        }
        return rnd;
    }

    /// <summary>
    /// Allows the ghost to be translated across the screen.
    /// </summary>
    private void GhostMovement()
    {
        currentPosition = transform.position;
        newPosition = currentPosition + ghostDir * ghostSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    /// <summary>
    /// Flips sprite.
    /// </summary>
    private void SpriteFlip()
    {
        if(ghostDir.x > 0)
        {
            ghostSprite.flipX = true;
        }
        if(ghostDir.x < 0)
        {
            ghostSprite.flipX = false;
        }
    }
}
