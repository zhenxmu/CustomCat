using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderShape 
{
    public int positiveIndex{get;set;}
    public int negativeIndex{get;set;}

    public BlenderShape(int positiveIndex, int negativeIndex)
    {
        this.positiveIndex = positiveIndex;
        this.negativeIndex = negativeIndex;
    }
    
}
