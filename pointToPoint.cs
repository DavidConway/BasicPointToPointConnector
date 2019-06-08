using UnityEngine;
using UnityEditor;
using System;

public class pointToPoint : EditorWindow
{
    string pos1;
    string pos2;

    float ajustX, ajustY, ajustZ;

    [MenuItem("Tools/Point to Point Connector")] //sets up menu and
    public static void ShowWindow()
    {
        EditorWindow editorWindow = GetWindow<pointToPoint>("Main");

        editorWindow.autoRepaintOnSceneChange = true; //alows the info to automaticly change
        
    }

    void OnGUI()
    {
        // displays info in menu
        EditorGUILayout.LabelField("positions",EditorStyles.boldLabel);
        
        GUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.LabelField("pos1: " + pos1);  
            EditorGUILayout.LabelField("pos2: " + pos2);
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            ajustX = EditorGUILayout.FloatField("X ajust: ", ajustX);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            ajustY = EditorGUILayout.FloatField("Y ajust: ", ajustY);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            ajustZ = EditorGUILayout.FloatField("Z ajust: ", ajustZ);
            GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();



        if (GUILayout.Button("Create"))
        {
            create();
        }



    }

    private void OnSelectionChange()
    {
        check();
    }

    private void Awake() // sets pow 1 and 2 to NA on open
    {
        pos1 = "NA";
        pos2 = "NA";
    }

    private void check() // checks to see what has changed
    {

        try// if at least one item is selected generats a readibul output for its data else sets it to NA
        {
            GameObject i = Selection.gameObjects[0]; 
            pos1 = "X:" + i.transform.position.x + " Y:" + i.transform.position.y + " Z:" + i.transform.position.z + " Name:" + i.name;
        }
        catch(IndexOutOfRangeException e)
        {
            pos1 = "NA";
        }

        try// if at least two item is selected generats a readibul output for its data else sets it to NA
        {
            GameObject i = Selection.gameObjects[1];
            pos2 = "X:" + i.transform.position.x + " Y:" + i.transform.position.y + " Z:" + i.transform.position.z + " Name:" + i.name;
        }
        catch(IndexOutOfRangeException e)
        {
            pos2 = "NA";
        }
    }

    private void create() // creats a object briging the gap between two points
    {
        GameObject i,j;

        i = Selection.gameObjects[0]; // sets up a referens to the first two selected objects
        j = Selection.gameObjects[1];

        GameObject make = GameObject.CreatePrimitive(PrimitiveType.Cylinder); //creats the bridging object

        make.transform.position = (i.transform.position + j.transform.position) / 2; //calculats the position 

        make.transform.LookAt(i.transform.position); // rotats to line up whith the two points
        make.transform.Rotate(ajustX, ajustY, ajustZ); // any rotation adjustments that are needed

        float scale = (Vector3.Distance(i.transform.position, j.transform.position))/2; //calculats scale
        make.transform.localScale =  new Vector3(1, scale, 1);  // sets scale
    }
}
