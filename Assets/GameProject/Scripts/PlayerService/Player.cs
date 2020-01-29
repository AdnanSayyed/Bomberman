﻿using UnityEngine;
using Common;
using EnemySystem;

namespace PlayerSystem
{
    public class Player : MonoBehaviour , IDamage
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Rigidbody2D myBody;
        [SerializeField] private Transform playerMainTransform;

        private PlayerController playerController;
        private float horizontalVal, verticalVal;

      
        private void Update()
        {
            MoveDirection();

            if (Input.GetKeyDown(KeyCode.Space)) SpawnBomb();
        }

        private void MoveDirection()
        {
            horizontalVal = Input.GetAxis("Horizontal");
            verticalVal = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontalVal) > Mathf.Abs(verticalVal))
            {
                if (horizontalVal > 0 && playerMainTransform.rotation.z != 90)
                    playerMainTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                else if (horizontalVal < 0 && playerMainTransform.rotation.z != 270)
                    playerMainTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

                verticalVal = 0;
            }

            if (Mathf.Abs(verticalVal) > Mathf.Abs(horizontalVal))
            {
                if (verticalVal > 0 && playerMainTransform.rotation.z != 180)
                    playerMainTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (verticalVal < 0 && playerMainTransform.rotation.z != 0)
                    playerMainTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                horizontalVal = 0;
            }
        }

        void SpawnBomb() => playerController.SpawnBomb();

        void FixedUpdate()
        {
            myBody.velocity = new Vector2(horizontalVal, verticalVal) * moveSpeed;
        }

        public void Damage() => playerController.PlayerKilled();

        public void SetController(PlayerController playerController) =>
            this.playerController = playerController;


        void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<Enemy>() != null)
            {
                Damage(); 
            }
        }

    }
}