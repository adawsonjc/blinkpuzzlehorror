using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private float currentHp = 3;
    private float maximumHP = 3;

    private void Awake()
    {
        this.currentHp = maximumHP;
    }

    public float getCurrentHp()
    {
        return this.currentHp;
    }

    public float addHp(float hp)
    {
        return this.currentHp += hp < this.maximumHP ? this.currentHp += hp : this.maximumHP;
    }

    public float removeHp(float hp)
    {
        return this.currentHp -= hp <= 0 ? 0 : this.currentHp -= hp;
    }

}
