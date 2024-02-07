using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Schemat : MonoBehaviour
{
    public new string name;
    public string description;
    public Item ItemCrafted;
    public List<string> NeedMinerals;
    public List<int> numofNeedMinerals;
}
