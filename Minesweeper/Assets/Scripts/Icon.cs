using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        if (gameManager != null)
            Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left");
            StartGame();
        }
    }
    public void StartGame()
    {
        animator.SetInteger("state", 0);
        gameManager.Reset();
    }
    public void Win()
    {
        animator.SetInteger("state", 1);
    }
    public void GameOver()
    {
        animator.SetInteger("state", 2);
    }
}
