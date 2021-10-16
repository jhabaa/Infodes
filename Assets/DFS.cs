using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : MonoBehaviour
{
  //  GameObject boule =  GameObject.FindGameObjectWithTag("DFS");
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.up * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "mur")
        {
            Debug.Log("Hit Something");
            this.transform.position = new Vector2(this.transform.position.x + 1 * Time.deltaTime, this.transform.position.y+1) ;
        }
    }
}
