using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviour
{
    public GameObject photonPrefab;
    public float spawnInterval;
    public int maxXspawnPosition;
    public int minXspawnPosition;
    float spawnTimer;

    Vector3 spawnPosition;


    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            spawnPosition = new Vector3(Random.Range(minXspawnPosition, maxXspawnPosition), transform.position.y, 0);
            Instantiate(photonPrefab, spawnPosition, Quaternion.identity, transform);
            spawnTimer = 0;
        }
    }

    void CheckForStateChange()
    {
        //foreach (Card card in CardManager.instance.cardsOnField)
        //{
        //    if (card.type == Card.cardType.GridConductor) { } //semi absorb
        //    if (card.type == Card.cardType.NTypeSilicon) { } //reacts with photons, a few get reflected, electrons activated
        //    if (card.type == Card.cardType.PTypeSilicon) { } //a few get reflected
        //    if (card.type == Card.cardType.Conductor) 
        //    {
        //        foreach (PhotonBehavior photon in GetComponentsInChildren<PhotonBehavior>())
        //        {
        //            photon.UpdatePhotonState(PhotonBehavior.PhotonState.Absorbed)
        //        }
        //    } //blocks

                
        //}
    }
}
