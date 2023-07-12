using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static GameData CreateFile(){
        string path = Application.persistentDataPath + "/game.fun";
        if(File.Exists(path)){
            Debug.Log("Fitxer de dades ja està creat" + path);
            return null;
        }
        else{
            SaveGame(true, 0f);
            return new GameData(true, 0f);
        }
    }

    public static void SaveGame(bool hasMadetutorial, float maxScore){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(hasMadetutorial, maxScore);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame(){
        string path = Application.persistentDataPath + "/game.fun";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else{
            GameData data = CreateFile();
            return data;
        }
    }
}
