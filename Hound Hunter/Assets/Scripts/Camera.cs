using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}