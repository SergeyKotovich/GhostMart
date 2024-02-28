using UnityEngine;

public class ProductSpawnerSecondType : ProductSpawner
{
    [SerializeField] private StorageProductsForInteraction _storageProductsForInteraction;
    private int _maxCountSpawnedProduct = 4;
    private float _delayBetweenSpawnObjects = 6;
    protected override void ProductSpawn()
    {
        if (!_storageProductsForInteraction.HasProductsForInteraction())
        {
            return;
        }
        base.ProductSpawn();
    }

    

    
}