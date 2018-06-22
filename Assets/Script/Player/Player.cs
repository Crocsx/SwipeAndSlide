using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    PlayerControl pControl;
    PlayerDamageHandler pDamageHandler;

    void Start () {
        pControl = transform.GetComponent<PlayerControl>();
        pDamageHandler = transform.GetComponent<PlayerDamageHandler>();
    }

	public void TakeDamage()
    {
        pDamageHandler.GetDamage();
    }
}
