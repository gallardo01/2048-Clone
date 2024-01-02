using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] SpriteRenderer image;
    [SerializeField] TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initNumber(int number)
    {
        text.text = number.ToString();
        int num = (int) Mathf.Log(number, 2);
        if (num > 6)
        {
            num = 6;
        }
        image.sprite = Resources.Load<Sprite>("UI/" + num.ToString());
    }
}
