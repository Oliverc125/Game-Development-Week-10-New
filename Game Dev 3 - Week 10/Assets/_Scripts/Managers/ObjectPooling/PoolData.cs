using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco.DesignPattern
{
    [CreateAssetMenu(fileName = "Pool", menuName = "Scriptable Objects/Pool")]
    public class PoolData : ScriptableObject
    {
        
        public int poolAmount = 40;
      
        public GameObject poolItem;
    }
}
