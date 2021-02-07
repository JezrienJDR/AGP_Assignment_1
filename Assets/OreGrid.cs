using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OreGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    StartUI startUI;
    GameUI gameUI;

    int[,] ore;
    GameObject[,] grid;

    public TextMeshProUGUI Scans;
    public TextMeshProUGUI Score;

    public enum Mode
    {
        SCAN,
        MINE
    }

    public Mode mode;

    int oreScore = 0;
    public int scansLeft = 6;
    public int minesLeft = 3;

    void Start()
    {
        // This 2D array of ints will represent the ore values of the tiles. It's 32x32, padded by 1 on
        // all sides for easier checking of adjacents.
        ore = new int[34, 34];

        mode = Mode.MINE;

        //gameUI.mineMode.interactable = false;

        oreScore = 0;

        // First we set all tiles to 0.
        for (int i = 0; i < 34; i++)
        {
            for (int j = 0; j < 34; j++)
            {
                ore[i, j] = 0;
            }
        }

        // Now we generate the starting positions of the ore deposits.
        int x1 = Random.Range(1, 33);
        int y1 = Random.Range(1, 33);
        int x2 = Random.Range(1, 33);
        int y2 = Random.Range(1, 33);
        int x3 = Random.Range(1, 33);
        int y3 = Random.Range(1, 33);
        int x4 = Random.Range(1, 33);
        int y4 = Random.Range(1, 33);
        int x5 = Random.Range(1, 33);
        int y5 = Random.Range(1, 33);



        ore[x1, y1] = 4;
        ore[x2, y2] = 4;
        ore[x3, y3] = 4;
        ore[x4, y4] = 4;
        ore[x5, y5] = 4;

        //DiamondScanAll();
        //SquareScanAll();
        //OrganicScanAll();

        //GenerateGrid();
        startUI = FindObjectOfType<StartUI>();
        gameUI = FindObjectOfType<GameUI>();
        gameUI.gameObject.SetActive(false);

        //Scans.text = "6";
        Scans.text = scansLeft.ToString();
    }

    public void Square()
    {
        SquareScanAll();
        GenerateGrid();
    }

    public void Organic()
    {
        OrganicScanAll();
        GenerateGrid();
    }

    void SquareScanAll()
    {
        SquareScan();
        SquareScan();
        SquareScan();
        SquareScan();
    }

    void DiamondScanAll()
    {
        DiamondScan();
        DiamondScan();
        DiamondScan();
        DiamondScan();
    }

    void OrganicScanAll()
    {
        //OrganicScan();
        //OrganicScan();
        //OrganicScan();
        //OrganicScan();
        CoreScan();
        CoreScan();


        DiamondScan();
        //SquareScan();
        DiamondScan();
        //SquareScan();
        DiamondScan();
        DiamondScan();

        OuterScan();


    }

    void GenerateGrid()
    {
        grid = new GameObject[32, 32];

        //float sideLength = tile.GetComponent<SpriteRenderer>().sprite.rect.width;
        float sideLength = 0.16f;

        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                grid[i, j] = Instantiate(tile, transform);
                grid[i, j].transform.position = new Vector3(transform.position.x + sideLength * i, transform.position.y + sideLength * j, transform.position.z);

                grid[i, j].GetComponent<OreTile>().SetTile(ore[i + 1, j + 1], i, j);

                //grid[i, j].GetComponent<OreTile>().Expose();

            }
        }

        transform.Translate(new Vector3(sideLength * -17, sideLength * -17, 0));

        startUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);

        MineMode();
    }

    public void ExposeAll()
    {
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                grid[i, j].GetComponent<OreTile>().Expose();
            }
        }
    }

    void CoreScan()
    {
        for (int i = 1; i < 33; i++)
        {
            for (int j = 1; j < 33; j++)
            {
                if (ore[i, j] == 0)
                {
                    if (HighestAdjacent(i, j) == 4)
                    {
                        int r = Random.Range(0, 5);

                        if (r > 2)
                        {
                            ore[i, j] = 4;
                        }
                        else
                        {
                            ore[i, j] = 3;
                        }
                    }

                }
            }
        }
    }

    void OuterScan()
    {
        for (int i = 1; i < 33; i++)
        {
            for (int j = 1; j < 33; j++)
            {
                if (ore[i, j] == 0)
                {
                    if (HighestCardinal(i, j) == 1)
                    {
                        int r = Random.Range(0, 5);

                        if (r > 3)
                        {
                            ore[i, j] = 1;
                        }
                        else
                        {
                            ore[i, j] = 0;
                        }
                    }

                }
            }
        }
    }
    void SquareScan()
    {
        for (int i = 1; i < 33; i++)
        {
            for (int j = 1; j < 33; j++)
            {
                if (ore[i, j] == 0)
                {
                    if (HighestAdjacent(i, j) == 4)
                    {
                        ore[i, j] = 3;
                    }

                    if (HighestAdjacent(i, j) == 3)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestAdjacent(i, j) == 3)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestAdjacent(i, j) == 2)
                    {
                        ore[i, j] = 1;
                    }
                }
            }
        }
    }

    void OrganicScan()
    {
        for (int i = 1; i < 33; i++)
        {
            for (int j = 1; j < 33; j++)
            {
                if (ore[i, j] == 0)
                {

                    if (HighestCardinal(i, j) == 4)
                    {
                        ore[i, j] = 4;
                    }

                    if (HighestCardinal(i, j) == 3)
                    {
                        ore[i, j] = 3;
                    }

                    if (HighestCardinal(i, j) == 2)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestCardinal(i, j) == 1)
                    {
                        ore[i, j] = 1;
                    }

                    if (HighestAdjacent(i, j) == 4)
                    {
                        ore[i, j] = 3;
                    }

                    if (HighestAdjacent(i, j) == 3)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestAdjacent(i, j) == 2)
                    {
                        ore[i, j] = 1;
                    }
                }
                
            }
        }
    }

    void DiamondScan()
    {
        for (int i = 1; i < 33; i++)
        {
            for (int j = 1; j < 33; j++)
            {
                if (ore[i, j] == 0)
                {
                    if (HighestCardinal(i, j) == 4)
                    {
                        ore[i, j] = 3;
                    }

                    if (HighestCardinal(i, j) == 3)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestCardinal(i, j) == 3)
                    {
                        ore[i, j] = 2;
                    }

                    if (HighestCardinal(i, j) == 2)
                    {
                        ore[i, j] = 1;
                    }
                }
            }
        }
    }

    int HighestAdjacent(int x, int y)
    {
        int n = Mathf.Max(HighestCardinal(x, y), HighestDiagonal(x, y));
        return n;
    }

    int HighestCardinal(int x, int y)
    {
        int n = 0;

        if (ore[x, y - 1] > n) n = ore[x, y - 1];
        if (ore[x, y + 1] > n) n = ore[x, y + 1];
        if (ore[x - 1, y] > n) n = ore[x - 1, y];
        if (ore[x + 1, y] > n) n = ore[x + 1, y];

        return n;
    }

    int HighestDiagonal(int x, int y)
    {
        int n = 0;

        if (ore[x - 1, y - 1] > n) n = ore[x - 1, y - 1];
        if (ore[x + 1, y + 1] > n) n = ore[x + 1, y + 1];
        if (ore[x - 1, y + 1] > n) n = ore[x - 1, y + 1];
        if (ore[x + 1, y - 1] > n) n = ore[x + 1, y - 1];

        return n;
    }

    int SumOfAdjacent(int x, int y)
    {
        int n = CardinalSum(x, y) + DiagonalSum(x, y);

        return n;
    }

    int CardinalSum(int x, int y)
    {
        int n = 0;

        n += ore[x, y - 1];
        n += ore[x, y + 1];
        n += ore[x - 1, y];
        n += ore[x + 1, y];

        return n;
    }

    int DiagonalSum(int x, int y)
    {
        int n = 0;

        n += ore[x - 1, y - 1];
        n += ore[x + 1, y + 1];
        n += ore[x - 1, y + 1];
        n += ore[x + 1, y - 1];

        return n;
    }


    public void MineMode()
    {
        mode = Mode.MINE;
        gameUI.mineMode.interactable = false;
        gameUI.scanMode.interactable = true;
    }

    public void ScanMode()
    {
        mode = Mode.SCAN;
        gameUI.scanMode.interactable = false;
        gameUI.mineMode.interactable = true;
    }

    List<OreTile> GrabSquare(int x, int y)
    {
        List<GameObject> objects = new List<GameObject>();

        //tiles.Add(grid[x, y + 1].GetComponent<OreTile>());
        //tiles.Add(grid[x + 1, y].GetComponent<OreTile>());
        //tiles.Add(grid[x, y - 1].GetComponent<OreTile>());
        //tiles.Add(grid[x - 1, y].GetComponent<OreTile>());
        //tiles.Add(grid[x - 1, y + 1].GetComponent<OreTile>());
        //tiles.Add(grid[x + 1, y + 1].GetComponent<OreTile>());
        //tiles.Add(grid[x + 1, y - 1].GetComponent<OreTile>());
        //tiles.Add(grid[x - 1, y - 1].GetComponent<OreTile>());
        //tiles.Add(grid[x - 2, y].GetComponent<OreTile>());
        //tiles.Add(grid[x, y + 2].GetComponent<OreTile>());
        //tiles.Add(grid[x + 2, y].GetComponent<OreTile>());
        //tiles.Add(grid[x, y - 2].GetComponent<OreTile>());
        //tiles.Add(grid[x - 1, y - 2].GetComponent<OreTile>());
        //tiles.Add(grid[x - 2, y - 1].GetComponent<OreTile>());
        //tiles.Add(grid[x - 2, y + 1].GetComponent<OreTile>());
        //tiles.Add(grid[x - 1, y + 2].GetComponent<OreTile>());
        //tiles.Add(grid[x + 1, y + 2].GetComponent<OreTile>());
        //tiles.Add(grid[x + 2, y + 1].GetComponent<OreTile>());
        //tiles.Add(grid[x + 2, y - 1].GetComponent<OreTile>());
        //tiles.Add(grid[x + 1, y - 2].GetComponent<OreTile>());
        //tiles.Add(grid[x - 2, y - 2].GetComponent<OreTile>());
        //tiles.Add(grid[x - 2, y + 2].GetComponent<OreTile>());
        //tiles.Add(grid[x + 2, y + 2].GetComponent<OreTile>());
        //tiles.Add(grid[x + 2, y - 2].GetComponent<OreTile>());

        objects.Add(GetTileByOffset(x, y, 0, 1));
        objects.Add(GetTileByOffset(x, y, 1, 0));
        objects.Add(GetTileByOffset(x, y, 0, -1));
        objects.Add(GetTileByOffset(x, y, -1, 0));
        objects.Add(GetTileByOffset(x, y, -1, 1));
        objects.Add(GetTileByOffset(x, y, 1, 1));
        objects.Add(GetTileByOffset(x, y, 1, -1));
        objects.Add(GetTileByOffset(x, y, -1, -1));
        objects.Add(GetTileByOffset(x, y, -2, 0));
        objects.Add(GetTileByOffset(x, y, 0, 2));
        objects.Add(GetTileByOffset(x, y, 2, 0));
        objects.Add(GetTileByOffset(x, y, 0, -2));
        objects.Add(GetTileByOffset(x, y, -1, -2));
        objects.Add(GetTileByOffset(x, y, -2, -1));
        objects.Add(GetTileByOffset(x, y, -2, 1));
        objects.Add(GetTileByOffset(x, y, -1, 2));
        objects.Add(GetTileByOffset(x, y, 1, 2));
        objects.Add(GetTileByOffset(x, y, 2, 1));
        objects.Add(GetTileByOffset(x, y, 2, -1));
        objects.Add(GetTileByOffset(x, y, 1, -2));
        objects.Add(GetTileByOffset(x, y, -2, -2));
        objects.Add(GetTileByOffset(x, y, -2, 2));
        objects.Add(GetTileByOffset(x, y, 2, 2));
        objects.Add(GetTileByOffset(x, y, 2, -2));        

        List<OreTile> tiles = new List<OreTile>();

        foreach(GameObject o in objects)
        {
            if(o != null)
            {
                tiles.Add(o.GetComponent<OreTile>());
            }
        }

        return tiles;
    }

    GameObject GetTileByOffset(int i, int j, int x, int y)
    {
        if( i + x >= 0 && i + x <= 31 && j + y >= 0 && j + y <= 31)
        {
            return grid[i + x, j + y];
        }
        else
        {
            return null;
        }
    }

    public void Scan(int x, int y)
    {

        if (scansLeft > 0)
        {
            scansLeft -= 1;
            Scans.text = scansLeft.ToString();

            StartCoroutine(scanSurroundingTiles(x, y));
        }
        //List<OreTile> surrounds = GrabSquare(x, y);

        //foreach (OreTile t in surrounds)
        //{       
        //    Debug.Log(t.GetOreValue());
        //    t.Expose();
        //}
    }

    IEnumerator scanSurroundingTiles(int x, int y)
    {
        List<OreTile> surrounds = GrabSquare(x, y);

        foreach (OreTile t in surrounds)
        {
            yield return new WaitForSeconds(0.04f);
            //Debug.Log(t.GetOreValue());
            t.Expose();
        }
    }

    public void Mine(int oreYield, int x, int y)
    {
        if (minesLeft > 0)
        {
            minesLeft -= 1;

            oreScore += oreYield;
            Score.text = oreScore.ToString();

            List<OreTile> surrounds = GrabSquare(x, y);

            foreach (OreTile t in surrounds)
            {
                t.DownGrade();
            }
        }
    }
}
