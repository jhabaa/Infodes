using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.DefaultControls;
using Resources = UnityEngine.Resources;

public class ClickSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        var heuristicInstance = new grid();
        Material yellow = Resources.Load("yellow", typeof(Material)) as Material;
        Debug.Log("Selection Depart");
        GameObject gameObject = this.GetComponent<GameObject>();
        var mesh = this.GetComponent<MeshRenderer>();
        mesh.material = yellow;
        mesh.tag = "startpoint";
        GameObject goal = GameObject.FindGameObjectWithTag("goal");

    }
}
