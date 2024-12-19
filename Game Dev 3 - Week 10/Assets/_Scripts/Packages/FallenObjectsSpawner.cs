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
        [SerializeField] float initialDelay = 2.0f;
        [SerializeField] float minDelay = 0.5f;
        [SerializeField] float delayIncreaseRate = 0.1f;
        float currentDelay;
        [Header("Packages Drop Chance Percentages")]
        [SerializeField] float goodPackageDropPercentage = 70f;
        [SerializeField] float badPackageDropPercentage = 25f;
        [SerializeField] float lifePackageDropPercentage = 5f;
        [SerializeField] float minimum_GoodPackagePercentage;
        [SerializeField] float maximum_BadPackageDropPercentage;
        [SerializeField] float percentageChangeRatio = 0.1f;

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
            goodPackageDropPercentage += percentageChangeRatio;
            badPackageDropPercentage -= percentageChangeRatio;
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
            float randomValue = Random.Range(0f, 100.1f);

            if (randomValue <= goodPackageDropPercentage)
            {
                return ObjectPoolingPattern.TypeOfPool.Good;
            }
            else if (randomValue > goodPackageDropPercentage &&
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
