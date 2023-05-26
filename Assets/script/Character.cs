using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Character : MonoBehaviour
{

    public float movespeed=2f;
    public float jumpPower= 300f;
    public GameObject gameManager;
    
    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.right*movespeed*Time.deltaTime); 
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.up*jumpPower);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "win")
        {
            gameManager.GetComponent<GameManager>().Win();
        }
        else if (other.transform.name=="lose")
        {
            gameManager.GetComponent<GameManager>().Lose();
        }
    }
}
