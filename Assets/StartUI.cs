using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    OreGrid grid;


    // Start is called before the first frame update
    void Start()
    {
        grid = transform.parent.GetComponent<OreGrid>();
    }

    public void SquareSetup()
    {
        grid.Square();      
    }

    public void OrganicSetup()
    {
        grid.Organic();        
    }
}
