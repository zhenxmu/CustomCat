using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour
{
   private static T _instance;
   public static T Instance{get{return _instance;}}
   //protected Singleton(){}

   protected virtual void Awake()
   {
      _instance = this as T;
   }
   
   
   
}
