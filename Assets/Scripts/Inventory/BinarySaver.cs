using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaver : MonoBehaviour
{
    //Checking to see if file exists given a key (filename)
    public static bool SaveExists(string key)
    {
        var path = Application.persistentDataPath + "/saves/";
        if (File.Exists(path + key + ".txt"))
        {
            //file exists
            return true;
        }

        //file doesnt exist
        return false;
    }

    //Generic Method for type T
    public static void Save<T>(T objectToSave, string key)
    {
        //key will be used for our filename
        SaveToFile<T>(objectToSave, key);
    }

    //Object method for type Object
    public static void Save(Object objectToSave, string key)
    {
        SaveToFile<Object>(objectToSave, key);
    }

    //serialize the T object
    private static void SaveToFile<T>(T objectToSave, string fileName)
    {
        var path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        //we utilise a method here as in future we may add delegates to the formatter
        //for now it's an standard binary formatter
        var formatter = GetBinaryFormatter();
        //create a file stream, this will overwrite any existing file
        var fileStream = new FileStream(path + fileName + ".txt", FileMode.Create);

        //Try - Serialize our object
        //Catch - report on any exceptions
        //finally - close our file
        try
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Save failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }


    //reverse of the save!
    public static T Load<T>( string key)
    {
        var path = Application.persistentDataPath + "/saves/";
        var formatter = GetBinaryFormatter();
        var fileStream = new FileStream(path + key + ".txt", FileMode.Open);
        var returnValue = default(T);
        try
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        catch (SerializationException exception)
        {
            Debug.Log("Load failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
        }

        return returnValue;
    }

    
    public static BinaryFormatter GetBinaryFormatter()
    {
        var formatter = new BinaryFormatter();
    
        return formatter;
    }
}
