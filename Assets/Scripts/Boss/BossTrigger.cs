using UnityEngine;

namespace Boss
{
    [RequireComponent(typeof(Collider))]
    public class BossTrigger : MonoBehaviour
    {
        [SerializeField] private BossBase _boss;
        [SerializeField] private EnemyBoss _enemyBoss;
        [SerializeField] private string playerTag = "Player";

        public GameObject bossCamera;

        private bool triggered = false;

        private void Awake()
        {
            bossCamera.SetActive(false);
     
            if (_enemyBoss != null)
                _enemyBoss.OnBossKilled += OnBossKilled;
        }

        private void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggered) return;
            if (!other.CompareTag(playerTag)) return;

            triggered = true;
            TurnCameraOn();
            _boss.Activate();

            GetComponent<Collider>().enabled = false;
        }

        private void TurnCameraOn()
        {
            bossCamera.SetActive(true);
        }

        private void OnBossKilled(EnemyBoss e)
        {

            bossCamera.SetActive(false);

        }
    }
}