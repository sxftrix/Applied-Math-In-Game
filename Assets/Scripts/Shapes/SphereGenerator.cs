using UnityEngine;

public class SphereGenerator : IShapeGenerator
{
    private GameObject currentShape;

    public GameObject Generate(Vector3 position)
    {
        Erase();
        currentShape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        currentShape.transform.position = position;
        currentShape.name = "Sphere";
        return currentShape;
    }

    public void Erase()
    {
        if (currentShape != null)
            GameObject.Destroy(currentShape);
    }
}
