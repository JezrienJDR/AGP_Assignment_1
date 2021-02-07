using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OreTile : MonoBehaviour, IPointerClickHandler
{
    [Header("Ore heat colours")]
    public Sprite blue;
    public Sprite green;
    public Sprite yellow;
    public Sprite orange;
    public Sprite red;

    [Header("Rock Sprites")]
    public Sprite Rock1;
    public Sprite Rock2;
    public Sprite Rock3;
    public Sprite Rock4;
    public Sprite Rock5;
    public Sprite Rock6;

    SpriteRenderer sr;

    private int oreValue;

    bool hidden;

    public int x;
    public int y;

    void Start()
    {
        //Debug.Log("initializing ore value at Zero");
        
        //oreValue = 0;
        hidden = true;

        sr = GetComponent<SpriteRenderer>();

        int rockNum = Random.Range(1, 7);

        //switch(rockNum)
        //{
        //    case 1:
        //        sr.sprite = Rock1;
        //        break;
        //    case 2:
        //        sr.sprite = Rock2;
        //        break;
        //    case 3:
        //        sr.sprite = Rock3;
        //        break;
        //    case 4:
        //        sr.sprite = Rock4;
        //        break;
        //    case 5:
        //        sr.sprite = Rock5;
        //        break;
        //    case 6:
        //        sr.sprite = Rock6;
        //        break;
        //}
    }

    public void SetTile(int val, int _x, int _y)
    {
        x = _x;
        y = _y;
        //Debug.Log(oreValue);
        SetOreValue(val);
        //Debug.Log(oreValue);
        int rockNum = Random.Range(1, 7);

        sr = GetComponent<SpriteRenderer>();

        switch (rockNum)
        {
            case 1:
                sr.sprite = Rock1;
                break;
            case 2:
                sr.sprite = Rock2;
                break;
            case 3:
                sr.sprite = Rock3;
                break;
            case 4:
                sr.sprite = Rock4;
                break;
            case 5:
                sr.sprite = Rock5;
                break;
            case 6:
                sr.sprite = Rock6;
                break;
        }
    }

    public void SetOreValue(int v)
    {
        oreValue = v;
        //Debug.Log("SETTING OREVALUE: ");
        //Debug.Log(oreValue);
    }

    public int GetOreValue()
    {
        return oreValue;
    }



    public void Expose()
    {
        //Debug.Log(oreValue);
        //Debug.Log("Exposing Tile");
        sr = GetComponent<SpriteRenderer>();

        hidden = false;

        if(green == null)
        {
            Debug.Log("GREEN ERROR");
        }

        //Debug.Log(oreValue);
        switch (oreValue)
        {
            case 0:
                sr.sprite = blue;
                break;
            case 1:
                sr.sprite = green;
                break;
            case 2:
                sr.sprite = yellow;
                break;
            case 3:
                sr.sprite = orange;
                break;
            case 4:
                sr.sprite = red;
                break;
        }
    }


    public void DownGrade()
    {
        if(oreValue > 0)
        {
            oreValue -= 1;
       
            if(!hidden)
            {
                switch (oreValue)
                {
                    case 0:
                        sr.sprite = blue;
                        break;
                    case 1:
                        sr.sprite = green;
                        break;
                    case 2:
                        sr.sprite = yellow;
                        break;
                    case 3:
                        sr.sprite = orange;
                        break;
                    case 4:
                        sr.sprite = red;
                        break;
                }
            }
        }

       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(oreValue);

        OreGrid grid = transform.parent.GetComponent<OreGrid>();
        if(grid.mode == OreGrid.Mode.MINE)
        {
            if (grid.minesLeft > 0)
            {
                grid.Mine(oreValue, x, y);
                oreValue = 0;
                sr.sprite = blue;

                if (hidden) Expose();
            }
        }

        if (grid.mode == OreGrid.Mode.SCAN)
        {
            if (grid.scansLeft > 0)
            {
                Debug.Log("SCAN");
                Expose();
                grid.Scan(x, y);
            }
        }
    }



}
