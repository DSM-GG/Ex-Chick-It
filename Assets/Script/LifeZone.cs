﻿using System.Collections;
using UnityEngine;

public class LifeZone : MonoBehaviour {

    [System.Serializable]
    private struct Status {
        public float scale;
        public float speed;
        public float waitSec;
    }

    [SerializeField]
    private Status[] status;
    private int nowStage;

    private void Start() {
        StartCoroutine("LimitCount");
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            col.GetComponent<Player>().Death();
        }

        else if (col.CompareTag("Destroy Wall")) {
            if (col.transform.childCount >= 0)
                Destroy(col.transform.GetChild(0).gameObject);
        }

        else if (col.CompareTag("Escape")) {
            Debug.Log("ddd");
            Destroy(col.gameObject);
        }
    }

    private IEnumerator LimitCount() {
        for (int count = 0; count < status.Length; count++) {
            yield return new WaitForSeconds(status[nowStage].waitSec);

            var destPos = (Random.insideUnitCircle * status[nowStage].scale * 0.03f) + (Vector2)transform.position;
            var destScale = Vector2.one * status[nowStage].scale;

            var originPos = transform.position;
            var originScale = transform.localScale;

            for (float i = 0; i < 1; i += status[nowStage].speed * Time.deltaTime) {
                transform.position = Vector2.Lerp(originPos, destPos, i);
                transform.localScale = Vector2.Lerp(originScale, destScale, i);
                yield return null;
            }

            transform.position = destPos;
            transform.localScale = destScale;

            nowStage++;
        }
    }
}
