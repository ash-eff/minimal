using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int changeTimer;
    [SerializeField]
    private int adjustAmount;

    GameController gc;

    private float spawnHeight;
    private float gameWidth;
    private float spawnWidth;


    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        spawnHeight = Camera.main.orthographicSize + 1f;
        gameWidth = Camera.main.orthographicSize * Camera.main.aspect / 3 - 1;
    }

    private void Start()
    {
        StartCoroutine(SpawnBlocks());
    }

    private void Update()
    {
        if(Time.time > changeTimer)
        {
            changeTimer += adjustAmount;
            float adjust = spawnTime * .2f;

            if (spawnTime - adjust <= .5f)
            {
                spawnTime = .5f;
            }
            else
            {
                spawnTime -= adjust;
            }
        }
    }

    IEnumerator SpawnBlocks()
    {
        while (!gc.PlayerDead)
        {
            spawnWidth = Random.Range(-gameWidth, gameWidth);
            Instantiate(blockPrefab, new Vector2(spawnWidth, spawnHeight), Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
