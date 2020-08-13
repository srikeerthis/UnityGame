using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private PlayerController player;
    private Animator byStander;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        byStander = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        CycleDone();
        onDeath();
    }

    public void CycleDone()
    {
        if(player.transform.position.z >= player.zBound)
        {
            byStander.SetInteger("Animation_int",6);
        }
        else
        {
            byStander.SetInteger("Animation_int",1);
        }
    }

    public void onDeath()
    {
        if(player.isDead)
        {
            byStander.SetInteger("Animation_int",0);
            byStander.SetFloat("Speed_f",1.75f);
        }
    }
}
