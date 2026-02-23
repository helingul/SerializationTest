using HelinTest.OdinSerializer;
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

    }

    [Serializable]
    public class GameSaveData
    {
        public int version = 1;

        public InventorySaveData inventorySavaData;
        public PlayerSaveData playerSaveData;
    }


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
        public void SaveGame()
        {
            GameSaveData save = new GameSaveData();

            foreach (var manager in managers)
            {
                manager.Save(save);
            }

            byte[] bytes = SerializationUtility.SerializeValue(
                save,
                DataFormat.JSON
            );

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            Debug.Log("Game Saved (Binary)");
        }

        public void LoadGame()
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("No Save File Found");
                return;
            }

            byte[] bytes;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }

            GameSaveData save =
                SerializationUtility.DeserializeValue<GameSaveData>(
                    bytes,
                    DataFormat.JSON
                );

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
    }
}
