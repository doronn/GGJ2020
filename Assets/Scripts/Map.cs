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
            _lanePrefabWidth = GetRendererWidth(lanePrefab.GetComponent<SpriteRenderer>());
            _separationLinePrefabWidth = GetRendererWidth(separationLinePrefab.GetComponent<SpriteRenderer>());
            _leftSidewalkPrefabWidth = GetRendererWidth(leftSidewalkPrefab.GetComponent<SpriteRenderer>());
            _rightSidewalkPrefabWidth = GetRendererWidth(rightSidewalkPrefab.GetComponent<SpriteRenderer>());
            Start2();
        }
        
        //public void Generate(int lanesNum)
        public void Start2()
        {
            var lanesNum = 3;
            // we must make sure that the position point is indeed set as the middle of the GameObject on Unity
            var gameObjectTransform = transform;
            var middlePosition = (Vector2) gameObjectTransform.position;
            var totalWidth = lanesNum * _lanePrefabWidth + (lanesNum - 1) * _separationLinePrefabWidth + _leftSidewalkPrefabWidth + _rightSidewalkPrefabWidth;
            var leftmostDrawPosition = middlePosition - new Vector2(totalWidth / 2, 0);
            
            var lastObject = Instantiate(leftSidewalkPrefab, leftmostDrawPosition + Vector2.right * _leftSidewalkPrefabWidth / 2, Quaternion.identity, gameObjectTransform);
            
            for (var i = 0; i < lanesNum; i++)
            {
                lastObject = InstantiateNextTo(lastObject, lanePrefab);
                
                if (i < lanesNum - 1)
                {
                    lastObject = InstantiateNextTo(lastObject, separationLinePrefab);
                }
            }
            
            InstantiateNextTo(lastObject, rightSidewalkPrefab);
        }

        private static float GetRendererWidth(Renderer spriteRenderer)
        {
            return spriteRenderer.bounds.size.x * spriteRenderer.transform.localScale.x;
        }
        
        private GameObject InstantiateNextTo(GameObject original, GameObject prefab)
        {
            var m = original.GetComponentInChildren<Renderer>();
            var t = original.transform;
            var originalWidth = m.bounds.size.x * t.localScale.x;
            return Instantiate(prefab, t.position + new Vector3(originalWidth,0,0), Quaternion.identity, transform);
        }
    }
}