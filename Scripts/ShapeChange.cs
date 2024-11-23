using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShapeChange : Singleton<ShapeChange>
{
    public string suffixMin = "Min";
    public string suffixMax = "Max";
    public SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh mesh;
    Dictionary<string,BlenderShape> blenderShapes=new Dictionary<string, BlenderShape>(); 
    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        mesh=skinnedMeshRenderer.sharedMesh;
        SaveBlenderShapes();
        
    }

    #region 数据存储
    void SaveBlenderShapes()
    {
        //下面这段是Enumerble.range.select使用的范例，用的少不太熟悉
        List<string> BlendeShapeNames=Enumerable.Range(0,mesh.blendShapeCount).Select(x=>mesh.GetBlendShapeName(x)).ToList();
        for (int i = 0; BlendeShapeNames.Count > 0;)
        {
            string noSuffix,altSuffix;
            noSuffix=BlendeShapeNames[i].TrimEnd(suffixMin.ToCharArray()).TrimEnd(suffixMax.ToCharArray()).Trim();
            string positiveName=string.Empty;
            string negativeName=string.Empty;
            int positiveIndex=-1;
            int negativeIndex=-1;
            bool exists=false;
            if (BlendeShapeNames[i].EndsWith(suffixMin))
            {
                negativeName = BlendeShapeNames[i];
                altSuffix = noSuffix+" "+suffixMax;
                if (BlendeShapeNames.Contains(altSuffix))
                {
                    exists = true;
                }
                negativeIndex = mesh.GetBlendShapeIndex(negativeName);
                if (exists)
                {
                    positiveName = altSuffix;
                    positiveIndex = mesh.GetBlendShapeIndex(positiveName);
                }
                
            }
            else
            {
                positiveName = BlendeShapeNames[i];
                altSuffix = noSuffix+" "+suffixMin;
                if (BlendeShapeNames.Contains(altSuffix))
                {
                    exists = true;
                }
                positiveIndex = mesh.GetBlendShapeIndex(positiveName);
                if (exists)
                {
                    negativeName = altSuffix;
                    negativeIndex = mesh.GetBlendShapeIndex(negativeName);
                }
            }
            if(blenderShapes.ContainsKey(noSuffix))
                Debug.Log(noSuffix+" 有了");
            Debug.Log(noSuffix);
            blenderShapes.Add(noSuffix,new BlenderShape(positiveIndex,negativeIndex));
            if(positiveName!=string.Empty)
                BlendeShapeNames.Remove(positiveName);
            if (negativeName != string.Empty)
                BlendeShapeNames.Remove(negativeName);
        }
    }
    #endregion

    public void ChangeBlenderShapeValue(string blendShapeName, float value)
    {
        if(!blenderShapes.ContainsKey(blendShapeName))
        {Debug.Log("不存在这个blendeshape"+blendShapeName);}
        
        BlenderShape blenderShape = blenderShapes[blendShapeName];
        value=Mathf.Clamp(value,-100,100);
        if (value > 0)
        {
            if (blenderShape.positiveIndex == -1)
                return;
            skinnedMeshRenderer.SetBlendShapeWeight(blenderShape.positiveIndex,value);
            if(blenderShape.negativeIndex == -1)
                return;
            skinnedMeshRenderer.SetBlendShapeWeight(blenderShape.negativeIndex,0);
            
        }
        else
        {
            if (blenderShape.negativeIndex == -1)
                return;
            skinnedMeshRenderer.SetBlendShapeWeight(blenderShape.negativeIndex,-value);
            if(blenderShape.positiveIndex == -1)
                return;
            skinnedMeshRenderer.SetBlendShapeWeight(blenderShape.positiveIndex,0);
        }
        
        
    }
}
