using UnityEngine;

namespace Boss
{
    [RequireComponent(typeof(Collider))]
    public class BossTrigger : MonoBehaviour
    {
        [SerializeField] private BossBase boss;
        [SerializeField] private string playerTag = "Player";

        public GameObject bossCamera;

        private bool triggered = false;
        private bool cameraOn = false;

        private void Awake()
        {
            bossCamera.SetActive(false);
        }

        private void Reset()
        {
            // Garante que o collider deste objeto seja um trigger
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggered) return;
            if (!other.CompareTag(playerTag)) return;

            triggered = true;
            TurnCameraOn();
            boss.Activate();
                        
            // Opcional: desativa o próprio collider/trigger para não disparar de novo
            GetComponent<Collider>().enabled = false;
        }

        private void TurnCameraOn()
        { 
            bossCamera.SetActive(true);
        }
    }
}