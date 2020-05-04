using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService 
{

    private const string xmlApi =
        "https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=5cc811e27c7c7d1b5a80b923c5bb6b7b&mode=xml";

    private const string webImage =
        "https://upload.wikimedia.org/wikipedia/commons/1/17/Moraine_Lake_2.jpg";

    private bool IsResponseValid(WWW www)
    {
        if (www.error != null)
        {
            Debug.LogError("Bad connection...");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.LogError("Bad data ...");
            return false;
        }
        else
        {
            // no issues with response!
            return true;
        }
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!IsResponseValid(www))
            yield break;

        callback(www.text);
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        WWW www = new WWW(webImage);
        yield return www;
        callback(www.texture);
    }
}
