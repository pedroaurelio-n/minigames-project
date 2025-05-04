using UnityEngine;

public class CameraProvider : ICameraProvider
{
    //TODO pedro: create ICamera component/interface/behaviour
    public Camera MainCamera { get; private set; }
    
    public void SetMainCamera(Camera mainCamera)
    {
        MainCamera = mainCamera;
    }
    
    public bool IsContainedInCameraBounds(Renderer renderer, float margin = 0.05f)
    {
        if (MainCamera == null || renderer == null)
            return false;

        Bounds bounds = renderer.bounds;
        Vector3[] corners = new Vector3[8];

        Vector3 extents = bounds.extents;
        Vector3 center = bounds.center;
        int i = 0;
        for (int x = -1; x <= 1; x += 2)
        for (int y = -1; y <= 1; y += 2)
        for (int z = -1; z <= 1; z += 2)
            corners[i++] = center + Vector3.Scale(extents, new Vector3(x, y, z));

        float minX = margin;
        float maxX = 1f - margin;
        float minY = margin;
        float maxY = 1f - margin;

        foreach (Vector3 corner in corners)
        {
            Vector3 viewportPoint = MainCamera.WorldToViewportPoint(corner);

            if (viewportPoint.z < 0 || 
                viewportPoint.x < minX || viewportPoint.x > maxX || 
                viewportPoint.y < minY || viewportPoint.y > maxY)
                return false;
        }

        return true;
    }
}