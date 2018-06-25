using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager instance = null;

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    private void OnEnable()
    {
        TouchManager.instance.OnSwipe += StartStage;
    }

    private void OnDisable()
    {
        TouchManager.instance.OnSwipe -= StartStage;
    }

    // Use this for initialization
    void Start () {
        GameManager.instance.InitGame();
    }

    void StartStage(TouchStruct touch, Vector2 direction)
    {
        GameManager.instance.StartGame();
        MusicPlayer.instance.Play();
    }

    public void PauseStage()
    {
        GameManager.instance.PauseGame();
        MusicPlayer.instance.Pause();
    }

    public void ResumeStage()
    {
        GameManager.instance.ResumeGame();
        MusicPlayer.instance.Play();
    }


    public void EndStage()
    {
        GameManager.instance.ResumeGame();
        GameManager.instance.FinishGame();
    }

    public void ReplayStage()
    {
        EndStage();
        GameManager.instance.ReloadScene();
    }

    public void BackMenu()
    {
        EndStage();
        GameManager.instance.LoadScene("Menu");
    }
}
