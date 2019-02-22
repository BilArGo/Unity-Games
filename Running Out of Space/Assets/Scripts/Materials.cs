using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{

    public int oxygen;
    public int material;
    public int fuel;
    public TextMesh text;

    private void Start()
    {
        oxygen = Random.Range(20,30);
        fuel = Random.Range(10, 15);
        material = Random.Range(10, 15);

    }
    private void Update()
    {
        text.transform.rotation = Quaternion.identity;
    }


}
