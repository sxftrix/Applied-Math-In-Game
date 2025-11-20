using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    private IShapeGenerator currentGenerator;
    private GameObject currentShapeObject;

    public float rotationSpeed = 30f; // degrees per second for ShapeRotater

    public void GeneratePyramid()
    {
        SetGenerator(new PyramidGenerator());
    }

    public void GenerateCylinder()
    {
        SetGenerator(new CylinderGenerator());
    }

    public void GenerateRectangularColumn()
    {
        SetGenerator(new RectangleGenerator());
    }

    public void GenerateSphere()
    {
        SetGenerator(new SphereGenerator());
    }

    public void EraseShape()
    {
        if (currentGenerator != null)
            currentGenerator.Erase();

        if (currentShapeObject != null)
            GameObject.Destroy(currentShapeObject);

        currentGenerator = null;
        currentShapeObject = null;
    }

    private void SetGenerator(IShapeGenerator generator)
    {
        EraseShape();

        currentGenerator = generator;
        currentShapeObject = currentGenerator.Generate(Vector3.zero);

        if (currentShapeObject != null)
        {
            // Add ShapeRotater if not already added
            ShapeRotater rotater = currentShapeObject.GetComponent<ShapeRotater>();
            if (rotater == null)
                rotater = currentShapeObject.AddComponent<ShapeRotater>();

            // Set rotation speed from manager
            rotater.rotationSpeed = rotationSpeed;
        }
    }
}