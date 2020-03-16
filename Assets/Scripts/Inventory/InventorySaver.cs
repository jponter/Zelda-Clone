using Leguar.TotalJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

/*
This class is responsible for saving out our scritpable objects that are stored in the inventory
we don't actually write out the scriptable objects to disk, only a reference
ie if we have a "Large Health Potion" SO in the inventory - we store the name
when importing we use a lookup table (SO's must all be in the inspector in an itemDatabase)
and recreate the inventory from that lookup table

Utilises the Total JSON library to serialize the class out (class contains a List of Structs)
see SerializbleListString.cs for details of that class
*/ 

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;

    public ItemDatabase ItemDB;

    //SL is our serializable class that contains a representation of the items we want to save - this is a COPY
    private SerializableListString SL = new SerializableListString();


    private void OnEnable()
    {
        //clear the inventory
        myInventory.myInventory.Clear();
        Debug.Log("Inventory Count = " + myInventory.myInventory.Count);

        //clear the SL - we don't want anything in there
        SL.serializableList.Clear();
        //Load our save file
        LoadScriptables();
        //re-import our save back into the game world
        ImportSaveData();
    }

    private void OnDisable()
    {
        //clear the SL 
        SL.serializableList.Clear();
        // build our save data from our current game state
        BuildSaveData();
        //save out the save data
        SaveScriptables();
    }

    private void ImportSaveData()
    {
        //go through the Sl and rebuild the items in the inventory
        for (int i = 0; i < SL.serializableList.Count; i++)
        {
            
            //we will need the name and the count from the save data
            string name = SL.serializableList[i].name;
            int count = SL.serializableList[i].count;


            // we dont save the actual scriptable objects only a reference (NAME) that we then lookup to insert the correct scriptable object
            InventoryItem obj =  ItemDB.GetItem(name);
                 if (obj)
                  {
                    // we have an object to restor - check how many of that item we need and set it 
                    obj.numberHeld = count;

                    // add the object to the inventory
                    myInventory.myInventory.Add(obj);
                    Debug.Log("Added " + obj.itemName + " count " + count +" to inventory");
                           
                        }
                        else
                        {
                            //should never hit this!
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
            //create a SerialItem and populate it from the inventory
            SerializableListString.SerialItem SI = new SerializableListString.SerialItem();
            SI.name = myInventory.myInventory[i].itemName;
            SI.count = myInventory.myInventory[i].numberHeld;
            
            //add to our SL - 
            SL.serializableList.Add(SI);

            
        }
    }


    public void SaveScriptables()
    {
        //ResetScriptables();
        Debug.Log("IS: Saving to: " + Application.persistentDataPath);
                
        //filepath
        string filepath = Application.persistentDataPath + "/newsave.json";
        
        //create a streamwriter
        StreamWriter sw = new StreamWriter(filepath);
               
        //use the JSON library to serialize our serializableList into a JSON object
        JSON jsonObject = JSON.Serialize(SL);
        
        //turn that JSON object into a pretty formatted string
        string json = jsonObject.CreatePrettyString();
        
        //write to our file
        sw.WriteLine(json);
        
        //close the file
        sw.Close();


    }


    public void LoadScriptables()
    {
        Debug.Log("IS: Loading From: " + Application.persistentDataPath);
    
        //filepath
        string filepath = Application.persistentDataPath + "/newsave.json";
        
        if (File.Exists(filepath))
        {
            //read in the file to a string
            string json = File.ReadAllText(filepath);
            //use the JSON library to parse the string
            JSON jsonObject = JSON.ParseString(json);
            //deserialize the JSON object back into our Serializable class
            SL = jsonObject.Deserialize<SerializableListString>();


        }


        

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


}
