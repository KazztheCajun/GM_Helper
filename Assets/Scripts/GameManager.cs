using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro; 

public class GameManager : MonoBehaviour
{
	public Transform Tables; // Indexs in order: Initiative Table ; Monster Table ; Treasure Table
	public Transform InitTemplate;
	public Transform EffectTemplate;
    public Transform RoundCounter;
    public Transform RollHistory;
    public Transform RollTemplate;
    public Transform RollNumber;
    public Transform RollModifier;

    public Transform PlayertListData;
    public Transform MonsterListData;

	private TMP_InputField[] playerNames;
	private TMP_InputField[] playerInits;
	private TMP_InputField[] monsterNames;
	private TMP_InputField[] monsterInits;

    private InitList InitList;

	void Start()
	{
        playerNames = PlayertListData.GetChild(1).GetComponentsInChildren<TMP_InputField>();
        playerInits = PlayertListData.GetChild(2).GetComponentsInChildren<TMP_InputField>();
        monsterNames = MonsterListData.GetChild(1).GetComponentsInChildren<TMP_InputField>();
        monsterInits = MonsterListData.GetChild(2).GetComponentsInChildren<TMP_InputField>();
	}

	public void StartCombat()
    {
        InitList = new InitList(PlayerList(), MonsterList());
        Transform InitTable = Tables.GetChild(0).GetChild(1);
        TableManager tm = InitTable.GetComponent<TableManager>();
        tm.InitializeTable();
        foreach (Creature c in InitList.List)
        {
            Transform temp = Instantiate(InitTemplate, InitTable);
            temp.GetChild(0).GetComponent<TextMeshProUGUI>().text = c.name;
            temp.gameObject.SetActive(true);
            temp.GetChild(1).gameObject.SetActive(false);
            tm.data.Add(temp);
        }
        tm.BuildTable();
        tm.data[0].GetChild(1).gameObject.SetActive(true);
        SetRound(1);
    }

    public void NextTurn()
    {
        InitList.NextCreature();
        if (InitList.ActiveIndex == 0)
        {
            Tables.GetChild(0).GetChild(1).GetChild(InitList.List.Count - 1).GetChild(1).gameObject.SetActive(false);
            Tables.GetChild(0).GetChild(1).GetChild(InitList.ActiveIndex).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Tables.GetChild(0).GetChild(1).GetChild(InitList.ActiveIndex - 1).GetChild(1).gameObject.SetActive(false);
            Tables.GetChild(0).GetChild(1).GetChild(InitList.ActiveIndex).GetChild(1).gameObject.SetActive(true);
        }
        SetRound();
    }

    private void SetRound()
    {
        RoundCounter.GetComponent<TextMeshProUGUI>().text = InitList.Round + "";
    }

     private void SetRound(int num)
    {
        RoundCounter.GetComponent<TextMeshProUGUI>().text = num + "";
    }

    private List<Creature> PlayerList()
    {
    	List<Creature> l = new List<Creature>();

    	for(int x = 0; x < playerNames.Length; x++)
    	{
    		if (playerNames[x].text != "" && playerInits[x].text != "")
    		{
    			l.Add(new Creature(true, playerNames[x].text, Convert.ToInt32(playerInits[x].text)));
    		}
    	}
    	Debug.Log(l.ToString());
    	return l;
    }

    private List<Creature> MonsterList()
    {
    	List<Creature> l = new List<Creature>();

    	for(int x = 0; x < monsterNames.Length; x++)
    	{
    		if (monsterNames[x].text != "" && monsterInits[x].text != "")
    		{
    			l.Add(new Creature(false, monsterNames[x].text, Convert.ToInt32(monsterInits[x].text)));
    		}
    	}
    	Debug.Log(l.ToString());
    	return l;
    }

    public void ClearPlayerList()
    {
        ClearListText(playerNames);
        ClearListText(playerInits);
    }

    public void ClearMonsterList()
    {
        ClearListText(monsterNames);
        ClearListText(monsterInits);
        ClearListText(MonsterListData.GetChild(3).GetComponentsInChildren<TMP_InputField>()); // Monster HP
    }

    public void ClearRollList()
    {
        foreach(Transform o in RollHistory)
        {
            Destroy(o.gameObject);
        }
    }

    private void ClearListText(TMP_InputField[] list)
    {
        foreach(TMP_InputField f in list)
        {
            f.text = "";
        }
    }

    public void EndCombat()
    {
        Transform table = Tables.GetChild(0).GetChild(1);
        foreach(Transform t in table)
        {
            if (t.name == "Init Template(Clone)")
            {
                Destroy(t.gameObject);
            }
        }
        table.GetComponent<TableManager>().data.Clear();
    }

    public void RollDice(int dice)
    {
        string test = RollNumber.GetComponent<TMP_InputField>().text;
        if (test == "" || Int32.Parse(test) <= 0 )
        {
            test = "1";
        }

        int roll = 0;
        int num = Int32.Parse(test);
        for (int x = 0; x < num; x++)
        {
            roll += UnityEngine.Random.Range(1, dice + 1); 
        }
        
        Transform temp = Instantiate(RollTemplate, RollHistory);
        string mod = RollModifier.GetComponent<TMP_InputField>().text;
        if (mod == "")
        {
            mod = "0";
        }
        int res = roll + Int32.Parse(mod);
        temp.GetComponent<TextMeshProUGUI>().text = res.ToString();
        temp.gameObject.SetActive(true);
    }
}

