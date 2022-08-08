using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareOpen : Square
{
    // Start is called before the first frame update
    private int countBomb;
    private bool opened;
    void Start()
    {
        animator = GetComponent<Animator>();
        opened = false;
        SetCountBomb(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCountBomb(int countBomb)
    {
        this.countBomb = countBomb;
        animator.SetInteger("countBomb", countBomb);
    }
    public int GetCountBomb()
    {
        return countBomb;
    }
    public void Open()
    {
        opened = true;
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }
    public bool GetStateOpened()
    {
        return opened;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(2))
        {
            gameManager.OpenAround(posXY);
        }
    }

}
