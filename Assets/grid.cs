using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grid : MonoBehaviour
{   public Button button;
    public Button startbutton;
    public Sprite sprite;
    public int[,] Grid;
    public GameObject[] sequence;
    public List<int[,]> NameTab;
    public List<GameObject> piles;
    public int a , b ;
    public GameObject startpoint;
    bool trouvé = false;
    int Vertical, Horizontal, Columns, Rows;
    public Material mate;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(newDFS);
        Button startbtn = startbutton.GetComponent<Button>();
        startbtn.onClick.AddListener(startPoint);

        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * (Screen.width / Screen.height);
        Columns = Horizontal * 3;
        Rows = Vertical * 3;
        Grid = new int[Columns, Rows];
        for (int i=0; i< Columns; i++)
        {
            for (int j=0; j<Rows; j++)
            {
                Grid[i, j] = Random.Range(0, 10);
                SpawnTile(i, j, Grid[i,j]);
            }
        }
        // Selectionnons un objet qui marquera l'objectif.
        GameObject goal = GameObject.Find("(2, 4)");
        var r = goal.GetComponent<SpriteRenderer>();
        r.color = Color.red;
        goal.tag = "goal";
    }

    private void startPoint()
    {
        
        a = Random.Range(0, 9);
        b = Random.Range(0, 9);
        startpoint = GameObject.Find((a,b).ToString());
        var r = startpoint.GetComponent<SpriteRenderer>();
        r.color = Color.yellow;
        startpoint.tag = "StartPoint";
        piles.Add(startpoint);
        Debug.Log(piles.Count);
        print("Test : " + ((int)char.GetNumericValue(piles[0].name[1]) + (int)1));
        print("Test 2 : " + (a +b));
    }
    public void file()
    {

    }

    private void clean()
    {
        GameObject S = GameObject.Find("StartPoint");
        
    }
    public void setCursor()
    {
        /*
         * reject the new paths with loops;
         */
        piles.RemoveAll(GameObject => GameObject == null);
        print("Valeurs nulles supprimées");
        // La suppression provoque le non retour. à régler.
        // La zone arrière n'est pas définie. On doit faire marche arrière si on ne peut plus bouger. 
        // suppression des boucles
        if(piles[0].tag == "check")
        {
            do
            {
                Debug.Log("suppression boucle");
                piles.RemoveAt(0);
                piles.RemoveAll(GameObject => GameObject == null);
            } while (piles[0].tag == "check");
        }
        
        if(piles[0].tag == "goal") {
            print("Fin de Partie : ");
            trouvé = true;
            SpriteRenderer FinalPoint = piles[0].GetComponent<SpriteRenderer>();
            FinalPoint.color = Color.green;
        }
        if(piles[0].tag != "check")
        {
            piles[0].tag = "check";
            SpriteRenderer cursor = piles[0].GetComponent<SpriteRenderer>();
            cursor.color = Color.grey;
            print("Nouveau pas");
            a = (int)char.GetNumericValue(piles[0].name[1]);
            b = (int)char.GetNumericValue(piles[0].name[4]);
        }
            
        
            
        
        
    }
    public void newDFS()
    {
        GameObject up, left, right, down;
        /*
         * remove the first path from the QUEUE (FILE); create new paths (to all children);
         * add the new paths to front of QUEUE (FILE);
         */
        do
        {


            Debug.Log(" DFS a = " + piles[0].name[1] + " Et b = " + piles[0].name[4]);
            piles.RemoveAt(0);
            if (a == 0)
            {
                if (b == 0)
                {
                    up = GameObject.Find((a, b + 1).ToString());
                    right = GameObject.Find((a + 1, b).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, right);

                }
                else
                if (b == 9)
                {
                    right = GameObject.Find((a + 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    piles.Insert(0, right);
                    piles.Insert(1, down);

                }
                else if (b != 0 && b != 9)
                {
                    up = GameObject.Find((a, b + 1).ToString());
                    right = GameObject.Find((a + 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }

            }
            else
            if (a == 9)
            {
                if (b == 9)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    piles.Insert(0, left);
                    piles.Insert(1, down);

                }
                else
                if (b == 0)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    up = GameObject.Find((a, b + 1).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, left);

                }
                else if (b != 0 && b != 9)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    up = GameObject.Find((a, b + 1).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, down);

                }
            }
            else if (a != 0 && a != 9)
            {
                if (b == 0)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    up = GameObject.Find((a, b + 1).ToString());
                    right = GameObject.Find((a + 1, b).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);

                }
                else if (b == 9)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    right = GameObject.Find((a + 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    piles.Insert(0, left);
                    piles.Insert(1, right);
                    piles.Insert(2, down);

                }
                else if (b != 0 && b != 9)
                {
                    left = GameObject.Find((a - 1, b).ToString());
                    up = GameObject.Find((a, b + 1).ToString());
                    right = GameObject.Find((a + 1, b).ToString());
                    down = GameObject.Find((a, b - 1).ToString());
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                    piles.Insert(3, down);

                }

            }
            setCursor();
        } while (piles.Count != 0 && trouvé == false);

        if(trouvé == true)
        {
            SpriteRenderer FinalPoint = piles[0].GetComponent<SpriteRenderer>();
            FinalPoint.color = Color.green;
        }
           
   
    }
    public void simpleInsert(GameObject[] tab) 
    {
        piles.RemoveAt(0);
        for(int i =0; i == tab.Length; i++)
        {
            piles.Insert(i, tab[i]);
        }
    }

    private void OnMouseDown()
    {
        print("Click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseClick()
    {
        Debug.Log("Mouse is clicked (down then up its 1 click)");
    }
    private void SpawnTile(int x, int y, int value)
    {
        GameObject g = new GameObject((x,y).ToString());
        g.AddComponent<BoxCollider2D>();
        g.transform.position = new Vector2(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
        g.tag = "blanche";
        var s = g.AddComponent<SpriteRenderer>();
        s.color = new Color(value, value, value,0);
        s.sprite = sprite;
        
        
    }
    
}
