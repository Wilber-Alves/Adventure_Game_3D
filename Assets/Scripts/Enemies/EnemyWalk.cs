using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {

        [Header("Way Points")]
        public GameObject[] waypoints;
        public float minDistance = 1f;
        public float speed = 3f;
        public float lookAtWaypointRotationSpeed = 8f; // TESTE, novo campo para suavizar rotação em direção ao waypoint
        private int _index = 0;

        public override void Update()
        {
            base.Update();

            // TESTE, guarda de seguranca contra array vazio ou nao atribuido no Inspector
            if (waypoints == null || waypoints.Length == 0) return;

            // TESTE, guarda de seguranca contra elemento nulo dentro do array
            if (waypoints[_index] == null) return;


            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if (_index >= waypoints.Length)
                {
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
            
            //transform.LookAt(waypoints[_index].transform.position); // TESTE, codigo original substituido por rotacao suave abaixo
            
            LookAtWaypoint();
        }
        protected void LookAtWaypoint() // TESTE, nova funcao para suavizar rotacao em direcao ao waypoint, evitando conflito visual com LookAtPlayer()
        {
            Vector3 direction = waypoints[_index].transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.0001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookAtWaypointRotationSpeed);
        }
    }
}