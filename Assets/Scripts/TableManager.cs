using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public List<Transform> data; // General model of a table is a list, divided into rows;
    public float offsetX;
    public float offsetY;

    protected int columns;
    protected int rows;
    protected float width;
	protected float height;
	protected float itemWidth;	
	protected float itemHeight;
	protected int maxRows;
	protected int maxCols;

	public void InitializeTable()
	{
		width = transform.GetComponent<RectTransform>().rect.width;
    	height = transform.GetComponent<RectTransform>().rect.height;
	}

    public void PopulateTable()
    {
    	int x = 1;
    	int y = 1;
    	foreach (Transform item in data)
    	{
    		item.GetComponent<RectTransform>().anchoredPosition = new Vector2(itemWidth * x + offsetX, -itemHeight * y + offsetY);
    		item.SetParent(transform, false);
    		x++;

    		if (x > columns)
    		{
    			x = 1;
    			y++;
    		}
    	}
    }

    public void BuildTable()
    {
    	CalculateItem();
    	CalculateTable();
    	PopulateTable();
    }  

    protected void CalculateItem()
    {
    	itemHeight = data[0].GetComponent<RectTransform>().rect.height;
    	itemWidth = data[0].GetComponent<RectTransform>().rect.width;
    }


	protected void CalculateTable() // all GameObjects must be uniform
    {
    	maxCols = (int) (width / data[0].GetComponent<RectTransform>().rect.width); // Max number of objects that can fit horazontally in the table
        if (maxCols == 0)
        {
            maxCols = 1;
        }
        Debug.Log(maxCols);

    	columns = data.Count;
    	rows = 1;

    	if (columns > maxCols) // Check if there are more columns than max and create rows accordingly 
    	{
    		columns = maxCols;
    		rows = (int) Math.Ceiling((double) (data.Count / maxCols));
    	}
    }



}
