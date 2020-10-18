using System.Collections;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance levelMusicInstance;
    private bool levelMusicCanPlay = true;
    private float lengthOfLevelMusic;
    private string nameOfSceneToCheckAgainstCurrentSceneName;
    

    private void Update()
    {
        DashSoundMethod();
        LevelMusicUpdate();
        CheckIfSceneChanged();
    }

    private void DashSoundMethod()
    {
        if (PlayerControl.speedBoostBool == true)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Speed_Boost");
            instance.start();
            instance.release();
        }
        else
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
    }

    private void LevelMusicUpdate()
    {
        if (levelMusicCanPlay == true)
        {
            StartCoroutine(LevelMusicCheck());
        }
    }

    private void LevelMusicSelection()
    {
        if (LevelSelect.currentSceneName == "Menu")
        {
            levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Menu_Music");
            lengthOfLevelMusic = 48f;
            levelMusicInstance.start();
        }
        else if (LevelSelect.currentSceneName == "Credits")
        {
            levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Credits_Music");
            lengthOfLevelMusic = 29f;
            levelMusicInstance.start();
        }
        else if (LevelSelect.currentSceneName == "Level1")
        {
            levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music_1");
            lengthOfLevelMusic = 29f;
            levelMusicInstance.start();
        }
        else
        {
            RepeatMethodForLevelMusic();
        }
    }

    private void CheckIfSceneChanged()
    {
        if (nameOfSceneToCheckAgainstCurrentSceneName != LevelSelect.currentSceneName)
        {
            RepeatMethodForLevelMusic();
            levelMusicCanPlay = true;
        }
    }

    public void RepeatMethodForLevelMusic()
    {
        levelMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        levelMusicInstance.release();
    }

    private IEnumerator LevelMusicCheck()
    {
        levelMusicCanPlay = false;
        nameOfSceneToCheckAgainstCurrentSceneName = LevelSelect.currentSceneName;
        LevelMusicSelection();
        yield return new WaitForSeconds(lengthOfLevelMusic);
        levelMusicCanPlay = true;
    }
}
