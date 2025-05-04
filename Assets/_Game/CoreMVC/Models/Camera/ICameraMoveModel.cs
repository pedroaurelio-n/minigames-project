using System;
using UnityEngine;

public interface ICameraMoveModel
{
    void MoveCamera (Vector2 moveVector);

    void ZoomCamera (
        float zoomAmount,
        float minZoom = Mathf.NegativeInfinity,
        float maxZoom = Mathf.Infinity
    );
}