﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Wrapper.FuncInt bossHP;
    public float attackTimer;
    public float attackTimerMax;

    public GameObject attack;
    GameObject whoops;

    public int hp;
    float rotSpeed = 200;

    public Attack[] attackList;
    // Use this for initialization
    void Start () {
        hp = 1000;

        attackTimer = attackTimerMax;
        bossHP = HandleHP;
        Wrapper.NetworkingPlugin_FuncBossHP(bossHP);
	}
	
	// Update is called once per frame
	void Update () {

        //rotate slowly on multiple axes
        //this.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.right * Time.deltaTime * rotSpeed);
        this.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

        attackTimer -= Time.deltaTime;

        if (!Wrapper.NetworkingPlugin_IsServer())
            return;

        if (attackTimer <= 0)
        {
            //Instantiate(attack, transform.position, Quaternion.identity);
            whoops = ReturnUseable();
            if (whoops)
                whoops.SendMessage("SetVelocity", transform.position);
            attackTimer = attackTimerMax;
        }

    }


    void damageBoss(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Debug.Log("GAME OVER WOW NICE");
        }
    }

    GameObject ReturnUseable()
    {
        foreach(Attack attacks in attackList)
        {
            if (!attacks.ReturnUsed())
            {
                return attacks.gameObject;
            }
        }
        return whoops;
    }

    public void HandleHP(ulong time, int numShout)
    {
        SubtractFromHP();
    }

    void SubtractFromHP()
    {
        hp -= 1;

        Wrapper.NetworkingPlugin_SendBossHP((int)hp);
    }
}
