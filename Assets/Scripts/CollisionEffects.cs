using UnityEngine;

public class CollisionEffects : MonoBehaviour
{
    [SerializeField] private GameObject _particleHitEffect;

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_particleHitEffect, other.GetContact(0).point, Quaternion.identity);
    }
}
