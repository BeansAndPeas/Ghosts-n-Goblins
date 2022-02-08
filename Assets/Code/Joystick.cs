using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour {
    [SerializeField]
    private Transform player, innerCircle, outerCircle;
    [SerializeField]
    private float speed = 5f;
    private bool touched = false;
    private Vector2 pointA, pointB;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            this.pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            this.innerCircle.transform.position = pointA;
            this.outerCircle.transform.position = pointA;
            this.innerCircle.GetComponent<SpriteRenderer>().enabled = true;
            this.outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Input.GetMouseButton(0)) {
            this.touched = true;
            this.pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            this.touched = false;
        }
    }

    private void FixedUpdate() {
        if (this.touched) {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            Move(direction);

            this.innerCircle.transform.position = new Vector2(this.pointA.x + direction.x, this.pointA.y + direction.y);
        } else {
            this.innerCircle.GetComponent<SpriteRenderer>().enabled = false;
            this.outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Move(Vector2 direction) {
        player.Translate(direction * speed * Time.deltaTime);
    }
}
