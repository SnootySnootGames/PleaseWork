using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private int scorePerCandy = 10;
    [SerializeField] private float comboTimerCount = 0f;
    [SerializeField] private float comboTimerReset = 3f;
    [SerializeField] private TMP_Text scoreBoard;
    [SerializeField] private TMP_Text xBoard;
    [SerializeField] private TMP_Text candyPerLevelText;
    [SerializeField] private TMP_Text candyCollectPerLevel;
    public int currentScore = 0;
    public int numberOfCandiesCollectedDuringCombo = 0;
    public bool comboEnded = true;
    public int scoreMultiplier = 1;
    public static int totalCandyCollected = 0;
    public static int candyAmountPerLevel = 0;

    public static bool candyCollected = false;

    private void Start()
    {
        scoreBoard.text = currentScore.ToString();
    }

    private void Update()
    {
        ComboMethod();
        comboTimer();
        CandyCollectionTextShow();
    }

    private void ComboMethod()
    {
        if (candyCollected == true)
        {
            candyCollected = false;
            if (comboEnded == true)
            {
                xBoard.gameObject.SetActive(true);
                comboEnded = false;
                numberOfCandiesCollectedDuringCombo = 1;
                comboTimerCount = comboTimerReset;
                currentScore += 100;
                scoreBoard.text = currentScore.ToString();
            }
            else
            {
                comboTimerCount = comboTimerReset;
                numberOfCandiesCollectedDuringCombo++;
                MultiplierMethod();
                ScoreMethod();
                scoreBoard.text = currentScore.ToString();
            }
        }
    }

    private void comboTimer()
    {
        if (comboTimerCount > 0)
        {
            comboTimerCount -= Time.deltaTime;
            Color tmp = xBoard.color;
            tmp.a = comboTimerCount / comboTimerReset;
            xBoard.color = tmp;
        }
        else
        {
            comboEnded = true;
            xBoard.gameObject.SetActive(false);
            numberOfCandiesCollectedDuringCombo = 0;
            scoreMultiplier = 1;
            xBoard.text = "x" + scoreMultiplier.ToString();
        }
    }

    private void MultiplierMethod()
    {
        if (numberOfCandiesCollectedDuringCombo % 3 == 0)
        {
            scoreMultiplier++;
            xBoard.text = "x" + scoreMultiplier.ToString();
            Color randomColorSwap = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            xBoard.color = randomColorSwap;
        }
    }

    private void ScoreMethod()
    {
        currentScore += 100 * scoreMultiplier;

        if (GameMasterScript.currentLevel == 1)
        {
            GameMasterScript.level1Score = currentScore;
        }
    }

    private void CandyCollectionTextShow()
    {
        candyCollectPerLevel.text = totalCandyCollected.ToString();
        candyPerLevelText.text = "/" + candyAmountPerLevel.ToString();
    }
}
