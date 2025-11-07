using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    private IShapeGenerator currentGenerator;

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

    /* public void GenerateCapsule()
    {
        SetGenerator(new CapsuleGenerator());
    } */

    public void EraseShape()
    {
        if (currentGenerator != null)
            currentGenerator.Erase();
    }

    private void SetGenerator(IShapeGenerator generator)
    {
        if (currentGenerator != null)
            currentGenerator.Erase();

        currentGenerator = generator;
        currentGenerator.Generate(Vector3.zero);
    }
}
