using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnFireableExpiredDelegate();
public interface IFireable  {

    void Initialize(OnFireableExpiredDelegate cb, Transform firePoint);
    void OnFireDown();
    void OnFireHeld();
}
