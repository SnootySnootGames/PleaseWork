using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject creditsGameobject;
    [SerializeField] private GameObject mainMenuGameobject;
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject characterSelectScreen;
    [SerializeField] private GameObject story1;
    [SerializeField] private GameObject story;

    private FMOD.Studio.EventInstance instanceButtonClick;
    private Scene currentScene; //store the current scene to check if scene is "level selection" scene
    public static string currentSceneName;
    public static bool gameOver = false;
    public static bool gameOverLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        instanceButtonClick = FMODUnity.RuntimeManager.CreateInstance("event:/Candy_Steal");
        GetActiveSceneName();        
    }

    private void Update()
    {
        CandyAmountPerLevel();
        LevelWinState();
        if (story == null)
        {
            story = GameObject.FindGameObjectWithTag("story");
            story.SetActive(false);
        }
    }

    private void GetActiveSceneName()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }

    public void GoToCreditsPageFromMainMenu()
    {
        instanceButtonClick.start();
        GameMasterScript.previousScene = currentSceneName;
        currentSceneName = "Credits";
        creditsGameobject.SetActive(true);
        mainMenuGameobject.SetActive(false);
    }

    public void GoToMainMenuFromCredits()
    {
        instanceButtonClick.start();
        GameMasterScript.previousScene = currentSceneName;
        currentSceneName = "Menu";
        mainMenuGameobject.SetActive(true);
        creditsGameobject.SetActive(false);
    }

    public void GoToHowToPlay()
    {
        instanceButtonClick.start();
        howToPlay.SetActive(true);
        mainMenuGameobject.SetActive(false);
        creditsGameobject.SetActive(false);
    }

    public void GoToCharacterSelectFromHowToPlay()
    {
        instanceButtonClick.start();
        characterSelectScreen.SetActive(true);
        howToPlay.SetActive(false);
        mainMenuGameobject.SetActive(false);
        creditsGameobject.SetActive(false);
    }

    public void GoToStory1FromCharacterSelect()
    {
        instanceButtonClick.start();
        story1.SetActive(true);
        characterSelectScreen.SetActive(false);
    }

    public void StartGame()
    {
        instanceButtonClick.start();
        GameMasterScript.previousScene = currentSceneName;
        currentSceneName = "Level1";
        SceneManager.LoadScene(currentSceneName);
        gameOver = false;
    }

    public void Story2ToLevel2()
    {
        GameMasterScript.pause = false;
        instanceButtonClick.start();
        GameMasterScript.previousScene = currentSceneName;
        currentSceneName = "Level2";
        SceneManager.LoadScene(currentSceneName);
    }

    public void MethodForQuit()
    {
        Application.Quit();
    }

    private void CandyAmountPerLevel()
    {
        if (currentSceneName == "Level2")
        {
            Score.candyAmountPerLevel = 6;
        }
        else if (currentSceneName == "Level1")
        {
            Score.candyAmountPerLevel = 15;
        }
        else if (currentSceneName == "Level3")
        {
            Score.candyAmountPerLevel = 20;
        }
    }

    private void LevelWinState()
    {
                if (Score.totalCandyCollected == Score.candyAmountPerLevel)
                {
                    GameMasterScript.level1Score = Score.currentScore;
                    Score.currentScore = 0;
                    Score.totalCandyCollected = 0;
                    story.SetActive(true);
                }
    }
}
