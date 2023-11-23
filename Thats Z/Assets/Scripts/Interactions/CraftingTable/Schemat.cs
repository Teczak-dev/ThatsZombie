using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Schemat", menuName = "Schemat/New Schemat")]
public class Schemat : ScriptableObject
{
    public new string name;
    public string description;
    public Item ItemCrafted;
    public List<string> NeedMinerals;
    public List<int> numofNeedMinerals;
}
