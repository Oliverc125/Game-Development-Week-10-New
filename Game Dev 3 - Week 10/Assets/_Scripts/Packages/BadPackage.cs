using UnityEngine;
using GameDevWithMarco.DesignPattern;
using GameDevWithMarco.Interfaces;

namespace GameDevWithMarco.Packages
{
    public class BadPackage : MonoBehaviour, ICollidable
    {
        [SerializeField] GameEvent goodPackageCollected;
        public void CollidedLogic()
        {
            goodPackageCollected.Raise();
        }

    }
}
