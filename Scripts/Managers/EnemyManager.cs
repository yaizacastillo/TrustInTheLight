using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager> {

    #region Public Variables
    [Header("\tGame Designers Variables")]
    [Tooltip("Cantidad inicial de enemigos para tener una pool inicial")]
    public int initialAmountOfEnemies = 10;

    [Header("\t    Own Script Variables")]
    [Tooltip("Prefab del enemigo")]
    public GameObject enemyPrefab;
    #endregion

    #region Private Variables
    private List<GameObject> enemyPoolList = new List<GameObject>();
    #endregion

    private void Start()
    {
        CreateStarterGhosts();
    }

    #region Creation Method
    private void CreateStarterGhosts()
    {
        for (int i = 0; i < initialAmountOfEnemies; i++)
        {
            GameObject go = Instantiate(enemyPrefab, Vector3.right * 1000, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            go.SetActive(false);
            enemyPoolList.Add(go);
        }
    }
    #endregion

    #region Enemy Managment Methods
    public GameObject GetEnemy(Vector3 position)
    {
        GameObject g;

        if (enemyPoolList.Count == 0)
        {
            //Create
            g = Instantiate(enemyPrefab, Vector3.right * 1000, Quaternion.identity) as GameObject;
        }
        else
        {
            //Get the first and remove it
            g = enemyPoolList[0];
            enemyPoolList.Remove(g);
        }

        g.transform.position = position;
        g.SetActive(true);

        g.GetComponent<Enemy>().StartBehaviour();

        return g;
    }

    public void ReturnEnemy(GameObject g)
    {
        //Reset ghost
        g.transform.position = Vector3.one * 9999;
        g.SetActive(false);

        enemyPoolList.Add(g);
    }
    #endregion

}
