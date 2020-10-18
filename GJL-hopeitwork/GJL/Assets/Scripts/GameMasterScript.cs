using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameMasterScript : MonoBehaviour
{

    [SerializeField] private bool paused = false;
    [SerializeField] private Light2D MainMenuGlobalLight;
    [SerializeField] private GameObject boy;
    [SerializeField] private GameObject girl;

    public static int currentLevel = 1;
    public static int level1Score;
    public static bool characterSelection = true; //true = girl, false = boy
    public static string previousScene;

    private bool globalLightChange = true;

    private void Update()
    {
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

    private void PauseGameFunction()
    {
        if (paused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
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
