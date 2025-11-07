using UnityEngine;

public interface IShapeGenerator
{
    GameObject Generate(Vector3 position);
    void Erase();
}
