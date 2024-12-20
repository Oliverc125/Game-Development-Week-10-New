using UnityEngine;
using GameDevWithMarco.DesignPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class LifePackage : MonoBehaviour, ICollidable
    {
        [SerializeField] GameEvent lifePackageCollected;
        public void CollidedLogic()
        {
            lifePackageCollected.Raise();
        }
    }
}
