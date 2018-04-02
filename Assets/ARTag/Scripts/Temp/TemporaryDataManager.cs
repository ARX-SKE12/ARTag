
namespace ARTag
{
    using System.Collections.Generic;
    using UnityEngine;

    public class TemporaryDataManager : MonoBehaviour
    {

        Dictionary<string, object> tempData;

        // Use this for initialization
        void Awake()
        {
            tempData = new Dictionary<string, object>();
        }
        
        public void Put(string key, object data)
        {
            tempData[key] = data;
        }

        public object Get(string key)
        {
            return tempData[key];
        }

        public bool Has(string key)
        {
            return tempData.ContainsKey(key);
        }

        public void Delete(string key)
        {
            tempData.Remove(key);
        }

    }

}
