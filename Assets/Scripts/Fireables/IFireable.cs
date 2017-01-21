using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnFireableExpiredDelegate();
public interface IFireable  {

    void Initialize(OnFireableExpiredDelegate cb, Transform firePoint, int PlayerIndex);
    void OnFireDown();
    void OnFireHeld();
}
