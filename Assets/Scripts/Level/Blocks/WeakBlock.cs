using UnityEngine;
using Common;

namespace LevelSystem
{
    /// <summary>
    /// Weak block which brokes on blast
    /// </summary>
    public class WeakBlock : MonoBehaviour, IDamage
    {
        LevelManager levelManager;

        public void SetLevelManager(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }


        /// <summary>
        /// getting damage by blast
        /// </summary>
        public void Damage()
        {
            levelManager.EmptyGrid(transform.position);
            Destroy(gameObject);
        }
    }
}