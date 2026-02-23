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

        public void UpdateAllValuesForTesting()
        {
            // Update list
            items.Clear();
            items.AddRange(new List<string> { "apple", "banana", "cherry" });

            // Update integer
            gold = 100;

            // Update dictionary
            testDictionary.Clear();
            testDictionary.Add("key1", 10);
            testDictionary.Add("key2", 20);

            // Update DateTime
            dateTime = DateTime.Now;

            // Update int array
            testIntArray = new int[] { 10, 20, 30, 40, 50 };

            // Update nested float array
            testNestedFloatArray = new float[][] {
            new float[] { 2.2f, 2.2f },
            new float[] { 3.3f, 3.3f }
        };
        }


        public void PrintAllValues()
        {
            Debug.Log("------------INVENTORY MANAGER-----------------");
            string logMessage = "";

            // List
            logMessage += "Items:\n";
            foreach (var item in items)
            {
                logMessage += "- " + item + "\n";
            }

            // Integer
            logMessage += $"Gold: {gold}\n";

            // Dictionary
            logMessage += "Dictionary:\n";
            foreach (var kvp in testDictionary)
            {
                logMessage += $"- {kvp.Key}: {kvp.Value}\n";
            }

            // DateTime
            logMessage += $"DateTime: {dateTime}\n";

            // Int array
            logMessage += "Int Array:\n";
            foreach (var num in testIntArray)
            {
                logMessage += "- " + num + "\n";
            }

            // Nested float array
            logMessage += "Nested Float Array:\n";
            for (int i = 0; i < testNestedFloatArray.Length; i++)
            {
                logMessage += $"Row {i}: ";
                for (int j = 0; j < testNestedFloatArray[i].Length; j++)
                {
                    logMessage += testNestedFloatArray[i][j] + " ";
                }
                logMessage += "\n";
            }

            // Single Debug.Log call
            Debug.Log(logMessage);
        }
       
    }

}