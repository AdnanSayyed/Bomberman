using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace LevelSystem
{
    public class WeakBlock : MonoBehaviour, IDamage
    {
        LevelManager levelManager;

        public void SetLevelService(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }

        public void Damage()
        {
            levelManager.EmptyGrid(transform.position);
            Destroy(gameObject);
        }
    }
}