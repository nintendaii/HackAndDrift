/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private Transform playerTransform;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Enemy.Create(playerTransform.position + UtilsClass.GetRandomDir() * Random.Range(50f, 100f));
        }
    }

}
