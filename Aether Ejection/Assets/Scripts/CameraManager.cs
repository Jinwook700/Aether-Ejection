using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float followThreshold = 7f;
    [SerializeField] private float smoothSpeed = 5f;

    private Transform player;

    private void Start()
    {
        player = Player.Instance.transform;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 pos = transform.position;
        Vector3 playerPos = player.position;

        Vector3 offset = playerPos - pos;

        if (Mathf.Abs(offset.x) > followThreshold)
            pos.x += Mathf.Sign(offset.x) * (Mathf.Abs(offset.x) - followThreshold);

        if (Mathf.Abs(offset.y) > followThreshold)
            pos.y += Mathf.Sign(offset.y) * (Mathf.Abs(offset.y) - followThreshold);

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothSpeed);
    }
}
