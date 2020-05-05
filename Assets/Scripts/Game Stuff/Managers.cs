using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(WeatherManager))]
//[RequireComponent(typeof(ImagesManager))]
[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour
{

    //public static WeatherManager Weather { get; private set; }
    //public static ImagesManager Images { get; private set; }
    public static AudioManager Audio { get; private set; }

    public static Managers instance;

    private List<IGameManager> _startSequence;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("Starting Managers");
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        

        //instance = this;

        
        //Weather = GetComponent<WeatherManager>();
        //Images = GetComponent<ImagesManager>();
        Audio = GetComponent<AudioManager>();



        _startSequence = new List<IGameManager>();
        //_startSequence.Add(Weather);
        //_startSequence.Add(Images);
        _startSequence.Add(Audio);


        StartCoroutine(StartupManagers());
        DontDestroyOnLoad(this);
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();

        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady) Debug.Log("Progress: " + numReady + "/" + numModules);

            yield return null;
        }

        Debug.Log("All managers have started");
    }
}
