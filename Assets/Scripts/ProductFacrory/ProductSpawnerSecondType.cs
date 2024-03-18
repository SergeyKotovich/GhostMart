using UnityEngine;

public class ProductSpawnerSecondType : ProductSpawnerFirstType
{
    [SerializeField] private StorageProductsForInteraction _storageProductsForInteraction;
    
    protected override void Update()
    {
        if (CanNotSpawn())
        {
            return;
        }
        _storageProductsForInteraction.DestroyProduct();
        SpawnProduct();
    }

    protected override bool CanNotSpawn()
    {
        if (!_storageProductsForInteraction.HasProductsForInteraction())
        {
            return true;
        }
        return base.CanNotSpawn();
    }
}