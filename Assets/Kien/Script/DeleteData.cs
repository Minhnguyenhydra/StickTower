using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeleteData", menuName = "DeleteData")]
public class DeleteData : ScriptableObject
{
    public InfoDelete[] infoDelete;
    [System.Serializable]
    public struct InfoDelete
    {
        public ResourceSprite resourceSprite;
        [System.Serializable]
        public struct ResourceSprite
        {
            public Sprite[] sp;
            public Sprite[] iconSp;
        }
    }
}
