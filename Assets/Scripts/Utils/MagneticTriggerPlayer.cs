
using UnityEngine;
using Items;

public class MagneticTriggerPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
