using UnityEngine;

public interface ShapeGen
{
    Mesh GenerateMesh(int segments = 0);
}
