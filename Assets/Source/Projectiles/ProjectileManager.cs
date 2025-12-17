using Core.Poolin;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileManager : MonoBehaviour
{
    [field:SerializeField]
    private GameObject BulletTemplate;
    [field:SerializeField]
    private int StartingObjectCount;
    [field:SerializeField]
    private int MaxObjectCount;
    private ObjectPool<ProjectileBehaviour> objectPool;
    public static ProjectileManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        objectPool = new(OnCreateAction,MaxObjectCount,StartingObjectCount);
    }
    private ProjectileBehaviour OnCreateAction()
    {
        GameObject newObj = Instantiate(BulletTemplate);
        newObj.SetActive(false);
        if (newObj.TryGetComponent(out ProjectileBehaviour behaviour))
        {
            return behaviour;
        }
        Debug.Log("Failed to create properly.");
        return null;
    }
    public void Shoot(Vector3 origin, Vector2 speed, AProjectileDataSO dataSO, string ignoredTag = "Player")
    {
        try
        {
            StartCoroutine(dataSO.CreateAction(objectPool,origin,speed,ignoredTag));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to create. Error message: " + e.Message);
        }
    }
}
