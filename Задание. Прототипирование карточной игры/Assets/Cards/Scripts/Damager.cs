using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Damager : MonoBehaviour
    {
        private Transform _card;

        public float _attackRange = 10f;


        public LayerMask _enemyLayer;

        public int Damage { get; set; }

        public void Attack()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(_card.position, _attackRange, _enemyLayer);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeDamage(Damage);
                Debug.Log(Damage);
            }
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_card.position, _attackRange);
        }

        void Start()
        {
            _card = GetComponent<Card>().transform;
        }

    }
}