using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator animator;
    protected Vector2Int posXY;
    protected GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosXY(Vector2Int newPos)
    {
         posXY = newPos;
    }
    public Vector2 GetPosXY()
    {
        return posXY;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
