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

    // Use this for initialization
    void Start () {
        GameManager.instance.InitGame();
        TouchManager.instance.OnSwipe += StartStage;
    }

    void StartStage(TouchStruct touch, Vector2 direction)
    {
        GameManager.instance.StartGame();
        MusicPlayer.instance.Play();
        TouchManager.instance.OnSwipe -= StartStage;
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


    void OnDestroy ()
    {
        TouchManager.instance.OnSwipe -= StartStage;
    }
}
