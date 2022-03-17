using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void Save()
    {
        FindObjectOfType<Player>().Heal();
        GameManager.Instance.RegisterCheckpoint(transform);
    }
}
