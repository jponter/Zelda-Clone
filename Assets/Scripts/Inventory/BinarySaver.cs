using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaver : MonoBehaviour
{
    //Checking to see if save exists
    public static bool SaveExists(string key)
    {
        var path = Application.persistentDataPath + "/saves/";
        if (File.Exists(path + key + ".txt"))
        {
            return true;
        }

        return false;
    }

    public static void Save<T>(T objectToSave, string key)
    {
        SaveToFile<T>(objectToSave, key);
    }

    public static void Save(Object objectToSave, string key)
    {
        SaveToFile<Object>(objectToSave, key);
    }

    private static void SaveToFile<T>(T objectToSave, string fileName)
    {
        var path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        var formatter = GetBinaryFormatter();
        var fileStream = new FileStream(path + fileName + ".txt", FileMode.Create);
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
