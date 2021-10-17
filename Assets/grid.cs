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
    public int a , b ;
    public GameObject startpoint;
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
        startpoint = GameObject.Find((a, b).ToString());
        var r = startpoint.GetComponent<SpriteRenderer>();
        r.color = Color.yellow;
        startpoint.tag = "StartPoint";
        piles.Add(startpoint);
        Debug.Log(piles.Count);
    }
    

    private void clean()
    {
        GameObject S = GameObject.Find("StartPoint");
        
    }
    public void setCursor()
    {
        SpriteRenderer cursor = piles[0].GetComponent<SpriteRenderer>();
        cursor.color = Color.grey;
    }
    public void newDFS()
    {
        GameObject up, left, right, down;
        print(a.GetType());
        
       
            Debug.Log(" DFS a = " + piles[0].name[1] + " Et b = " + piles[0].name[4]);
            piles.RemoveAt(0);
            if (piles[0].name[1] == 0)
            {
                if (piles[0].name[4] == 0)
                {
                    up = GameObject.Find((piles[0].name[1], piles[0].name[4] + 1).ToString());
                    up.tag = "up";
                    right = GameObject.Find((piles[0].name[1] + 1, piles[0].name[4]).ToString());
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
   
    }
    public void simpleInsert(GameObject[] tab) 
    {
        piles.RemoveAt(0);
        for(int i =0; i == tab.Length; i++)
        {
            piles.Insert(i, tab[i]);
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
    }
    
}
