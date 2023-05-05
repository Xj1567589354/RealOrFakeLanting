using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dictionary_Add
{
    public class Dictionary_Keys : MonoBehaviour
    {
        public Dictionary<string, string> dictionary;
        public void Start()
        {
            dictionary = new Dictionary<string, string>();
        }

        public void Update()
        {
            dictionary.Add("WaterBox", "Ë®Í°");
            dictionary.Add("Handle", "µÐÈË");
        }
    }
}
