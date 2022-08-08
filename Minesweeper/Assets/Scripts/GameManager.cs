using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 startPos;
    private int height;
    private int width;

    public GameObject openNumber;
    
    void Start()
    {
        startPos = new Vector3(-4,3);

        height = 20;
        width = 20;
        InitMap();
    }

    void InitMap()
    {
        Vector3 pos = startPos;
        float h = 0.4f;
        float w = 0.4f;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pos = new Vector3(pos.x + w, pos.y, 0);
                GameObject.Instantiate(openNumber, pos, Quaternion.identity);
            }
            pos = new Vector3(pos.x - w*width, pos.y-h, 0);
        }
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
}
