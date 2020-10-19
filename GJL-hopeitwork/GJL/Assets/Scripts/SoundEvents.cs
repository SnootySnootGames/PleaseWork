using System.Collections;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] private AudioClip gameplaySong1;
    [SerializeField] private AudioClip gameplaySong2;
    [SerializeField] private AudioClip gameplaySong3;
    [SerializeField] private AudioClip gameplaySong4;
    [SerializeField] private AudioClip MainMenuSong;
    [SerializeField] private AudioClip CreditsSong;
    private FMOD.Studio.EventInstance instance;
    private bool levelMusicCanPlay = true;
    private string nameOfSceneToCheckAgainstCurrentSceneName;
    private AudioClip whichClipToPlay;

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
        if (LevelSelect.gameOver == false)
        {
            if (LevelSelect.currentSceneName == "Menu")
            {
                whichClipToPlay = MainMenuSong;
                //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Menu_Music");
                //lengthOfLevelMusic = 48f;
                //levelMusicInstance.start();
            }
            else if (LevelSelect.currentSceneName == "Credits")
            {
                whichClipToPlay = CreditsSong;
                //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Credits_Music");
                //lengthOfLevelMusic = 29f;
                //levelMusicInstance.start();
            }
            else if (LevelSelect.currentSceneName == "Level1")
            {
                whichClipToPlay = gameplaySong1;
                //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music_1");
                //lengthOfLevelMusic = 29f;
                //levelMusicInstance.start();
            }
            else if (LevelSelect.currentSceneName == "Level2")
            {
                whichClipToPlay = gameplaySong2;
                //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music_3");
                //lengthOfLevelMusic = 29f;
                //levelMusicInstance.start();
            }
            else if (LevelSelect.currentSceneName == "Level3")
            {
                whichClipToPlay = gameplaySong3;
                //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music_4");
                //lengthOfLevelMusic = 30f;
                //levelMusicInstance.start();
            }
            else
            { 
            
            }
        }
        else
        {
            whichClipToPlay = gameplaySong4;
            //levelMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Gameplay_Music_2");
            //lengthOfLevelMusic = 30f;
            //levelMusicInstance.start();
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
        cameraAudioSource.Stop();
    }

    private IEnumerator LevelMusicCheck()
    {
        levelMusicCanPlay = false;
        nameOfSceneToCheckAgainstCurrentSceneName = LevelSelect.currentSceneName;
        LevelMusicSelection();
        cameraAudioSource.PlayOneShot(whichClipToPlay);
        yield return new WaitForSeconds(whichClipToPlay.length);
        levelMusicCanPlay = true;
    }
}
