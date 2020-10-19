using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameMasterScript : MonoBehaviour
{

    [SerializeField] private bool paused = false;
    [SerializeField] private Light2D MainMenuGlobalLight;
    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;

    public static bool pause = false;
    public static int currentLevel = 1;
    public static int level1Score;
    public static bool characterSelection = true; //true = girl, false = boy
    public static string previousScene;
    public static bool level1completed = false;
    public static bool level2completed = false;
    public static bool level3completed = false;

    private bool globalLightChange = true;
    private GameObject pauseMenu;

    private void Update()
    {
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        PauseGameInput();
        PauseGameFunction();
        if (boy != null & girl != null)
        {
            CharacterSelect();
        }
        if (MainMenuGlobalLight != null)
        {
            GlobalLightChange();
        }
    }

    private void PauseGameInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
    }

    public void CancleQuitMenu()
    {
        paused = !paused;
    }

    public void MethodForQuit()
    {
        Application.Quit();
    }

    private void PauseGameFunction()
    {
        if (paused == true)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    private void GlobalLightChange()
    {
        if (MainMenuGlobalLight.intensity >= 0.6f && globalLightChange == true)
        {
            MainMenuGlobalLight.intensity -= Time.deltaTime / Random.Range(8, 10);
        }
        else
        {
            globalLightChange = false;
        }

        if (MainMenuGlobalLight.intensity <= 1 && globalLightChange == false)
        {
            MainMenuGlobalLight.intensity += Time.deltaTime / Random.Range(8,10);
        }
        else
        {
            globalLightChange = true;
        }
    }

    private void CharacterSelect()
    {
        if (characterSelection == true)
        {
            girl.SetActive(true);
            boy.SetActive(false);
        }
        else if (characterSelection == false)
        {
            girl.SetActive(false);
            boy.SetActive(true);
        }
    }

    public void CharacterSwap()
    {
        characterSelection = !characterSelection;
    }
}
