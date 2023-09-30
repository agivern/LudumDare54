using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStarsCoverScreen : MonoBehaviour
{
  public Camera mainCamera;

  private ParticleSystem.ShapeModule particleShapeModule;

  private void Start()
  {
    if (!mainCamera) mainCamera = Camera.main;

    particleShapeModule = GetComponent<ParticleSystem>().shape;

    AdjustParticleSize();
  }

  void AdjustParticleSize()
  {
    float height = mainCamera.orthographicSize * 2.0f;
    float width = height * mainCamera.aspect;

    particleShapeModule.scale = new Vector3(width, height, 1);
  }
}