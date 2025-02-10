using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactoryProduct
{
    Stack<IFactoryProduct> pool { get; set; }

    public void Initialize();

    public void ReturnToPool();
}
