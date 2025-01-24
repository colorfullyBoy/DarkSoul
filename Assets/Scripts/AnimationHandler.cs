using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    private int vertical;
    private int horizontal;
    public bool canRotate;

    public void init(){
        this.anim = this.GetComponentInChildren<Animator>();
        this.vertical = Animator.StringToHash("Vertical");
        this.horizontal = Animator.StringToHash("Horizontal");
    }

    public void updateAnimatorValues(float verticalMove, float horizonMove){
        float v = 0;
        if (verticalMove > 0 && verticalMove <= 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMove > 0.55f){
            v = 1;
        }
        else if (verticalMove < 0 && verticalMove >= -0.55f){
            v = -0.5f;
        }
        else if (verticalMove < -0.55){
            v = -1;
        }
        else {
            v = 0;
        }

        float h = 0;
        if (horizonMove > 0 && horizonMove <= 0.55f)
        {
            h = 0.5f;
        }
        else if (horizonMove > 0.55f){
            h = 1;
        }
        else if (horizonMove < 0 && horizonMove >= -0.55f){
            h = -0.5f;
        }
        else if (horizonMove < -0.55){
            h = -1;
        }
        else {
            h = 0;
        }
        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void setRotateEnabled(Boolean isEnabled){
        this.canRotate = isEnabled;
    }
}
