using System;
using System.Collections;
using Core.Poolin;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour , IPoolItem<ProjectileBehaviour>
{
    public event Action<ProjectileBehaviour> OnObjectDeathRequest;
    internal AProjectileDataSO projectileData;
    private string ignoredTag;
    private bool Active = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ignoredTag) == true || !Active || collision.isTrigger)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TryDamage(projectileData.Damage);
        }
        Disable();
    }
    public void OnActivationEvent()
    {
        Active = true;
        gameObject.SetActive(true);
        StartCoroutine(LifetimeEnumerator(2));
    }
    public void Init(AProjectileDataSO newData,string IgnoredTag)
    {
        projectileData = newData;
        StartCoroutine(LifetimeEnumerator(projectileData.LifeTime));
        if (gameObject.TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.color = projectileData.color;
        }
        ignoredTag = IgnoredTag;
    }
    void Disable()
    {
        Active = false;
        gameObject.SetActive(false);
        OnObjectDeathRequest.Invoke(this);
    }
    IEnumerator LifetimeEnumerator(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        if (gameObject.activeInHierarchy)
            Disable();
    }
}
