using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 startPos;
    private int height;
    private int width;
    private float h = 0.4f;
    private float w = 0.4f;
    public bool isGameOver;

    private int numberOfBomb;
    private int countFlagSetted;
    private int countSquareOpened;

    public SquareClose squareClose;
    private SquareClose[,] arraySquareClose;
    public SquareOpen squareOpen;
    private SquareOpen[,] arraySquareOpen;


    [SerializeField]
    private TimeBoard timeBoard;
    [SerializeField]
    private NumberTwoUnit highScore;
    [SerializeField]
    private NumberTwoUnit countFlag;
    [SerializeField]
    private NumberTwoUnit maxBomb;
    [SerializeField]
    private Icon icon;


    private int[,] around = new int[,] {
            {0,1},
            {0,-1},
            {-1,0},
            {1,0},
            {-1,-1},
            {1,-1},
            {-1,1},
            {1,1}
        };

    void Start()
    {
        height = 20;
        width = 20;
        arraySquareClose = new SquareClose[width, height];
        arraySquareOpen = new SquareOpen[width, height];
        //PlayerPrefs.SetInt("highScore", 0);
        if (PlayerPrefs.GetInt("highScore") != 0)
            highScore.SetNumber(PlayerPrefs.GetInt("highScore"));
        Reset();
    }

    public void Reset()
    {
        timeBoard.Reset();
        countFlag.SetNumber(0);
        isGameOver = false;
        startPos = new Vector3(-4, 3);
        countSquareOpened = 0;
        numberOfBomb = 40;
        countFlagSetted = 0;
        maxBomb.SetNumber(numberOfBomb);



        InitMapSquare();
    }
    void InitMapSquare()
    {

        Vector3 pos = startPos;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pos = new Vector3(pos.x + w, pos.y, 0);

                if (arraySquareClose[i, j] != null)
                    Destroy(arraySquareClose[i, j].gameObject);
                if (arraySquareOpen[i, j] != null)
                    Destroy(arraySquareOpen[i, j].gameObject);

                SquareOpen tmpSquareOpen = Instantiate(squareOpen, pos, Quaternion.identity);
                tmpSquareOpen.SetPosXY(new Vector2Int(i, j));
                tmpSquareOpen.SetGameManager(this);
                arraySquareOpen[i, j] = tmpSquareOpen;

                SquareClose tmpSquareClose = Instantiate(squareClose, pos, Quaternion.identity);
                tmpSquareClose.SetPosXY(new Vector2Int(i, j));
                tmpSquareClose.SetGameManager(this);
                arraySquareClose[i, j] = tmpSquareClose;


            }
            pos = new Vector3(pos.x - w * width, pos.y - h, 0);
        }
    }
    void SetStateSquareOpen()
    {
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                if (arraySquareOpen[i, j].GetCountBomb() != -1)
                {
                    int countBomb = 0;
                    for (int k = 0; k < 8; k++)
                        if (i + around[k, 0] >= 0 && j + around[k, 1] >= 0 && i + around[k, 0] < height && j + around[k, 1] < width && arraySquareOpen[i + around[k, 0], j + around[k, 1]].GetCountBomb() == -1)
                            countBomb++;
                    arraySquareOpen[i, j].SetCountBomb(countBomb);
                }
    }
    void InitMapBomb(Vector2 posExceptBomb)
    {
        timeBoard.Begin(99);
        int countBomb = 0;
        while (countBomb < numberOfBomb)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            if (x != posExceptBomb.x && y != posExceptBomb.y&& arraySquareOpen[x,y].GetCountBomb()!=-1)
            {
                countBomb++;
                arraySquareOpen[x, y].SetCountBomb(-1);
                // Debug.Log(x + " " + y);
            }
        }
        Debug.Log(countBomb);
    }
    public void OpenAround(Vector2Int posSquare)
    {
        int i = posSquare.x;
        int j = posSquare.y;

        int countFlag = 0;
        for (int k = 0; k < 8; k++)
            if (i + around[k, 0] >= 0 && j + around[k, 1] >= 0 && i + around[k, 0] < height && j + around[k, 1] < width && arraySquareClose[i + around[k, 0], j + around[k, 1]].GetState() == 1)
                countFlag++;

        if (countFlag == arraySquareOpen[i, j].GetCountBomb())
        {
            for (int k = 0; k < 8; k++)
                if (i + around[k, 0] >= 0 && j + around[k, 1] >= 0 && i + around[k, 0] < height && j + around[k, 1] < width && arraySquareClose[i + around[k, 0], j + around[k, 1]].GetState() == 0)
                    OpenSquare(new Vector2Int(i + around[k, 0], j + around[k, 1]));
        }
    }
    public void OpenSquare(Vector2Int posSquare)
    {
        int i = posSquare.x;
        int j = posSquare.y;
        if (arraySquareOpen[i, j].GetStateOpened() || isGameOver)
            return;


        arraySquareOpen[i, j].Open();
        arraySquareClose[i, j].Open();
        if (arraySquareOpen[i, j].GetCountBomb() == -1)
        {
            arraySquareOpen[i, j].SetCountBomb(-3);
            GameOver();
        }

        countSquareOpened++;
        if (countSquareOpened == 1)
        {
            InitMapBomb(posSquare);
            SetStateSquareOpen();
        }
        if (countSquareOpened == height * width - numberOfBomb)
        {
            Win();
            return;
        }
       // Debug.Log(countSquareOpened+"/"+ (height * width - numberOfBomb));
        if (arraySquareOpen[posSquare.x, posSquare.y].GetCountBomb() == 0)
        {
            for (int k = 0; k < 8; k++)
                if (i + around[k, 0] >= 0 && j + around[k, 1] >= 0 && i + around[k, 0] < height && j + around[k, 1] < width)
                    OpenSquare(new Vector2Int(i + around[k, 0], j + around[k, 1]));
        }
    }


    void Win()
    {
        icon.Win();
        timeBoard.Stop();
        isGameOver = true;

        Debug.Log("Win" + PlayerPrefs.GetInt("highScore")+"/"+timeBoard.GetValue());
        if (timeBoard.GetValue()>PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore",timeBoard.GetValue());
            highScore.SetNumber(PlayerPrefs.GetInt("highScore"));
        }
    }
    void GameOver()
    {
        isGameOver = true;
        timeBoard.Stop();
        icon.GameOver();
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                if (arraySquareClose[i, j] != null)
                    arraySquareClose[i, j].Open();
                if (arraySquareClose[i, j].GetState() == 1 && arraySquareOpen[i, j].GetCountBomb() != -1)
                {
                    arraySquareOpen[i, j].SetCountBomb(-2);
                }
            }
    }
    public bool SetFlag()
    {
       if (!isGameOver && countFlagSetted<numberOfBomb)
        {
            countFlagSetted++;
            countFlag.Increase();
            return true;
        }
        return false;
    }
    public void UnSetFlag()
    {
        countFlagSetted--;
        countFlag.Decrease();
    }
    void Update()
    {

    }
}
