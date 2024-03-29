﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class PlayerActions : MonoBehaviour {
    public Block selector;
    public Block placeBlock;
    public Camera c;
    UIAnimations a;
    private bool building = false;
    public bool Building
    {
        get
        {
            return building;
        }
        set
        {
            building = value;
        }
    }
    // Use this for initialization
    void Start () {
       a = GameObject.Find("Canvas").GetComponent<UIAnimations>();
	}
    public void StartBuild()
    {
        building = true;
    }
    public void StopBuild()
    {
        building = false;
    }
    int startx;
    int currentx;
    int starty;
    int currenty;
    Dictionary<GenerateGrid.coords, GameObject> createdBlocks = new Dictionary<GenerateGrid.coords, GameObject>();
    Dictionary<GenerateGrid.coords, Block> buildArea = new Dictionary<GenerateGrid.coords, Block>();
    // Update is called once per frame
    void Update()
    {


            if (building)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Select();
                
                }
                else if (Input.GetMouseButton(0))
                {
                    Select();
                    Build();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                LockIn();
                    buildArea = new Dictionary<GenerateGrid.coords, Block>();
                    Build();
               
            }
            
        }
    }
    void LockIn()
    {
        GenerateGrid.Building g = new GenerateGrid.Building(buildArea);
        if (!GenerateGrid.buildingList.ContainsKey(new GenerateGrid.coords((int)pos.x, (int)pos.y)))
        {
            GenerateGrid.buildingList.Add(new GenerateGrid.coords((int)pos.x, (int)pos.y), g);
        }
    }
    Vector3 pos;
    Vector2 two = Vector2.zero;
    int current = 0;
    void Select () {
        RaycastHit hit;
        pos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin,ray.direction,Color.green,2);
        
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.transform.position;
            
        }
        Debug.Log(pos);
        switch(current)
        {

        }
        if (Input.GetMouseButtonDown(0))
        {
            buildArea = new Dictionary<GenerateGrid.coords, Block>();
            startx = Mathf.RoundToInt(pos.x);
            starty = Mathf.RoundToInt(pos.y);
        }
        buildArea = new Dictionary<GenerateGrid.coords, Block>();
        int currentx = Mathf.RoundToInt(pos.x);
            int currenty = Mathf.RoundToInt(pos.y);
            int lessX;
            int greatX;
            if(startx < currentx)
            {
                lessX = startx;
                greatX = currentx;
            }
             else
            {
                lessX = currentx;
                greatX = startx;
            }

            int lessY;
            int greatY;
            if (starty < currenty)
            {
                lessY = starty;
                greatY = currenty;
            }
            else
            {
                lessY = currenty;
                greatY = starty;
            }
      //  Debug.Log(lessX + "," + greatX + ":" + lessY + "," + greatY);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX, lessY))))
                buildArea.Add(new GenerateGrid.coords(lessX, lessY), selector);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX, greatY))))
            buildArea.Add(new GenerateGrid.coords(lessX, greatY), selector);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, lessY))))
            buildArea.Add(new GenerateGrid.coords(greatX, lessY), selector);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, greatY))))
            buildArea.Add(new GenerateGrid.coords(greatX, greatY), selector);
            for(int i = lessX; i < greatX; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(i, lessY))))
                buildArea.Add(new GenerateGrid.coords(i, lessY), selector);
            }
            for (int i = lessY; i < greatY; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX,i))))
                buildArea.Add(new GenerateGrid.coords(lessX, i), selector);
            }
            for (int i = lessY; i < greatY; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, i))))
                buildArea.Add(new GenerateGrid.coords(greatX, i), selector);
            }
            for (int i = lessX; i < greatX; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(i, greatY))))
                buildArea.Add(new GenerateGrid.coords(i, greatY), selector);
            }

            for (int x = lessX; x < greatX; x++)
            {
                for(int y = lessY; y < greatY; y++)
                {
                if (!buildArea.ContainsKey((new GenerateGrid.coords(x, y))))
                    buildArea.Add(new GenerateGrid.coords(x, y), selector);
                }
            
        }
	}
    void Build()
    {
        foreach(KeyValuePair<GenerateGrid.coords,Block> entry in buildArea)
        {
            if(!createdBlocks.ContainsKey(entry.Key))
            {
                GameObject b = Instantiate(entry.Value.gameObject,new Vector2(entry.Key.x,entry.Key.y),Quaternion.identity) as GameObject;
                createdBlocks.Add(entry.Key, b);
            }
        }
        List<GenerateGrid.coords> entrycoords = new List<GenerateGrid.coords>();
        foreach (KeyValuePair<GenerateGrid.coords, GameObject> entry in createdBlocks)
        {
            entrycoords.Add(entry.Key);
        }
        foreach(GenerateGrid.coords c in entrycoords)
        {
            
            if (!buildArea.ContainsKey(c))
            {
                Destroy(createdBlocks[c]);
                createdBlocks.Remove(c);
            }
        }
    }
}
