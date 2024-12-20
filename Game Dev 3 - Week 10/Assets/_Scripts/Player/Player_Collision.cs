using GameDevWithMarco.Interfaces;
using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class Player_Collision : MonoBehaviour
    {
        [SerializeField] GameEvent goodPackageCollected;
        [SerializeField] GameEvent badPackageCollected;
        [SerializeField] GameEvent lifePackageCollected;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            ExecuteLogicBasedOnWhatWeHaveCollidedWith(collision);
            collision.gameObject.SetActive(false);  
        }

        private void ExecuteLogicBasedOnWhatWeHaveCollidedWith(Collider2D collision)
        {
            ICollidable collidable = collision.GetComponent<ICollidable>();
            if (collidable != null)
            {
                collidable.CollidedLogic();
            }
        }
    }
}
