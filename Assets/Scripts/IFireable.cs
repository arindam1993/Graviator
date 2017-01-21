using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnFireableExpiredDelegate();
public interface IFireable  {

    void Initialize(OnFireableExpiredDelegate cb);
    void OnFireDown();
    void OnFireHeld();
}
