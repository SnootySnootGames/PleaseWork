using UnityEngine;

public class CandyScript : MonoBehaviour
{

    [SerializeField] private int scorePerCandy = 10;
    [SerializeField] private float comboTimerCount;
    [SerializeField] private float comboTimerReset = 3f;
    [SerializeField] private Sprite[] candyChoices;
    
    private FMOD.Studio.EventInstance instance;
    private SpriteRenderer candySpriteRend;

    private void Start()
    {
        candySpriteRend = gameObject.GetComponent<SpriteRenderer>();
        int randomCandyTextInt = Random.Range(0, candyChoices.Length);
        candySpriteRend.sprite = candyChoices[randomCandyTextInt];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Candy_Grab");
            instance.start();
            Score.candyCollected = true;
            gameObject.SetActive(false);
            Score.totalCandyCollected++;
        }
    }
}
