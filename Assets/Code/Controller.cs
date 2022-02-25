using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class Controller : MonoBehaviour {
    [SerializeField]
    private Transform player, innerCircle, outerCircle;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Sprite normalSprite, attackSprite;
    [SerializeField]
    private Button attackButton;

    private bool touched;
    private Vector2 pointA, pointB;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        attackButton.onClick.AddListener(() => StartCoroutine(OnAttack()));
    }

    private IEnumerator OnAttack() {
        attackButton.gameObject.SetActive(false);
        Debug.Assert(spriteRenderer != null, nameof(spriteRenderer) + " != null");
        spriteRenderer.sprite = attackSprite;
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.sprite = normalSprite;
        attackButton.gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            innerCircle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            innerCircle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Input.GetMouseButton(0)) {
            touched = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            touched = false;
        }
    }

    private void FixedUpdate() {
        if (touched) {
            Vector2 offset = pointB - pointA;
            var direction = Vector2.ClampMagnitude(offset, 1.0f);
            Move(direction);

            innerCircle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        } else {
            innerCircle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Move(Vector2 direction) => player.Translate(direction * (speed * Time.deltaTime));
}
