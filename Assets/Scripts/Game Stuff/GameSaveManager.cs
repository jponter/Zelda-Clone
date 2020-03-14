using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    

    // WE ARE MAKING THIS A SINGLETON
    public static GameSaveManager gameSave;

    public List<ScriptableObject> objects = new List<ScriptableObject>();


    private void Awake()
    {
        if(gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }


    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {
        Debug.Log("Resetting Scriptables");
        for (int i = 0; i < objects.Count; i++)
        {
          
            switch (objects[i].GetType().FullName)
            {
                case "FloatValue":
                    FloatValue ftmp = (FloatValue)objects[i];
                    ftmp.RuntimeValue = ftmp.initialValue;
                    Debug.Log("resetting FloatValue");
                    break;

                case "BoolValue":
                    BoolValue btmp = (BoolValue)objects[i];
                    btmp.RuntimeValue = btmp.initialValue;
                    Debug.Log("resetting BoolValue");
                    break;

                case "Inventory":
                    Inventory itmp = (Inventory)objects[i];
                    itmp.coins = 0;
                    itmp.maxMagic = 10;
                    itmp.items.Clear();
                    itmp.numberOfKeys = 0;
                    Debug.Log("Inventory Reset");
                    break;

                  

                default:
                    break;
            }

          


            if(File.Exists(Application.persistentDataPath +
                string.Format($"/{i}.dat")))
            {
                File.Delete(Application.persistentDataPath +
                string.Format($"/{i}.dat"));
            }


        }
    }


    public void SaveScriptables()
    {
        Debug.Log("Saving to: " + Application.persistentDataPath);

        for (int i = 0; i < objects.Count; i++)
        {
            //open a file
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format($"/{i}.dat"));

            //create a binary formatter
            BinaryFormatter binary = new BinaryFormatter();

            //format the object as json
            var json = JsonUtility.ToJson(objects[i]);

            //write to the file
            binary.Serialize(file, json);

            //close the file
            file.Close();


        }
    }

    public void LoadScriptables()
    {
        
        for (int i = 0; i < objects.Count; i++)
        {

            if (File.Exists(Application.persistentDataPath +
                string.Format($"/{i}.dat")))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format($"/{i}.dat"), FileMode.Open);

                BinaryFormatter binary = new BinaryFormatter();

                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    objects[i]);

                file.Close();

            }


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
