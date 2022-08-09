using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBoard : NumberTwoUnit
{
    // Start is called before the first frame update
    private int value;
    private bool isBegin;

    private float time;
    void Start()
    {
        //Begin(11);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBegin)
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                time = time - 1f;
                if (!Decrease())
                    isBegin = false;
            }
        }
    }
    public void Begin(int value)
    {
        this.value = value;
        SetNumber(value);
        isBegin = true;
    }
}
