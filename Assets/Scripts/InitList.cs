using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitList 
{
    public List<Creature> List {get; set;}
    public int ActiveIndex {get; set;}
    public int Round {get; set;}

    public InitList(List<Creature> players, List<Creature> monsters)
    {
    	List = new List<Creature>();
    	ActiveIndex = 0;
    	Round = 1;
    	BuildList(players, monsters);
    }

    public void BuildList(List<Creature> pl, List<Creature> ml)
    {
    	//Add players and monsters to the list
    	foreach (Creature c in pl)
    	{
    		List.Add(c);
    	}

    	foreach (Creature c in ml)
    	{
    		List.Add(c);
    	}

    	List.Sort();
    }

    public void NextCreature()
    {
    	ActiveIndex++;

    	if (ActiveIndex >= List.Count)
    	{
    		NewRound();
    	}
    }

    public void RemoveCreature()
    {

    }

    private void NewRound()
    {
    	Round += 1;
    	ActiveIndex = 0;

    }

   
}
