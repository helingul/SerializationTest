using HelinTest.OdinSerializer;
using MemoryPack;
using MultipleClassOdinTestSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.LightTransport;

namespace FinalSaveSystem
{
    public interface ISaveable
    {
        void LoadData(GameSaveData saveData);
        void Save(GameSaveData saveData);

        //void LoadDataWithMemoryPack(GameSaveDataMemoryPack saveData);
        //void SaveWithMemoryPack(GameSaveDataMemoryPack saveData);

    }

    [Serializable]
    public class GameSaveData
    {
        public int version = 1;

        public InventoryManager inventoryManager;
        public PlayerManager playerManager;
    }


    //[Serializable]
    //public class GameSaveData
    //{
    //    public int version = 1;

    //    public InventorySaveData inventorySavaData;
    //    public PlayerSaveData playerSaveData;
    //}

    //[MemoryPackable]
    //public partial class GameSaveDataMemoryPack
    //{
    //    public int version = 1;

    //    public InventorySaveData inventorySavaData;
    //    public PlayerSaveData playerSaveData;
    //}

    public class SaveManager : MonoBehaviour
    {
       
        private List<ISaveable> managers = new List<ISaveable>();
        private string filePath;
        InventoryManager inventoryManager;
        PlayerManager playerManager;

        private void Awake()
        {
            filePath = Path.Combine(Application.persistentDataPath, "multipleManagerSave.json");
        }
        void Start()
        {
            // this is added for testing purposes
            inventoryManager = new InventoryManager();
            playerManager = new PlayerManager();
            managers.Add(inventoryManager);
            managers.Add(playerManager);
        }

        // this is added for testing purposes
        public void UpdateAllValuesForTesting()
        {

            inventoryManager.UpdateAllValuesForTesting();
            playerManager.UpdateAllValuesForTesting();
        }

        // this is added for testing purposes
        public void PrintAllValuesForTesting()
        {

            inventoryManager.PrintAllValues();
            playerManager.PrintAllValues();
        }


        public void SaveGame()
        {
            GameSaveData save = new GameSaveData();

            foreach (var manager in managers)
            {
                manager.Save(save);
            }

            byte[] bytes = SerializationUtility.SerializeValue(save, DataFormat.JSON);

            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Game Saved (Binary)");
        }

        public void LoadGame()
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("No Save File Found");
                return;
            }

            byte[] bytes = File.ReadAllBytes(filePath);
            GameSaveData save = SerializationUtility.DeserializeValue<GameSaveData>(bytes, DataFormat.JSON);

            if (save == null)
            {
                Debug.LogError("Save file corrupted");
                return;
            }

            foreach (var manager in managers)
            {
                manager.LoadData(save);
            }

            Debug.Log("Game Loaded (Binary)");
        }


        //public void SaveGame()
        //{
        //    GameSaveData save = new GameSaveData();

        //    foreach (var manager in managers)
        //    {
        //        manager.Save(save);
        //    }

        //    byte[] bytes = SerializationUtility.SerializeValue(save,DataFormat.Binary);

        //    File.WriteAllBytes(filePath, bytes);

        //    //using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //    //{
        //    //    stream.Write(bytes, 0, bytes.Length);
        //    //}

        //    Debug.Log("Game Saved (Binary)");
        //}

        //public void LoadGame()
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        Debug.Log("No Save File Found");
        //        return;
        //    }

        //    byte[] bytes = File.ReadAllBytes(filePath);

        //    //using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    //{
        //    //    bytes = new byte[stream.Length];
        //    //    stream.Read(bytes, 0, bytes.Length);
        //    //}

        //    GameSaveData save = SerializationUtility.DeserializeValue<GameSaveData>(bytes, DataFormat.Binary);

        //    if (save == null)
        //    {
        //        Debug.LogError("Save file corrupted");
        //        return;
        //    }

        //    foreach (var manager in managers)
        //    {
        //        manager.LoadData(save);
        //    }

        //    Debug.Log("Game Loaded (Binary)");
        //}


        //public void SaveGameWithMemoryPack()
        //{
        //    GameSaveDataMemoryPack save = new GameSaveDataMemoryPack();

        //    foreach (var manager in managers)
        //    {
        //        manager.SaveWithMemoryPack(save);
        //    }

        //    byte[] bytes = MemoryPackSerializer.Serialize(save);

        //    File.WriteAllBytes(filePath, bytes);
        //}

        //public void LoadGameWithMemoryPack()
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        Debug.Log("No Save File Found");
        //        return;
        //    }

        //    byte[] bytes = File.ReadAllBytes(filePath);
        //    GameSaveDataMemoryPack save = MemoryPackSerializer.Deserialize<GameSaveDataMemoryPack>(bytes);

        //    if (save == null)
        //    {
        //        Debug.LogError("Save file corrupted");
        //        return;
        //    }

        //    foreach (var manager in managers)
        //    {
        //        manager.LoadDataWithMemoryPack(save);
        //    }

        //}
    }
}
