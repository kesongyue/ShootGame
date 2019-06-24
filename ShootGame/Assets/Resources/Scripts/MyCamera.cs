using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private Vector3 m_TargetPosition;
    Transform follow;

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_TargetPosition = follow.position + new Vector3(0, 1.0f, -1.0f);
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime);
        //transform.LookAt(follow);
    }

}
