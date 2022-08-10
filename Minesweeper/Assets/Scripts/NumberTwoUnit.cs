using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTwoUnit : MonoBehaviour
{
    [SerializeField]
    protected Number number1;
    [SerializeField]
    protected Number number2;
    protected int value;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Decrease()
    {
        if (number1.GetState() == 0 && number2.GetState() == 0)
        {
            number1.SetState(-1);
            number2.SetState(-1);
            return false;
        }
        if (number2.GetState() == 0)
            number1.Decrease();
        number2.Decrease();

        return true;
    }
    public void Increase()
    {
        if (number1.GetState() == 9 && number2.GetState() == 9)
        {
            return;
        }
        if (number2.GetState() == 9)
            number1.Increase();
        number2.Increase();
    }
    public void SetNumber(int number1,int number2)
    {
        this.number1.SetState(number1);
        this.number2.SetState(number2);
        this.value = number1 * 10 + number2;
    }
    public void SetNumber(int value)
    {
        this.number1.SetState(value/10);
        this.number2.SetState(value%10);
        this.value = value;
    }
    public void Reset()
    {
        value = -1;
        this.number1.SetState(-1);
        this.number2.SetState(-1);
    }
    public int GetValue()
    {
        return value;
    }
}
