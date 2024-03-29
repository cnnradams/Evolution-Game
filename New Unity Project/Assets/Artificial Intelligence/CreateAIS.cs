﻿using UnityEngine;
using System.Collections.Generic;

public class CreateAIS : MonoBehaviour {
    public GameObject AI;
    public static List<Brain> aiList = new List<Brain>();
    public int ais;
    public GenerateGrid grid;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
      
            for (int i = 0; i < ais; i++)
        {
            GameObject g = Instantiate(AI, new Vector2(Random.Range(0, grid.length), Random.Range(0, grid.width)),Quaternion.identity) as GameObject;
            g.transform.parent = transform;
        }
        foreach (Transform child in GameObject.Find("AIS").transform)
        {
            aiList.Add(child.GetComponent<Brain>());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
