using UnityEngine;
using UnityEngine.InputSystem;

public class mouseInteractionScript : MonoBehaviour
{ 

    public playerBehaviorScript player;
    public AudioSource playerAudioSource;
    public AudioClip burnSound;
    public AudioClip lighterSound;
    public soundScript mainSound;
    private float volume;

    void Start()
    {
        mainSound = FindAnyObjectByType<soundScript>();
        volume = mainSound.gameVolume;
        playerAudioSource.volume = volume;
    }

    void Update()
    {
        if(volume != mainSound.gameVolume)
        {
            volume = mainSound.gameVolume;
            playerAudioSource.volume = volume;
        }
    }

    /// <summary>
    /// Handles player click input.
    /// </summary>
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            // Creates vector from mouse position to first gameobject with a collider.
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Checks if the ray hit a candle
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Candle"))
            {
                candleInteractionScript candle = hit.collider.gameObject.GetComponent<candleInteractionScript>();
                if(candle.CandleCheck())
                {
                    // Burns player if candle is lit.
                    player.SubtractHealth();
                    playerAudioSource.clip = burnSound;
                    playerAudioSource.Play();
                }
                else
                {
                    // Light candle if not lit.
                    candle.LightCandle();
                    playerAudioSource.clip = lighterSound;
                    playerAudioSource.Play();
                }
            }
        }
    }
}
