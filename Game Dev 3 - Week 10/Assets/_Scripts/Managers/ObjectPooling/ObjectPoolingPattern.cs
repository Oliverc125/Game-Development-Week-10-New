using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.Managers;

namespace GameDevWithMarco.DesignPattern
{
    public class ObjectPoolingPattern : Singleton<ObjectPoolingPattern>
    {
        [SerializeField] PoolData goodPackagePoolData;
        [SerializeField] PoolData badPackagePoolData;
        [SerializeField] PoolData lifePackagePoolData;

        public List<GameObject> goodPool = new List<GameObject>();
        public List<GameObject> badpool = new List<GameObject>();
        public List<GameObject> lifepool = new List<GameObject>();

        public enum TypeOfPool
        {
            Good,
            Bad,
            Life
        }

        protected override void Awake()
        {
            FillThePool(goodPackagePoolData, goodPool);
            FillThePool(badPackagePoolData, badpool);
            FillThePool(lifePackagePoolData, lifepool);
        }

        private void FillThePool(PoolData poolData, List<GameObject> targetPool)
        {
            GameObject container = CreateAContainerForThePool(poolData);

            //Goes as many time as we want the pool amount to be
            for (int i = 0; i < poolData.poolAmount; i++)
            {
                //Instantiates on item in the pool
                GameObject thingToAddToThePool = Instantiate(poolData.poolItem);
                //Sets the patent to be what this script is attached to
                thingToAddToThePool.transform.parent = transform;
                //Deactivates it 
                thingToAddToThePool.SetActive(false);
                //Adds it to the pool container list
                targetPool.Add(thingToAddToThePool);
            }
        }

        private GameObject CreateAContainerForThePool(PoolData poolData)
        {
            GameObject container = new GameObject();
            container.transform.SetParent(this.transform);
            container.name = poolData.name;
            return container;
        }

        public GameObject GetPoolItem(TypeOfPool typeOfPoolToUse)
        {
            List<GameObject> poolToUse = new();

            switch (typeOfPoolToUse)
            {
                case TypeOfPool.Good:
                    poolToUse = goodPool;
                    break;
                case TypeOfPool.Bad:
                    poolToUse = badpool;
                    break;
                case TypeOfPool.Life:
                    poolToUse = lifepool;
                    break;
            }

            int itemPoolCount = poolToUse.Count;

            for (int i = 0; itemPoolCount > 0; i++)
            {
                //Looks for the first item that is not active
                if (!poolToUse[i].activeInHierarchy)
                {
                    poolToUse[i].SetActive(true);
                    return poolToUse[i];
                }
            }

            Debug.LogWarning("No Availeble Items Found, Pool Too Small!");

            return null;
        }
    }
}
