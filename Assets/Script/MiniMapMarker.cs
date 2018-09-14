using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMarker : MonoBehaviour {

    RectTransform me;

    [SerializeField]
    private Transform player;

    private void Awake() {
        me = GetComponent<RectTransform>();
    }

    void Update () {
        me.anchoredPosition = player.position * 5;
	}
}
