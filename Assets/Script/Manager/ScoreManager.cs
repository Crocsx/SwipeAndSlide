using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int ScoreEnemyKilled = 10;
    public enum Score { EnemyKilled };

    float _score;
    public float score { get { return _score; } }

    public static ScoreManager instance = null;
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


    // Use this for initialization
    void Start()
    {

    }

    // Use this for initialization
    public void AddScore (Score value) {
        switch (value)
        {
            case Score.EnemyKilled:
                _score += ScoreEnemyKilled;
            break;
            default:
            break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
