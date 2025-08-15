using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public static DataPersistanceManager instance { get; private set; }
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler fileDataHandler;

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame(); //Prolly change this so saved data loads on some other input instead on start
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Some bs with the save system");
        } 
        instance = this;
    } 

    public void NewGame()
    {
        this.gameData = new GameData();
    } 

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        if(this.gameData == null)
        {
            Debug.Log("No saved data, starting new");
            NewGame();
        } 

        //TODO: Data Read 
        foreach(IDataPersistance obj in dataPersistanceObjects)
        {
            obj.LoadData(gameData);
        }
        Debug.Log("Loaded high score:" + gameData.highScore);
    } 

    public void SaveGame()
    {
        //TODO: Data Write 
        foreach (IDataPersistance obj in dataPersistanceObjects)
        {
            obj.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);

        Debug.Log("Saved high score:" + gameData.highScore); 
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>(); 
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
