//using UnityEngine;
//using System.IO;
//public class VeryBadIdea : MonoBehaviour
//{

//    private string filename = "WeWLad.txt";
//    private float some_variable;
//    private string textToWrite = "Variables : ";
//    // Update is called once per frame
//    void Update()
//    {
//        some_variable += Time.deltaTime;//update the variable with something;
//        textToWrite = textToWrite + " " + some_variable.ToString();//create a proper string so we can read the file afterwards
//        File.AppendAllText(filename, textToWrite);//write to the file. No need to call Flush or Close. Note this does NOT overwrite the file every time you restart the game
//    }
//}