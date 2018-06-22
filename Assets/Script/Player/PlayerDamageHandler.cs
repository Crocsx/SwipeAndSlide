using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour {
    public delegate void onDeath();
    public event onDeath OnDeath;

    public delegate void onDamageRecieved();
    public event onDamageRecieved OnDamageRecieved;

    public int life = 3;

    public void GetDamage()
    {
        if (OnDamageRecieved != null)
            OnDamageRecieved();

        life--;

        if (life <= 0)
            Die();
    }

    public void Die()
    {
        if (OnDeath != null)
            OnDeath();
    }
}
