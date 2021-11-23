using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.DefaultControls;
using Resources = UnityEngine.Resources;

public class ClickSelect : MonoBehaviour
{
    Material yellow;
    Material tempMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Material yellow = Resources.Load("Yellow", typeof(Material)) as Material;
        if (this.tag == "startpoint")
        {
           var mesh = this.GetComponent<MeshRenderer>();
            mesh.material = yellow;
        }
    }

    private void OnMouseDown()
    {
        var heuristicInstance = new grid();
        Debug.Log("Selection Depart");
        GameObject gameObject = this.GetComponent<GameObject>();
        var mesh = this.GetComponent<MeshRenderer>();
        mesh.material = yellow;
        mesh.tag = "startpoint";
        GameObject goal = GameObject.FindGameObjectWithTag("goal");

    }
    private void OnMouseOver()
    {
        tempMaterial = this.GetComponent<MeshRenderer>().material;
        Material white = Resources.Load("white", typeof(Material)) as Material;
        var mesh = this.GetComponent<MeshRenderer>();
        mesh.material = white;
        if(Input.GetMouseButtonDown(0) == true)
        {
            Debug.Log("Clic droit");
        }
    }
    private void OnMouseExit()
    {
        Material green = Resources.Load("green", typeof(Material)) as Material;
        var mesh = this.GetComponent<MeshRenderer>();
        mesh.material = green;
    }
}
