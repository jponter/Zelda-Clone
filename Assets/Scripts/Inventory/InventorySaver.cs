using Leguar.TotalJSON;
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
    private SerializableListString SL = new SerializableListString();


    private void OnEnable()
    {
        //clear the inventory
        myInventory.myInventory.Clear();
        Debug.Log("Inventory Count = " + myInventory.myInventory.Count);

        //clear the SL
        SL.serializableList.Clear();
        
        LoadScriptables();
        ImportSaveData();
    }

    private void OnDisable()
    {
        SL.serializableList.Clear();
        BuildSaveData();
        SaveScriptables();
    }

    private void ImportSaveData()
    {
        for (int i = 0; i < SL.serializableList.Count; i++)
        {
            //go through the Sl and rebuild the items

            string name = SL.serializableList[i].name;
            int count = SL.serializableList[i].count;

            InventoryItem obj = ItemDB.GetItem(name);
                 if (obj)
                  {
                    obj.numberHeld = count;
                    myInventory.myInventory.Add(obj);
                    Debug.Log("Added " + obj.itemName + " count " + count +" to inventory");
                           
                        }
                        else
                        {
                            Debug.LogError("ITEM DB Not Found: " + SL.serializableList[i].name);
                        }


        }
    }

    private void BuildSaveData()
    {

        //go through the inventory and save out a key value pair of itemName and itemCount
        //then add to the serializablelist
        for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            SerializableListString.SerialItem SI = new SerializableListString.SerialItem();
            SI.name = myInventory.myInventory[i].itemName;
            SI.count = myInventory.myInventory[i].numberHeld;





            SL.serializableList.Add(SI);

            ////look through our serializable list to see if we can find the item                  
            //int index = (SL.serializableList.FindIndex(x => x.name == itemName));
            //if (index >= 0)
            //{
            //    //we have an item so let's
            //    playerInventory.SL.serializableList.Add(thisItem.itemName);
            //}
        }
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

        //var json = JsonUtility.ToJson(SL.serializableList, true);

        JSON jsonObject = JSON.Serialize(SL);

        string json = jsonObject.CreatePrettyString();

        //Debug.Log(JsonUtility.ToJson(SL,true));
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
            JSON jsonObject = JSON.ParseString(json);
            SL = jsonObject.Deserialize<SerializableListString>();


        }

            


    //        myInventory.SL = JsonUtility.FromJson<SerializableListString>(json);
    //        if (myInventory.SL.serializableList.Count > 0)
    //        {
    //            for (int i = 0; i < myInventory.SL.serializableList.Count; i++)
    //            {
    //                //SerializableListString.SerialItem temp = ;
    //                InventoryItem obj = ItemDB.GetItem(myInventory.SL.serializableList[i]);
    //                if (obj)
    //                {
    //                    myInventory.myInventory.Add(obj);
    //                    Debug.Log("Added " + obj.itemName + " to inventory");
    //                    //if(myInventory.myInventory)
    //                    if (obj.numberHeld == 0)
    //                    {
    //                        obj.numberHeld = 1;
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogError("ITEM DB Not Found: " + myInventory.SL.serializableList[i]);
    //                }
    //            }
    //        }
    //    }

    //    //int i = 0;

    //    //while (File.Exists(Application.persistentDataPath +
    //    //    string.Format($"/{i}.inv")))
    //    //{
    //    //    //create a temporary object that we can "fill in" with the saved data details
    //    //    //this is because we can't just load an item into a non existant slot we need to use the list.add() below to restore the inventory
    //    //    InventoryItem temp = ScriptableObject.CreateInstance<InventoryItem>();

            

    //    //    //FileStream file = File.Open(Application.persistentDataPath +
    //    //    //string.Format($"/{i}.inv"), FileMode.Open);

    //    //    //BinaryFormatter binary = new BinaryFormatter();
    //    //    string filepath = Application.persistentDataPath +
    //    //        string.Format($"/{i}.inv");

    //    //    StreamReader sr = new StreamReader(filepath);

    //    //    string saveJson = "";

    //    //    string line;
    //    //    while ((line = sr.ReadLine())!= null)
    //    //    {
    //    //        saveJson += line;
    //    //    }

    //    //    JsonUtility.FromJsonOverwrite(saveJson,
    //    //        temp);
          
            

            
    //    //    // file.Close();
    //    //    sr.Close();

    //    //    // add the temporary item we filled in to the inventory
    //    //    myInventory.myInventory.Add(temp);

    //    //    //string _path = UnityEditor.AssetDatabase.GetAssetPath(temp.GetInstanceID());
    //    //    //if (_path != null)
    //    //    //{
    //    //    //    Debug.Log("Path = " + _path);
    //    //    //    AssetDatabase.AddObjectToAsset(myInventory.myInventory[i], _path);
    //    //    //}
            

    //    //    i++;

    //    //}


        

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
