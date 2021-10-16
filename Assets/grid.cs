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
    public List<GameObject> piles;
    public int a = 0, b =0;
    public GameObject cursor,startpoint;
    bool trouvé = false;
    int Vertical, Horizontal, Columns, Rows;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(newDFS);
        Button startbtn = startbutton.GetComponent<Button>();
        startbtn.onClick.AddListener(startPoint);

        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * (Screen.width / Screen.height);
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
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
    
        Debug.Log("Boutton appuyé :" + a);
        startpoint = GameObject.Find((a, b).ToString());
        var r = startpoint.GetComponent<SpriteRenderer>();
        r.color = Color.yellow;
        startpoint.name = "StartPoint";
        piles.Add(startpoint);
        cursor = piles[0];
    }
    

    private void clean()
    {
        GameObject S = GameObject.Find("StartPoint");
        
    }
    public void setCursor(List<GameObject> curseur)
    {
        // Position du curseur et suppression des boucles et des obstacles

    }
    public void newDFS()
    {
        GameObject up, left, right, down;
        piles.RemoveAt(0);
        if (a == 0)
        {
            if (b == 0)
            {
                up = GameObject.Find((a, b + 1).ToString());
                right = GameObject.Find((a + 1, b).ToString());
                piles.Insert(0, up);
                piles.Insert(1, right);
            }else
            if (b == 9)
            {
                right = GameObject.Find((a + 1, b).ToString());
                down = GameObject.Find((a, b - 1).ToString());
                piles.Insert(0, right);
                piles.Insert(1, down);
            }
            else if (b!=0 && b!=9)
            {
                up = GameObject.Find((a, b + 1).ToString());
                right = GameObject.Find((a + 1, b).ToString());
                down = GameObject.Find((a, b - 1).ToString());
                piles.Insert(0, up);
                piles.Insert(1, right);
                piles.Insert(2, down);
            }
            
        }else
        if (a == 9)
        {
            if (b == 9)
            {
                left = GameObject.Find((a - 1, b).ToString());
                down = GameObject.Find((a, b - 1).ToString());
                piles.Insert(0, left);
                piles.Insert(1, down);
            }else
            if (b == 0)
            {
                left = GameObject.Find((a - 1, b).ToString());
                up = GameObject.Find((a, b + 1).ToString());
                piles.Insert(0, up);
                piles.Insert(1, left);
            }
            else if(b!=0 && b!=9)
            {
                left = GameObject.Find((a - 1, b).ToString());
                down = GameObject.Find((a, b - 1).ToString());
                up = GameObject.Find((a, b + 1).ToString());
                piles.Insert(0, up);
                piles.Insert(1, left);
                piles.Insert(2, down);
            }
        }
        else if (a != 0 && a!=9)
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
            else if (b!=0 && b != 9)
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
        
    }
    public void simpleInsert(GameObject[] tab) 
    {
        piles.RemoveAt(0);
        for(int i =0; i == tab.Length; i++)
        {
            piles.Insert(i, tab[i]);
        }
    }

    public void dfs()
    {
        int x = 0;
        Random rnd = new Random();
        GameObject S = GameObject.Find("StartPoint");

        var StartPoint = S.GetComponent<SpriteRenderer>();
        StartPoint.color = Color.green;
        //Initialisation de la pile
        piles.Insert(0,S);
        //Insertion des élemnents
        do
        {
            //Check du Goal.
            gaol();
            //Affectation du curseur
            cursor = piles[0];
            var curseur = cursor.GetComponent<SpriteRenderer>();
            curseur.color = Color.grey;
            if (a == 0 && b == 0)
            {
                GameObject up = GameObject.Find((a, b + 1).ToString());
                GameObject right = GameObject.Find((a + 1, b).ToString());
                var haut = up.GetComponent<SpriteRenderer>();
                var droite = right.GetComponent<SpriteRenderer>();
                //Mise à jour de la file
                piles.RemoveAt(0);
                insertion(up, null, right, null);
                if (haut.color == Color.black)
                {
                    a++;
                    piles.Insert(0, right);

                }
                else
                {
                    b++;
                    piles.Insert(0, up);
                    if (droite.color == Color.white)
                    {
                        piles.Insert(1, right);
                    }
                }

            }
            if (a == 0)
            {
                GameObject up = GameObject.Find((a, b + 1).ToString());
                GameObject right = GameObject.Find((a + 1, b).ToString());
                GameObject down = GameObject.Find((a, b - 1).ToString());
                var haut = up.GetComponent<SpriteRenderer>();
                var droite = right.GetComponent<SpriteRenderer>();
                var bas = down.GetComponent<SpriteRenderer>();
                //Mise à jour de la file
                piles.RemoveAt(0);
                insertion(up, null, right, down);
            }
            if (b == 0)
            {
                GameObject up = GameObject.Find((a, b + 1).ToString());
                GameObject right = GameObject.Find((a + 1, b).ToString());
                GameObject left = GameObject.Find((a - 1, b).ToString());
                var haut = up.GetComponent<SpriteRenderer>();
                var droite = right.GetComponent<SpriteRenderer>();
                var gauche = left.GetComponent<SpriteRenderer>();
                //Mise à jour de la file
                piles.RemoveAt(0);
                insertion(up, left, right, null);
            }
            if (a == 9)
            {
                if (b == 9)
                {
                    GameObject left = GameObject.Find((a - 1, b).ToString());
                    GameObject down = GameObject.Find((a, b - 1).ToString());
                    var gauche = left.GetComponent<SpriteRenderer>();
                    var bas = down.GetComponent<SpriteRenderer>();
                    //Mise à jour de la file
                    piles.RemoveAt(0);
                    insertion(null, left, null, down);
                }
                if (b == 0)
                {
                    GameObject left = GameObject.Find((a - 1, b).ToString());
                    GameObject up = GameObject.Find((a, b + 1).ToString());
                    var gauche = left.GetComponent<SpriteRenderer>();
                    var haut = up.GetComponent<SpriteRenderer>();
                    //Mise à jour de la file
                    piles.RemoveAt(0);
                    insertion(up, left, null, null) ;
                }
                else
                {
                    GameObject left = GameObject.Find((a - 1, b).ToString());
                    GameObject down = GameObject.Find((a, b - 1).ToString());
                    GameObject up = GameObject.Find((a, b + 1).ToString());
                    var gauche = left.GetComponent<SpriteRenderer>();
                    var bas = down.GetComponent<SpriteRenderer>();
                    var haut = up.GetComponent<SpriteRenderer>();
                    //Mise à jour de la file
                    piles.RemoveAt(0);
                    insertion(up, left, null,down);
                }
            }
            else
            {

            }

        }
        while (trouvé == false);

      
      //  do
      //  {
            //var h = (GameObject.Find((x, y + 1).ToString())).GetComponent<SpriteRenderer>();
           // var g = (GameObject.Find((x-1, y).ToString())).GetComponent<SpriteRenderer>();
           // var d = (GameObject.Find((x+1, y).ToString())).GetComponent<SpriteRenderer>();
           // var b = (GameObject.Find((x, y - 1).ToString())).GetComponent<SpriteRenderer>();

            /*if (h.gameObject.activeInHierarchy == true)
            {
                if (h.color != Color.black)
                {
                    print("Haut blanc");
                    haut = h;
                    h.color = Color.yellow;
                    if (y + 1 == 10)
                    {
                        y = 0;
                    }
                    else
                        y++;
                }
                else
                {
                    print("Haut noir");
                   //  haut.color = Color.black;
                    x++;
                    y = 0;
                }
            }
            
          /* if (h.color == Color.black)
            {
                print("Haut noir");
                haut = h;
                x++;
            }*/
     //   }
       // while (cursor.color != Color.red) ;

    }

    private void gaol()
    {
        var g = cursor.GetComponent<SpriteRenderer>();
        if(g.color == Color.red)
        {
            trouvé = true;
        }
    }

    private void insertion(GameObject up, GameObject left, GameObject right, GameObject down)
    {
        if (up != null)
        {
            if (left != null)
            {
                if (right != null)
                {
                    if (down != null)
                    {
                        piles.Insert(0, up);
                        piles.Insert(1, left);
                        piles.Insert(2, right);
                        piles.Insert(3, down);
                    }else
                    piles.Insert(0, up);
                    piles.Insert(1, left);
                    piles.Insert(2, right);
                }
                else // Right == null
                {
                    if (down != null)
                    {
                        piles.Insert(0, up);
                        piles.Insert(1, left);
                        piles.Insert(2, down);
                    }
                    else
                        piles.Insert(0, up);
                        piles.Insert(1, left);
                }
            }
            else // left == null
            {
                if (right != null)
                {
                    if (down != null)
                    {
                        piles.Insert(0, up);
                        piles.Insert(1, right);
                        piles.Insert(2, down);
                    }
                    else
                        piles.Insert(0, up);
                        piles.Insert(1, right);
                }
                else // Right == null
                {
                    if (down != null)
                    {
                        piles.Insert(0, up);
                        piles.Insert(1, down);
                    }
                    else
                        piles.Insert(0, up);
                }
            }

        }
        else
        {
            if (left != null)
            {
                if (right != null)
                {
                    if (down != null)
                    {
                        piles.Insert(0, left);
                        piles.Insert(1, right);
                        piles.Insert(2, down);
                    }
                    else
                        piles.Insert(0, left);
                        piles.Insert(1, right);
                }
                else // Right == null
                {
                    if (down != null)
                    {
                        piles.Insert(0, left);
                        piles.Insert(1, down);
                    }
                    else
                        piles.Insert(0, left);
                }
            }
            else // left == null
            {
                if (right != null)
                {
                    if (down != null)
                    {
                        piles.Insert(0, right);
                        piles.Insert(1, down);
                    }
                    else
                        piles.Insert(0, right);
                }
                else // Right == null
                {
                    if (down != null)
                    {
                        piles.Insert(0, down);
                    }
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void SpawnTile(int x, int y, int value)
    {
        GameObject g = new GameObject((x,y).ToString());
        g.transform.position = new Vector2(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
        g.tag = "blanche";
        var s = g.AddComponent<SpriteRenderer>();
        s.color = new Color(value, value, value);
        s.sprite = sprite;
        s.tag = "noire";
    }
    
}
