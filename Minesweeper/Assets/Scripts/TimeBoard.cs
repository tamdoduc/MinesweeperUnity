using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBoard : NumberTwoUnit
{
    // Start is called before the first frame update
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
                else
                    value--;
                Debug.Log("Value: "+value);
            }
        }
    }
    public void Begin(int value)
    {
        this.value = value;
        SetNumber(value);
        isBegin = true;
    }
    public void Stop()
    {
        isBegin = false;
        Debug.Log("Value when Stop: "+value);
        Debug.Log("Value when Stop: "+GetValue());
    }
}
