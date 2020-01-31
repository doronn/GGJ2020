using UnityEngine;

namespace UnityTemplateProjects
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private GameObject lanePrefab; 
        [SerializeField] private GameObject separationLinePrefab;
        [SerializeField] private GameObject leftSidewalkPrefab;
        [SerializeField] private GameObject rightSidewalkPrefab;
        private float _lanePrefabWidth;
        private float _separationLinePrefabWidth;
        private float _leftSidewalkPrefabWidth;
        private float _rightSidewalkPrefabWidth;
        
        private void Start()
        {
            _lanePrefabWidth = lanePrefab.GetComponent<Sprite>().bounds.size.x;
            _separationLinePrefabWidth = separationLinePrefab.GetComponent<Sprite>().bounds.size.x;
            _leftSidewalkPrefabWidth = leftSidewalkPrefab.GetComponent<Sprite>().bounds.size.x;
            _rightSidewalkPrefabWidth = rightSidewalkPrefab.GetComponent<Sprite>().bounds.size.x;
        }
        
        public void Generate(int lanesNum)
        {
            // we must make sure that the position point is indeed set as the middle of the GameObject on Unity
            var middlePosition = (Vector2) transform.position;
            var totalWidth = lanesNum * _lanePrefabWidth + (lanesNum - 1) * _separationLinePrefabWidth + _leftSidewalkPrefabWidth + _rightSidewalkPrefabWidth;
            var leftmostDrawPoint = middlePosition - new Vector2(totalWidth / 2, 0);
            
            DrawGameObject(leftSidewalkPrefab, leftmostDrawPoint, _leftSidewalkPrefabWidth);
            
            for (var i = 0; i < lanesNum; i++)
            {
                DrawGameObject(lanePrefab, leftmostDrawPoint, _lanePrefabWidth);
                
                if (i < lanesNum - 1)
                {
                    DrawGameObject(separationLinePrefab, leftmostDrawPoint, _separationLinePrefabWidth);
                }
            }
            
            DrawGameObject(rightSidewalkPrefab, leftmostDrawPoint, _rightSidewalkPrefabWidth);
        }
        
        private static void DrawGameObject(GameObject obj, Vector3 position, float width)
        {
            Instantiate(obj, position, Quaternion.identity);
            position.Set(position.x + width, position.y, position.z);
        }
    }
}