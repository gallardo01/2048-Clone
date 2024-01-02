using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] Transform[] boardPoitns;
    [SerializeField] GameObject numberCell;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] Button cancel;
    [SerializeField] Button purchase;
    [SerializeField] GameObject panelDead;

    private int[,] numberMap = { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
    private List<GameObject> numberCells = new List<GameObject>();
    private bool isSwipe = true;
    private int points = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 16; i++)
        {
            GameObject cell = Instantiate(numberCell, boardPoitns[i].position, Quaternion.identity);
            numberCells.Add(cell);
        }
        createNewCell();
        cancel.onClick.AddListener(() => backToMain());
        purchase.onClick.AddListener(() => verifyPurchased());
    }

    private void backToMain()
    {
        Debug.Log("AAA");
        SceneManager.LoadScene(0);
    }

    private void verifyPurchased()
    {
        // Verify purchase here
        bool isPurchaseSuccess = true;
        if (isPurchaseSuccess)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    numberMap[i, j] = 0;
                }
            }
        }
        createNewCell();
        initCell();
        panelDead.SetActive(false);
    }

    public void swipe(int direction)
    {
        if (direction == 0) // left
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (numberMap[j, i] == 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numberMap[k, i] > 0)
                            {
                                numberMap[j, i] = numberMap[k, i];
                                numberMap[k, i] = 0;
                                break;
                            }
                        }
                    }
                    else if (numberMap[j, i] > 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numberMap[k, i] > 0)
                            {
                                if (numberMap[k, i] == numberMap[j, i])
                                {
                                    numberMap[j, i] = 2 * numberMap[k, i];
                                    numberMap[k, i] = 0;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        else if (direction == 1) // right
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (numberMap[j, i] == 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numberMap[k, i] > 0)
                            {
                                numberMap[j, i] = numberMap[k, i];
                                numberMap[k, i] = 0;
                                break;
                            }
                        }
                    }
                    else if (numberMap[j, i] > 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numberMap[k, i] > 0)
                            {
                                if (numberMap[k, i] == numberMap[j, i])
                                {
                                    numberMap[j, i] = 2 * numberMap[k, i];
                                    numberMap[k, i] = 0;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        else if (direction == 2) // up
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (numberMap[i, j] == 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numberMap[i, k] > 0)
                            {
                                numberMap[i, j] = numberMap[i, k];
                                numberMap[i, k] = 0;
                                break;
                            }
                        }
                    }
                    else if (numberMap[i, j] > 0)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (numberMap[i, k] > 0)
                            {
                                if (numberMap[i, k] == numberMap[i, j])
                                {
                                    numberMap[i, j] = 2 * numberMap[i, k];
                                    numberMap[i, k] = 0;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        else if (direction == 3) // up
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (numberMap[i, j] == 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numberMap[i, k] > 0)
                            {
                                numberMap[i, j] = numberMap[i, k];
                                numberMap[i, k] = 0;
                                break;
                            }
                        }
                    }
                    else if (numberMap[i, j] > 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (numberMap[i, k] > 0)
                            {
                                if (numberMap[i, k] == numberMap[i, j])
                                {
                                    numberMap[i, j] = 2 * numberMap[i, k];
                                    numberMap[i, k] = 0;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        initCell();
        StartCoroutine(startNewCell());
    }

    IEnumerator startNewCell()
    {
        yield return new WaitForSeconds(0.5f);
        createNewCell();
    }
    private void createNewCell()
    {
        addNumberToCell();
        initCell();
    }

    private void addNumberToCell()
    {
        List<int> randomNumber = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (numberMap[i,j] == 0)
                {
                    randomNumber.Add(i * 4 + j);
                }
            }
        }

        if (randomNumber.Count > 0)
        {
            int rand = randomNumber[Random.Range(0, randomNumber.Count)];
            numberMap[rand/4, rand % 4] = 2;
            points += 2;
            if (Random.Range(0, 3) == 1)
            {
                numberMap[rand / 4, rand % 4] = 4;
                points += 2;
            }
            pointsText.text = points.ToString();
        }
        else
        {
            // end game
            panelDead.SetActive(true);
        }
    }
    private void initCell()
    {
        for (int i = 0; i < 16; i++)
        {
            numberCells[i].GetComponent<Cell>().initNumber(numberMap[i / 4, i % 4]);
        }
    }
    
}
