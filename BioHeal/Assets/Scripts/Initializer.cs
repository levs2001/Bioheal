using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Initializer : ScriptableObject
    {
        interface IInitFromDB
        {
            void InitFromDB();
        }
    }
}