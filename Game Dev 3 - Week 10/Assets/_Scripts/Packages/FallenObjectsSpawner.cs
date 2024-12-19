using GameDevWithMarco.DesignPattern;
using GameDevWithMarco.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevWithMarco.Packages
{
    public class FallenObjectsSpawner : MonoBehaviour
    {
        [Header("Packages Pawn Position")]
        [SerializeField] GameObject[] spawners;
        [Header("Package Delay Variables ")]
        [SerializeField] float initialDelay;
        [SerializeField] float minDelay;
        [SerializeField] float delayIncreaseRate;
        float currentDelay;
        [Header("Packages Drop Chance Percentages")]
        [SerializeField] float goodPackageDropPercentage;
        [SerializeField] float badPackageDropPercentage;
        [SerializeField] float lifePackageDropPercentage;
        [SerializeField] float minimum_GoodPackagePercentage;
        [SerializeField] float maximum_BadPackageDropPercentage;
        [SerializeField] float percentageChangeRatio;

        void Start()
        {
            StartCoroutine(SpawningLoop());
        }

        public void GrowBadPercentage()
        {
            goodPackageDropPercentage -= percentageChangeRatio;
            badPackageDropPercentage += percentageChangeRatio;
            CapThePercentages();
        }
        public void GrowGoodPercentage()
        {
            goodPackageDropPercentage -= percentageChangeRatio;
            badPackageDropPercentage += percentageChangeRatio;
            CapThePercentages();
        }

        private void SpawnPackageAtRandomLoaction(ObjectPoolingPattern.TypeOfPool poolType)
        {
            GameObject spawnedPackage = ObjectPoolingPattern.Instance.GetPoolItem(poolType);
            int randomInterger = Random.Range(0, spawners.Length -1);
            Vector2 spawnPosion = spawners[randomInterger].transform.position;
            spawnedPackage.transform.position = spawnPosion;
        }

        private IEnumerator SpawningLoop()
        {
            SpawnPackageAtRandomLoaction(ObjectPoolingPattern.TypeOfPool.Good);
            yield return new WaitForSeconds(currentDelay);
            currentDelay -= delayIncreaseRate;
            if (currentDelay < minDelay) currentDelay = minDelay;
            StartCoroutine(SpawningLoop());
        }

        private ObjectPoolingPattern.TypeOfPool GetPackageBasedOnPercentage()
        {
            float randomValue = Random.Range(0, 100.1f);

            if (randomValue <= goodPackageDropPercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Good;
            }
            else if (randomValue >= goodPackageDropPercentage &&
                randomValue <= (goodPackageDropPercentage + badPackageDropPercentage))
            {
                return ObjectPoolingPattern.TypeOfPool.Bad;
            }
            else
            {
                return ObjectPoolingPattern.TypeOfPool.Life;
            }
        }

        private void CapThePercentages()
        {
            if (goodPackageDropPercentage <= minimum_GoodPackagePercentage
                && badPackageDropPercentage >= maximum_BadPackageDropPercentage)
            {
                goodPackageDropPercentage = minimum_GoodPackagePercentage;
                badPackageDropPercentage = maximum_BadPackageDropPercentage;
            }
        }

    }
}
