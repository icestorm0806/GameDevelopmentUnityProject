using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGooseBossScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene("GooseBoss");
            }
    }
}

/*
public Text pressF;
pressF.text = "";
Time.timeScale = 0f;
*/