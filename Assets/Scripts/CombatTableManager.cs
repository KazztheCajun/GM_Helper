using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatTableManager : MonoBehaviour
{
    public Transform entryContainer;
	public Transform entryTemplate;

	public float templateHeight = 30f;

    private RectTransform[] entries; // 27 MAX entries
    private int LastIndex; // Index of the previous creatures turn

    public void InitializeTable()
    {
    	entryTemplate.gameObject.SetActive(false);
    	entries = new RectTransform[27];
    	
    	for (int i = 0; i < 27; i++) 
    	{
    		Transform entryTransform = Instantiate(entryTemplate, entryContainer);
    		RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
    		entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
    		SetEntryData(entryRectTransform, " ");
    		entryTransform.gameObject.SetActive(true);
    		entries[i] = entryRectTransform;
    	}
    }

    public void NewList(List<Creature> list)
    {
        AddEntries(list);
        entries[0].GetChild(1).gameObject.SetActive(true);
    }

    public void AddEntries(List<Creature> list)
    {
    	int count = 0;
    	foreach(Creature c in list)
    	{
    		SetEntryData(entries[count], c.name);
    		count++;
    	}
    }

    public void NextTurn(int index)
    {
        entries[LastIndex].GetChild(1).gameObject.SetActive(false);
        entries[index].GetChild(1).gameObject.SetActive(true);
        LastIndex = index;
    }

    public void SetEntryData(RectTransform r, string n)
    {
    	TextMeshProUGUI name = r.GetChild(0).GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
//    	TextMeshProUGUI init = r.GetChild(1).GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
    	name.text = n;
//    	init.text = i;
    }

    public void ClearTable()
    {
    	for(int i = 0; i < entries.Length; i++)
    	{
    		SetEntryData(entries[i], " ");
    	}
        entries[LastIndex].GetChild(1).gameObject.SetActive(false);
        LastIndex = 0;
    }
}
