using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGooseBossScene : MonoBehaviour
{
    public Text pressF;
    
    // Use this for initialization
    void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GooseDoor"))
        {
            pressF.text = "Press f to enter";
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene("GooseBoss");
            }
        }
        else
        {
            pressF.text = "";
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
