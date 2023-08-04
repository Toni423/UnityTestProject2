using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Buildable
{
    [SerializeField] private LayerMask inView;
    private ContactFilter2D _viewContactFilter = new ContactFilter2D();
    private CircleCollider2D _fieldOfView;
    private Vector2 _startingPosition;
    [SerializeField] private float movementSpeed;
    
    private void Awake() {
        _startingPosition = transform.position;
        _viewContactFilter.SetLayerMask(inView);
        _fieldOfView = GetComponent<CircleCollider2D>();
    }
    
    private void FixedUpdate() {
        move();
    }
    
    
    private void move() {
        Collider2D temp = detectNearestObject();
        Vector2 target = temp == null ? _startingPosition : temp.transform.position;
        Vector2 currentPos = transform.position;
        
        // _rb.AddForce(Time.fixedDeltaTime * movementSpeed * ((detectNearestObject()).normalized), ForceMode2D.Force);
        transform.position = Vector2.MoveTowards(currentPos, target, movementSpeed * Time.fixedDeltaTime);
    }


    private Collider2D detectNearestObject() {
        Collider2D[] inView = new Collider2D[1];
        _fieldOfView.OverlapCollider(_viewContactFilter, inView);
        Vector2 currentPos = transform.position;

        Vector2 shortestVector = (Vector2) Vector2.positiveInfinity;
        Collider2D result = null;
        

        if (inView.Length == 0) {
            return result;
        }
        
        foreach (Collider2D curr in inView) {
            if (curr == null) {
                break;
            }
            Vector2 currWay = (Vector2) curr.transform.position - currentPos;

            if (currWay.magnitude < shortestVector.magnitude) {
                shortestVector = currWay;
                result = curr;
            }
        }
        
        return result;
    }
    
    
    
}
