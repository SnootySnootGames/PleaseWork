using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject creditsGameobject;
    [SerializeField] private GameObject mainMenuGameobject;
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject characterSelectScreen;

    private FMOD.Studio.EventInstance instanceButtonClick;
    private Scene currentScene; //store the current scene to check if scene is "level selection" scene
    public static string currentSceneName;
    

    // Start is called before the first frame update
    void Start()
    {
        instanceButtonClick = FMODUnity.RuntimeManager.CreateInstance("event:/Candy_Steal");
        GetActiveSceneName();        
    }

    private void Update()
    {
        CandyAmountPerLevel();
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

    public void StartGame()
    {
        instanceButtonClick.start();
        GameMasterScript.previousScene = currentSceneName;
        currentSceneName = "Level1";
        SceneManager.LoadScene(currentSceneName);
    }

    private void CandyAmountPerLevel()
    {
        if (currentSceneName == "Level1")
        {
            Score.candyAmountPerLevel = 6;
        }
    }

}
