using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private int state;

    void Start()
    {
        state = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(int state)
    {
        this.state = state;
        SetAnimation();
    }
    public int GetState()
    {
        return state;
    }
    public void Increase()
    {
        state++;
        state %= 10;
        SetAnimation();
    }
    public void Decrease()
    {
        state--;
        if (state == -1)
            state = 9;
        SetAnimation();
    }
    public void SetAnimation()
    {
        animator.SetInteger("state", state);
    }
}
