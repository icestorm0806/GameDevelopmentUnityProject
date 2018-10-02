using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Power_Up") {
            Destroy(col.gameObject);
        }
    }
}