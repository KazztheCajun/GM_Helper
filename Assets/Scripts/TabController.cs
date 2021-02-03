using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{

    public GameObject[] tabs;

    public void OpenInitiativeTab()
    {
        OpenTab("Initiative Table");
    }

    public void OpenStatsTab()
    {
    	OpenTab("Monster Info");
    }

    public void OpenTreasureTab()
    {
    	OpenTab("Treasure Table");
    }

    public void OpenDiceRoller()
    {
        OpenTab("Dice Roller");
    }

    private void OpenTab(string button)
    {
        foreach( GameObject o in tabs)
        {
            if (o.name == button)
            {
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
    }


}
