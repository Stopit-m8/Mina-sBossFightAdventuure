using UnityEngine;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float missileDamage;
    [SerializeField] private GameObject rootParent;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float projectileDropDuration;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BossAttackArea _attackArea;
    private Collider2D _col;
    private Tween _dropTween;

    private void Awake()
    {
        transform.position = startPoint.position;
        _col = GetComponent<Collider2D>();
        _attackArea = GetComponent<BossAttackArea>();
    }

    private void Start()
    {
        _attackArea.SetDamage(missileDamage);
    }

    private void OnEnable()
    {
        _col.enabled = false;
        spriteRenderer.enabled = true;
        transform.position = startPoint.position;
        explosionVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        Drop();
    }

    private void OnDisable()
    {
        _dropTween?.Kill();
    }

    private void Drop()
    {
        _dropTween = transform.DOMove(endPoint.position, projectileDropDuration).OnComplete(Explode);
    }

    private void Explode()
    {
        Debug.Log("Explode");
        _col.enabled = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.Missile);
        explosionVFX.Play();
        spriteRenderer.enabled = false;
        StartCoroutine(WaitForExplosion());
    }

    private IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(explosionVFX.main.duration/2);
        _col.enabled = false;

        yield return new WaitForSeconds(explosionVFX.main.duration);
        rootParent.SetActive(false);
        
    }

}
