using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public static TimeManager instance = null;

    #region Variables
    public float deltaTime
    {
        get
        {
            return _deltaTime;
        }
    }
    private float _deltaTime;

    public float fixedDeltaTime
    {
        get
        {
            return _fixedDeltaTime;
        }
    }
    private float _fixedDeltaTime;

    private float _modifier;
    #endregion

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
    }
    #endregion

    #region Native Methods
    void Start()
    {
        GameManager.instance.OnPauseGame += OnPause;
        GameManager.instance.OnResumeGame += OnResume;

        _deltaTime = 0;
        _fixedDeltaTime = 0;
        _modifier = 1;
    }

    void Update()
    {
        _deltaTime = Time.deltaTime * _modifier;
    }

    void FixedUpdate()
    {
        _fixedDeltaTime = Time.fixedDeltaTime * _modifier;
    }
    #endregion

    private void OnDestroy()
    {
        GameManager.instance.OnPauseGame -= OnPause;
        GameManager.instance.OnResumeGame -= OnResume;
    }
    #region Methods

    private void OnPause()
    {
        Time.timeScale = 0;
        _modifier = 0;
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        _modifier = 1;
    }
    #endregion
}
