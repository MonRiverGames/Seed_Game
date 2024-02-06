using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "Scriptable Objects/Seed")]
public class SeedSO : ScriptableObject
{
    string SeedName;
    Sprite icon;
    PhysicsMaterial2D PhysicsMaterial;
    Rigidbody2D Rigidbody;
    float Weight;
    float Drag;
}
