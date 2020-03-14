using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;

    public ItemDatabase ItemDB;

    private void OnEnable()
    {
        //clear the inventory
        myInventory.myInventory.Clear();
        Debug.Log("Inventory Count = " + myInventory.myInventory.Count);
        
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        //ResetScriptables();
        Debug.Log("IS: Saving to: " + Application.persistentDataPath);

        //for (int i = 0; i < myInventory.myInventory.Count; i++)
        //{
        //open a file
        //FileStream file = File.Create(Application.persistentDataPath +
        //    string.Format($"/{i}.inv"));

        //string filepath = Application.persistentDataPath +
        //    string.Format($"/{i}.inv");

        string filepath = Application.persistentDataPath + "/newsave.json";

            StreamWriter sw = new StreamWriter(filepath);



        //create a binary formatter
        //BinaryFormatter binary = new BinaryFormatter();
        //myInventory.myInventory[i].GetInstanceID;
        //format the object as json
        //var json = JsonUtility.ToJson(myInventory.myInventory[i],true);

        var json = JsonUtility.ToJson(myInventory.SL, true);

            //write to the file
            //binary.Serialize(file, json);
            sw.WriteLine(json);



            //close the file
            //file.Close();

            sw.Close();


        //}
    }


    public void LoadScriptables()
    {
        Debug.Log("IS: Loading From: " + Application.persistentDataPath);

        string filepath = Application.persistentDataPath + "/newsave.json";
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);

            myInventory.SL = JsonUtility.FromJson<SerializableListString>(json);
            if (myInventory.SL.serializableList.Count > 0)
            {
                for (int i = 0; i < myInventory.SL.serializableList.Count; i++)
                {
                    InventoryItem obj = ItemDB.GetItem(myInventory.SL.serializableList[i]);
                    if (obj)
                    {
                        myInventory.myInventory.Add(obj);
                        Debug.Log("Added " + obj.itemName + " to inventory");
                        //if(myInventory.myInventory)
                        if (obj.numberHeld == 0)
                        {
                            obj.numberHeld = 1;
                        }
                    }
                    else
                    {
                        Debug.LogError("ITEM DB Not Found: " + myInventory.SL.serializableList[i]);
                    }
                }
            }
        }

        //int i = 0;

        //while (File.Exists(Application.persistentDataPath +
        //    string.Format($"/{i}.inv")))
        //{
        //    //create a temporary object that we can "fill in" with the saved data details
        //    //this is because we can't just load an item into a non existant slot we need to use the list.add() below to restore the inventory
        //    InventoryItem temp = ScriptableObject.CreateInstance<InventoryItem>();

            

        //    //FileStream file = File.Open(Application.persistentDataPath +
        //    //string.Format($"/{i}.inv"), FileMode.Open);

        //    //BinaryFormatter binary = new BinaryFormatter();
        //    string filepath = Application.persistentDataPath +
        //        string.Format($"/{i}.inv");

        //    StreamReader sr = new StreamReader(filepath);

        //    string saveJson = "";

        //    string line;
        //    while ((line = sr.ReadLine())!= null)
        //    {
        //        saveJson += line;
        //    }

        //    JsonUtility.FromJsonOverwrite(saveJson,
        //        temp);
          
            

            
        //    // file.Close();
        //    sr.Close();

        //    // add the temporary item we filled in to the inventory
        //    myInventory.myInventory.Add(temp);

        //    //string _path = UnityEditor.AssetDatabase.GetAssetPath(temp.GetInstanceID());
        //    //if (_path != null)
        //    //{
        //    //    Debug.Log("Path = " + _path);
        //    //    AssetDatabase.AddObjectToAsset(myInventory.myInventory[i], _path);
        //    //}
            

        //    i++;

        //}


        

    }

    public void ResetScriptables()
    {
        Debug.Log("Resetting Scriptables");
        //for (int i = 0; i < myInventory.myInventory.Count; i++)
        //{
        // this is from the GameSaveManager script - not using at the moment
        //switch (objects[i].GetType().FullName)
        //{
        //    case "FloatValue":
        //        FloatValue ftmp = (FloatValue)objects[i];
        //        ftmp.RuntimeValue = ftmp.initialValue;
        //        Debug.Log("resetting FloatValue");
        //        break;

        //    case "BoolValue":
        //        BoolValue btmp = (BoolValue)objects[i];
        //        btmp.RuntimeValue = btmp.initialValue;
        //        Debug.Log("resetting BoolValue");
        //        break;

        //    case "Inventory":
        //        Inventory itmp = (Inventory)objects[i];
        //        itmp.coins = 0;
        //        itmp.maxMagic = 10;
        //        itmp.items.Clear();
        //        itmp.numberOfKeys = 0;
        //        Debug.Log("Inventory Reset");
        //        break;



        //    default:
        //        break;
        //}



        int i = 0;
        while (File.Exists(Application.persistentDataPath +
            string.Format($"/{i}.inv")))
        {
            File.Delete(Application.persistentDataPath +
            string.Format($"/{i}.inv"));
            i++;
        }


        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
