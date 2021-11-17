using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class grid : MonoBehaviour
{   public Button button;
    public Button startbutton;
    public Button cleanButton;
    public Button ndsButton, beamButton;
    public Button ufcButton, greddyButton;
    public Sprite sprite;
    public int[,] Grid;
    private float speed = 2.0f;
    public GameObject[] sequence;
    public List<int[,]> NameTab;
    public List<GameObject> piles,paths;
    private List<string> tags, liste;
    private int a , b, goalPointX, goalPointY, finalCost=200;
    public int AccCost = 0;
    public int AccCost1 = 0;
    public GameObject startpoint, goalPoint, rat;
    bool trouvé = false;
    public int Vertical, Horizontal, Columns, Rows;
    public Material mate;
    public Sprite circle;
    Material yellow;
    Material red;
    Material green;
    Material white ;
    Material plateforme;
   
    // Start is called before the first frame update
    void Start()
    {

        yellow = Resources.Load("yellow", typeof(Material)) as Material;
        red = Resources.Load("red", typeof(Material)) as Material;
        green = Resources.Load("green", typeof(Material)) as Material;
        white = Resources.Load("white", typeof(Material)) as Material;
        plateforme = Resources.Load("plateforme", typeof(Material)) as Material;
        rat = Resources.Load("Rat", typeof(GameObject)) as GameObject;

        Button beamBtn = beamButton.GetComponent<Button>();
        beamBtn.onClick.AddListener(BeamSearch);
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(newDFS);
        Button startbtn = startbutton.GetComponent<Button>();
        startbtn.onClick.AddListener(startPoint);
        Button cleanBtn = cleanButton.GetComponent<Button>();
        cleanBtn.onClick.AddListener(Clean);
        Button ndsBtn = ndsButton.GetComponent<Button>();
        ndsBtn.onClick.AddListener(NonDeterministicSearch);
        Button greedyBtn = greddyButton.GetComponent<Button>();
        greedyBtn.onClick.AddListener(greedySearch);
        Button ufcBtn = ufcButton.GetComponent<Button>();
        ufcBtn.onClick.AddListener(UpgradedUniformCost);

        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * (Screen.width / Screen.height);
        Grid = new int[15, 15];
        for (int i=0; i< 15; i++)
        {
            for (int j=0; j<15; j++)
            {
                SpawnTile(i, j);
            }
        }
        // Selectionnons un objet qui marquera l'objectif. Il sera rouge et deviendra vert une fois atteint
        a = Random.Range(0,15);
        b = Random.Range(13, 15);
        GameObject goal = GameObject.Find(a.ToString() + "," + b.ToString() + "," + string.Empty+"#"+string.Empty);
        goalPoint = goal;
        goal.GetComponent<MeshRenderer>().material = red;
        goal.tag = "goal";
        goalPointX = a;
        goalPointY = b;
    }
    /*
     * $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  GREEDY SEARCH $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
     * $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
     * $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
     */

    private void greedySearch()
    {
        GameObject up, left, right, down;
        /*
         * remove the first path from the QUEUE (FILE); create new paths (to all children);
         * add the new paths to front of QUEUE (FILE);
         */
        do
        {
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);

                }
                else
                if (b == 14)
                {
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, right);
                    piles.Insert(1, down);

                }
                else if (b != 0 && b != 14)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }
            }
            else
            if (a == 14)
            {
                if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, down);
                }
                else
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, down);
                }
            }
            else if (a != 0 && a != 14)
            {
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                }
                else if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, right);
                    piles.Insert(2, down);
                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);

                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                    piles.Insert(3, down);
                }
            }
            greedyCursor();
        } while (piles.Count != 0 && trouvé == false);

        if (trouvé == true)
        {
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
    }

    private void moveRat(List<GameObject> list)
    {
        float step = speed * Time.deltaTime;
        
        foreach(GameObject gameObject in list)
        {
            GameObject animal = GameObject.FindGameObjectWithTag("Rat");
            animal.transform.position = Vector3.MoveTowards(animal.transform.position,gameObject.transform.position, step);
        }
        
    }

    private void AddHeuristics(List<GameObject> gameObjects)
    {
        foreach(GameObject gameObject in gameObjects)
        {
            string[] part = gameObject.name.Split('#');
            string[] split = part[0].Split(',');
            if (split[2].Length > 0)
            {
                print("Split est : " + gameObject.name);
                gameObject.name = (split[0] + "," + split[1] + "," + split[2].Remove(0)+"#"+string.Empty);
            }
            gameObject.name = split[0] + "," + split[1] + "," + HeuristicCost(gameObject,goalPoint) + "#" + string.Empty;
        }
    }


    /*$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  GREEDY CURSOR  $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
    private void greedyCursor()
    {

        piles.RemoveAll(GameObject => GameObject == null);
        if (piles[0].tag == "check")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "check");
        }
        if (piles[0].tag == "noire")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "noire");
        }
        AddHeuristics(piles);
        InsertionSortHeuristic(piles);
        piles.RemoveAll(GameObject => GameObject == null);
        if (piles[0].tag == "goal")
        {
            print("Fin de Partie : ");
            trouvé = true;
            piles[0].GetComponent<MeshRenderer>().material = green;
        }

        if (piles[0].tag == "blanche")
        {
            piles[0].tag = "check";
            piles[0].GetComponent<MeshRenderer>().material = yellow;
            paths.Add(piles[0]);
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
        }
        
    }

    private void startPoint()
    {
        startpoint = GameObject.FindGameObjectWithTag("startpoint");
        piles.Add(startpoint);
        string[] split = startpoint.name.Split(',');
        a = int.Parse(split[0]);
        b = int.Parse(split[1]);
        startpoint.name += HeuristicCost(startpoint, goalPoint);
        GameObject newrat = GameObject.Instantiate(rat);
        newrat.tag = "Rat";
        newrat.transform.localScale = (new Vector3(0.4f,1f,0.31f));
        newrat.transform.Rotate(new Vector3(-128f,0f,0f));
        newrat.transform.position = startpoint.transform.position;
        Maze();
    }

    private void Clean()
    {
        
        
    }
/*
* Cursor for DFS and NDS algorithms. 
*/
    public void dfsAndNdsCursor()
    {
        /*
         * reject the new paths with loops;
         */
        piles.RemoveAll(GameObject => GameObject == null);
        // La suppression provoque le non retour. à régler.
        // La zone arrière n'est pas définie. On doit faire marche arrière si on ne peut plus bouger. 
        // suppression des boucles
        if(piles[0].tag == "check")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "check");
        }
        //Lets delete the first GameObject of the file if tag is black
        if (piles[0].tag == "noire")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "noire");
            
        }
        if(piles[0].tag == "goal") {
            print("Fin de Partie : ");
            trouvé = true;
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
        if(piles[0].tag == "blanche")
        {
            piles[0].tag = "check";
            piles[0].GetComponent<MeshRenderer>().material = yellow;
            paths.Add(piles[0]);
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles[0].name = split[0] + "," + split[1] + "," + string.Empty + "#" + string.Empty;
        }
        
    }

    /*
* DFS algorithm part
*/
    public void newDFS()
    {
        GameObject up, left, right, down;

        /*
         * remove the first path from the QUEUE (FILE); create new paths (to all children);
         * add the new paths to front of QUEUE (FILE);
         */
        do
        {
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {
                    
                    up = GameObject.Find(Sum(a,b,0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a,b,2)+","+ string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);

                }
                else
                if (b == 14)
                {
                    right = GameObject.Find(Sum(a,b,2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a,b,3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, right);
                    piles.Insert(1, down);

                }
                else if (b != 0 && b != 14)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a,b,3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }

            }
            else
            if (a == 14)
            {
                if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, down);

                }
                else
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, down);

                }
            }
            else if (a != 0 && a != 14)
            {
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);

                }
                else if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a,b,1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a,b,0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a,b,2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                    piles.Insert(3, down);

                }

            }
            dfsAndNdsCursor();
        } while (piles.Count != 0 && trouvé == false);

        if(trouvé == true)
        {
            piles[0].GetComponent<MeshRenderer>().material = green;
            
        } 
    }
    //Fonctions linked to NDS to place objects at random place (surcharged)
