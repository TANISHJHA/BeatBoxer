using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string dataDirPath = " ";
    private string dataFileName = " "; 

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    } 

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = " ";

                //File Read
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize 
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            catch
            {
                Debug.LogError("Error occurred when trying to load data from: " + fullPath);
            }
        }
        return loadedData;
    } 

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName); 
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); 

            //serialize to JSON 
            string dataToStore = JsonUtility.ToJson(gameData, true);

            //write to file 
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        } 
        catch
        {
            Debug.LogError("Error occurred when trying to save data to: " + fullPath);
        }
    }
}
