using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareClose : Square
{
    // Start is called before the first frame update
    [SerializeField]
    private int state;
    void Start()
    {
        state = 0;
        animator = GetComponent<Animator>();
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void ChangeState()
    {
        if (state == 0 && gameManager.SetFlag())
            state = 1;
        else
        if (state == 1)
        {
            gameManager.UnSetFlag();
            state = 0;
        }

        animator.SetInteger("state", state);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == 0)
            {
                Debug.Log("This left");
                gameManager.OpenSquare(posXY);
                Open();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("This Right");
            ChangeState();
        }
    }
    public void Open()
    {
        Destroy(gameObject);
    }
    public int GetState()
    {
        return state;
    }
}
