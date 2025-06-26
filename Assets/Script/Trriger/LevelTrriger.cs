using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrriger : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] private string nextLevelName;

    void OnTriggerEnter2D(Collider2D _player)
    {
        if (((1 << _player.gameObject.layer) & playerLayer) != 0)
        {
            SaveSystem.SaveGame(GameManager.Instance.crrentLevel + 1, GameManager.Instance.lastPosition);

        }

        SceneManager.LoadScene(nextLevelName);
    }

}

