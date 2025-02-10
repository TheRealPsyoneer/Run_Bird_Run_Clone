using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Factory",menuName = "Factory SO")]
public class MyFactorySO : ScriptableObject
{
    [SerializeField] GameObject product;
    Stack<IFactoryProduct> productPool;
    [SerializeField] int poolCapacity;

    public void Initialize()
    {
        productPool = new();
        for (int i = 0; i < poolCapacity; i++)
        {
            MakeNewProduct();
        }
    }

    public IFactoryProduct GetProduct()
    {
        if (productPool.Count == 0)
        {
            MakeNewProduct();
        }

        IFactoryProduct instance = productPool.Pop();
        instance.Initialize();
        return instance;
    }

    private void MakeNewProduct()
    {
        GameObject newProduct = Instantiate(product);
        IFactoryProduct instance = newProduct.GetComponent<IFactoryProduct>();
        instance.pool = productPool;
        newProduct.SetActive(false);
    }
}
