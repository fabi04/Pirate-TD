using UnityEngine;

public abstract class Resource : Placeable {
    public int yieldAmount {get; protected set;}
    public Resources resourceType {get; protected set;}
}