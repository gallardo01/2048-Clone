using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField] Button loadGame;
    // Start is called before the first frame update
    void Start()
    {
        loadGame.onClick.AddListener(() => loadGameFunc());
    }

    private void loadGameFunc()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
