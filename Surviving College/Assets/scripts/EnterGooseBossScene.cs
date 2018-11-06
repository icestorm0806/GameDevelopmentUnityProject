using System.Collections;
using System.Collections.Generic;
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
        if (other.gameObject.CompareTag("GooseDoor"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene("GooseBoss");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