/*
 * NDS Algorithm Part, with a surcharged function to place objects in file and algorithm itself
 */
    public void NdsPlacement(GameObject x, GameObject y)
    {
        int place = Random.Range(0, piles.Count);
        piles.Insert(place, x);
        piles.Insert(place + 1, y);
    }
    public void NdsPlacement(GameObject x, GameObject y,GameObject w)
    {
        int place = Random.Range(0, piles.Count);
        piles.Insert(place, x);
        piles.Insert(place + 1, y);
        piles.Insert(place + 2, w);
    }
    public void NdsPlacement(GameObject x, GameObject y,GameObject w, GameObject z)
    {
        int place = Random.Range(0, piles.Count);
        piles.Insert(place, x);
        piles.Insert(place + 1, y);
        piles.Insert(place + 2, w);
        piles.Insert(place + 3, z);
    }

    public void NonDeterministicSearch()
    {
        GameObject up, left, right, down;
        /*
         * remove the first path from the QUEUE (FILE); create new paths (to all children);
         * add the new paths to a random place in QUEUE(FILE);
         */
        do
        {
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, right);

                }
                else
                if (b == 14)
                {
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(right, down);
                }
                else if (b != 0 && b != 14)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, right, down);
                }
            }
            else
            if (a == 14)
            {
                if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(left, down);
                }
                else
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, left);
                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, left, down);
                }
            }
            else if (a != 0 && a != 14)
            {
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, left, right);
                }
                else if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(left, right, down);
                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    NdsPlacement(up, left, right, down);
                }

            }
            dfsAndNdsCursor();
        } while (piles.Count != 0 && trouvé == false);

        if (trouvé == true)
        {
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
    }

    /*
     * ***************************************** BEAM SEARCH PART *****************************************
     * 
     */
    public void BeamSearch()
    {
        GameObject up, left, right, down;

        /*
         * remove the first path from the QUEUE (FILE); create new paths (to all children);
         * add the new paths to front of QUEUE (FILE);
         */
        do
        {
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {

                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(right);

                }
                else
                if (b == 14)
                {
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(right);
                    piles.Add(down);

                }
                else if (b != 0 && b != 14)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(right);
                    piles.Add(down);

                }

            }
            else
            if (a == 14)
            {
                if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(left);
                    piles.Add(down);

                }
                else
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(left);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(left);
                    piles.Add(down);

                }
            }
            else if (a != 0 && a != 14)
            {
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(left);
                    piles.Add(right);

                }
                else if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(left);
                    piles.Add(right);
                    piles.Add(down);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Add(up);
                    piles.Add(left);
                    piles.Add(right);
                    piles.Add(down);

                }

            }
            BeamCursor();
        } while (piles.Count != 0 && trouvé == false);

        if (trouvé == true)
        {
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
    }
    public void BeamCursor()
    {
        piles.RemoveAll(GameObject => GameObject == null);
        if (piles[0].tag == "check")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "check");
        }
        if (piles[0].tag == "noire")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "noire");

        }
        if (piles[0].tag == "goal")
        {
            print("Fin de Partie : ");
            trouvé = true;
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
        if (piles[0].tag == "blanche")
        {
            piles[0].tag = "check";
            piles[0].GetComponent<MeshRenderer>().material = yellow;
            paths.Add(piles[0]);
            string[] split = piles[0].name.Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles[0].name = split[0] + "," + split[1] + "," + string.Empty + "#" + string.Empty;
        }
    }
    /*
     * ****************************************** Heuristic Part ********************************************************
     */
    /*
     * ****************************************** Optimal Part ********************************************************
     *************************  EstimatedExtendedUniformCost+Branch&Bound+Suppression=A*  *******************
     */
    // Continue UniformCost until the minimun node cost is greater than accumulate goal Cost

    public void UpgradedUniformCost()
    {
      
        do
        {
            int finalCostInt = Cost(piles);
            finalCost = finalCostInt;
            GameObject up, left, right, down;
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);

                }
                else
                if (b == 14)
                {
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, right);
                    piles.Insert(1, down);

                }
                else if (b != 0 && b != 14)
                {
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }

            }
            else
            if (a == 14)
            {
                if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, down);

                }
                else
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, down);

                }
            }
            else if (a != 0 && a != 14)
            {
                if (b == 0)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);

                }
                else if (b == 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, left);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }
                else if (b != 0 && b != 14)
                {
                    left = GameObject.Find(Sum(a, b, 1) + "," + string.Empty + "#" + string.Empty);
                    up = GameObject.Find(Sum(a, b, 0) + "," + string.Empty + "#" + string.Empty);
                    right = GameObject.Find(Sum(a, b, 2) + "," + string.Empty + "#" + string.Empty);
                    down = GameObject.Find(Sum(a, b, 3) + "," + string.Empty + "#" + string.Empty);
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                    piles.Insert(3, down);
                }
            }
            UniformSort(piles);
        } while (piles.Count != 0 && Cost(piles)>finalCost);
    }

    private int AccumulateCostPoint(GameObject point)
    {
        int x = 0;
        x = HeuristicCost(point, goalPoint) + CostByTag(point);
        return x;
    }

    //Function F(path) where f(path) = cost(path) + h(endpoint)
    private int F(List<GameObject> path)
    {
        int f = 0;
        f = Cost(path) + HeuristicCost(path[0], goalPoint);
        return f;
    }
    private int Cost(List<GameObject> path)
    {
        int cost = 0;
        foreach (GameObject gameObject in path)
        {
            cost += CostByTag(gameObject);

        }
        return cost;
    }

    private int CostByTag(GameObject gameObject)
    {
        int costBytag = 0;
        if (gameObject.tag == "blanche")
        {
            costBytag = 4;
        }
        return costBytag;
    }
    private void AddF(List<GameObject> piles)
    {
        foreach (GameObject gameObject in piles)
        {
            string[] part = gameObject.name.Split('#');
            string[] split = part[0].Split(',');
            if (part[1].Length != 0)
            {
                gameObject.name = (split[0] + "," + split[1] + "," + split[2]+"#"+part[1].Remove(0));
            }
            int f = int.Parse(split[2]) + Cost(piles);
            gameObject.name = (split[0] + "," + split[1] + "," + split[2] + "#" + f.ToString());
        }
    }
    private void AddHSeparately(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            string[] part = gameObject.name.Split('#');
            string[] split = part[0].Split(',');
           // string[] 
            if (split[2].Length > 0)
            {
                gameObject.name = (split[0] + "," + split[1] + "," + split[2].Remove(0) + "#" + string.Empty);
            }
            gameObject.name = (split[0] + "," + split[1] + "," + HeuristicCost(gameObject, goalPoint) + "#" + string.Empty);
            // gameObject.name += HeuristicCost(gameObject, goalPoint);
        }
    }
    //Here we can sort our UniformCost and set cursor
    private void UniformSort(List<GameObject> piles)
    {
        
        piles.RemoveAll(GameObject => GameObject == null);
        //Reject new paths with loops 
        if (piles[0].tag == "check")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "check");
        }
        //Lets delete the first GameObject of the file if tag is black
        if (piles[0].tag == "noire")
        {
            do
            {
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "noire");

        }
        //Add new paths and sort the entire Queue by f = cost+h
        AddHSeparately(piles);
        AddF(piles);
        InsertionSort(piles);
        if (piles[0].tag == "goal")
        {
            print("Fin de Partie : ");
            trouvé = true;
            piles[0].GetComponent<MeshRenderer>().material = green;
        }
        if (piles[0].tag != "check")
        {
            paths.Insert(0,piles[0]);
            piles[0].tag = "check";
            piles[0].GetComponent<MeshRenderer>().material = yellow;
            string[] part = piles[0].name.Split('#');
            string[] split = part[0].Split(',');
            a = int.Parse(split[0]);
            b = int.Parse(split[1]);

        }
    }
    //Cost Estimation by bird fly
    internal int HeuristicCost(GameObject gameObject, GameObject goal)
    {
        int heuristic;
        string[] split = gameObject.name.Split(',');
        string[] splitGoal = goal.name.Split(',');
        a = int.Parse(split[0]);
        b = int.Parse(split[1]);
        int x = int.Parse(splitGoal[0]);
        int y = int.Parse(splitGoal[1]);
        //in the case where Goal is always up. 
        if (x > a)
        {
            heuristic = (x - a) + (y - b);

        }
        else
            heuristic = (a - x) + (y - b);

        return heuristic;
    }

    /*
     * SUM algorithm for all others
     */
    private string Sum(int a, int b, int r)
    {
        int x, y;
        string s;
        if (r == 0)
        {
            x = a;
            y = b + 1;
            s = x.ToString() + "," + y.ToString();
            return s;
        }
        else
            if (r == 1)
        {
            x = a-1;
            y = b;
            s = x.ToString() + "," + y.ToString();
            return s;
        }else
            if (r == 2)
        {
            x = a+1;
            y = b;
            s = x.ToString() + "," + y.ToString();
            return s;
        }
        else if(r == 3)
        {
            x = a;
            y = b-1;
            s = x.ToString() + "," + y.ToString();
            return s;
        }else
            return null;  
    }
    private void Swap(GameObject x, GameObject y)
    {
        GameObject temp;
        temp = x;
        x = y;
        y = temp;
    }
    public static void InsertionSort(List<GameObject> input)
    {

        for (var i = 0; i < input.Count; i++)
        {
            
            var min = i;
            string[] split = input[min].name.Split('#');
            for (var j = i + 1; j < input.Count; j++)
            {
                string[] split1 = input[j].name.Split('#');
                if (uint.Parse(split[1]) > uint.Parse(split1[1]))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                var lowerValue = input[min];
                input[min] = input[i];
                input[i] = lowerValue;
            }
        }
    }
    public static void InsertionSortHeuristic(List<GameObject> input)
    {
        for (var i = 0; i < input.Count; i++)
        {

            var min = i;
            string[] part = input[min].name.Split('#');
            string[] split = part[0].Split(',');
            for (var j = i + 1; j < input.Count; j++)
            {
                string[] part1 = input[j].name.Split('#');
                string[] split1 = part1[0].Split(',');
                if (uint.Parse(split[2]) > uint.Parse(split1[2]))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                var lowerValue = input[min];
                input[min] = input[i];
                input[i] = lowerValue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
         {
             GameObject s = FindObjectOfType<GameObject>();
             Debug.Log("Click" + s.name);

         }*/
       if(button.enabled == false)
        {
            StartCoroutine("newDFS");
        }
    }
/*
 * Here we populate our "Grid" with gameobjects and Sprites in our GameObjects.
 * One day we'll be back to make 3D spheres instead
 */
    private void SpawnTile(int x, int y)
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        g.name = x.ToString() + "," + y.ToString()+","+ string.Empty + "#"+string.Empty;
        var s = g.GetComponent<MeshRenderer>();
        s.material = white;
        g.AddComponent<ClickSelect>();
        g.transform.position = new Vector2(x - 1f, y - 1f);
        g.tag = "blanche";
    }
/*
 * Let's create an amazing maze in the field
 */
    public void Maze()
    {
        for (int i = 0; i<35; i++)
        {
            a = Random.Range(0, 15);
            b = Random.Range(3, 13);
            GameObject block = GameObject.Find(a.ToString() + "," + b.ToString() + "," + string.Empty + "#" + string.Empty);
            block.tag = "noire";
            block.GetComponent<MeshRenderer>().material = plateforme;
        }
    }    
}
