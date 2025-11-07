using UnityEngine;

public class RectangleGenerator : IShapeGenerator
{
    private GameObject currentShape;

    public GameObject Generate(Vector3 position)
    {
        Erase();
        currentShape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        currentShape.transform.position = position;
        currentShape.transform.localScale = new Vector3(1, 3, 1);
        currentShape.name = "Rectangular Column";
        return currentShape;
    }

    public void Erase()
    {
        if (currentShape != null)
            GameObject.Destroy(currentShape);
    }
}
