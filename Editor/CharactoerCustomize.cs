using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
[CustomEditor(typeof(ShapeChange))]
public class CharactoerCustomize : Editor
{
    private int selectIndex;
    Canvas canvas;
    //先检查有没有胃空
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  
        EditorGUILayout.Space();
        var shapeChange = (ShapeChange)target;
        if (shapeChange.skinnedMeshRenderer == null)
        {
            EditorGUILayout.LabelField("No Skinned Mesh Renderer");
        }
        EditorGUILayout.LabelField("请创建滑动条");
        //检查是否有换其他的skinmeshrenderer
        if (!shapeChange.DoesTargerMatch())
        {
            shapeChange.cleanBlenderShapes();
           
        }
//包括最开始没有的情况或者切换的情况
        if (shapeChange.GetNumbers() <= 0)
        {
            shapeChange.Initialize();
        }

        string[] blendeshapeNames = shapeChange.GetBlenderShapeNames();
        if (blendeshapeNames.Length <= 0)
        {
            EditorGUILayout.LabelField("目标没有正确的blendershape");
            shapeChange.cleanBlenderShapes();
            return;
        }   
        selectIndex=EditorGUILayout.Popup("BlendershapeNames", selectIndex, blendeshapeNames);
        if (GUILayout.Button("创建滑动条"))
        {
            if (canvas == null)
            {
                canvas=GameObject.FindObjectOfType<Canvas>();
            }

            if (canvas == null)
            {
                throw new System.Exception("场景无canvas");
            }
            GameObject sliderGO=Instantiate(Resources.Load("Slider",typeof(GameObject))) as GameObject;

            var blenderShapeSlider = sliderGO.GetComponent<SliderControl>();
            blenderShapeSlider.blenderShapeName = blendeshapeNames[selectIndex];
            blenderShapeSlider.name=blendeshapeNames[selectIndex];
            blenderShapeSlider.transform.parent = canvas.transform;
            blenderShapeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(140f, 50f);
            blenderShapeSlider.GetComponentInChildren<Text>().text = blendeshapeNames[selectIndex];

            //获取到blendershape
            BlenderShape blenderShape = shapeChange.GetBlendShape(blendeshapeNames[selectIndex]);
            //调整slider的min和max
            Slider slider = sliderGO.GetComponent<Slider>();
            if (blenderShape.positiveIndex == -1)
                slider.maxValue = 0;
            if(blenderShape.negativeIndex==-1)
                slider.minValue = 0;

            slider.value = 0;
            Debug.Log(blendeshapeNames[selectIndex]+"slider创建完成！");
            

        }
    }
    
}
