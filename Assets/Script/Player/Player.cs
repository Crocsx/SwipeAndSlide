using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    PlayerControl pControl;
    PlayerDamageHandler pDamageHandler;

    void Awake () {
        pControl = GetComponent<PlayerControl>();
        pDamageHandler = GetComponent<PlayerDamageHandler>();
    }

	public void TakeDamage()
    {
        pDamageHandler.GetDamage();
    }
}
