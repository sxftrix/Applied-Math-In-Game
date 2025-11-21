using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    private MeshController platformController;
    private MeshController cubeController;

    public Material shapeMaterial;

    private void Start()
    {
        SpawnPlatform();
        SpawnCube();
    }

    void SpawnPlatform()
    {
        var generator = new PlatformGen();

        platformController = CreateShapeController(Vector3.zero, "Platform", null);

        Mesh mesh = generator.GenerateMesh();
        platformController.SetMeshInstant(mesh);
        platformController.SetMaterial(shapeMaterial);
        platformController.transform.localScale = Vector3.one;

        platformController.gameObject.AddComponent<PlatformPhysics>();
    }

    void SpawnCube()
    {
        var generator = new CubeGen();

        cubeController = CreateShapeController(new Vector3(0, 5f, 0), "Cube", null);

        Mesh mesh = generator.GenerateMesh();
        cubeController.SetMeshInstant(mesh);
        cubeController.SetMaterial(shapeMaterial);
        cubeController.transform.localScale = Vector3.one;

        cubeController.gameObject.AddComponent<CubePhysics>();
    }

    MeshController CreateShapeController(Vector3 pos, string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent);
        go.transform.localPosition = pos;

        return go.AddComponent<MeshController>();
    }
}
