using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalSaveSystem
{
    [Serializable]
    public class InventorySaveData
    {
        public List<string> items = new List<string>();
        public int gold;
        public Dictionary<string, int> testDictionary = new Dictionary<string, int>();
        public DateTime dateTime;
        public int[] testIntArray = { 1, 2, 3, 4, 5 };

        public float[][] testNestedFloatArray = new float[][] {
                new float[] { 1.1f, 1.1f },
                new float[] { 1.1f, 1.1f }
            };
    }

    public class InventoryManager : InventorySaveData, ISaveable
    {
        public void Save(GameSaveData saveData)
        {
            saveData.inventorySavaData = this;
        }

        public void LoadData(GameSaveData saveData)
        {

            ReflectionHelper.CopyFields(this, saveData.inventorySavaData);

            //items = saveData.inventorySavaData.items;
            //gold = saveData.inventorySavaData.gold;
            //testDictionary = saveData.inventorySavaData.testDictionary;
            //dateTime = saveData.inventorySavaData.dateTime;
            //testIntArray = saveData.inventorySavaData.testIntArray;
            //testNestedFloatArray = saveData.inventorySavaData.testNestedFloatArray;
            //testNestedFloatArray = saveData.inventorySavaData.testNestedFloatArray;
        }
    }

}