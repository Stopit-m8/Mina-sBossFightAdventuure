using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    private List<GameObject> projectilePooling = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(projectilePrefab, this.transform);
            obj.SetActive(false);
            projectilePooling.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < projectilePooling.Count; i++)
        {
            if (!projectilePooling[i].activeInHierarchy)
            {
                return projectilePooling[i];
            }
        }
        return null;
    }
}
