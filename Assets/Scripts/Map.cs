using System.Collections.Generic;
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

        public List<LaneController> laneControllers;
        
        private void Awake()
        {
            _lanePrefabWidth = GetRendererWidth(lanePrefab.GetComponentInChildren<SpriteRenderer>());
            _separationLinePrefabWidth = GetRendererWidth(separationLinePrefab.GetComponent<SpriteRenderer>());
            _leftSidewalkPrefabWidth = GetRendererWidth(leftSidewalkPrefab.GetComponent<SpriteRenderer>());
            _rightSidewalkPrefabWidth = GetRendererWidth(rightSidewalkPrefab.GetComponent<SpriteRenderer>());
            laneControllers = new List<LaneController>();
        }
        
        public void Generate(int lanesNum)
        {
            // we must make sure that the position point is indeed set as the middle of the GameObject on Unity
            var gameObjectTransform = transform;
            var middlePosition = (Vector2) gameObjectTransform.position;
            var totalWidth = lanesNum * _lanePrefabWidth + (lanesNum - 1) * _separationLinePrefabWidth + _leftSidewalkPrefabWidth + _rightSidewalkPrefabWidth;
            var leftmostDrawPosition = middlePosition - new Vector2(totalWidth / 2, 0);
            
            var lastObject = Instantiate(leftSidewalkPrefab, leftmostDrawPosition + new Vector2(_leftSidewalkPrefabWidth / 2, 0), Quaternion.identity, gameObjectTransform);
            
            for (var i = 0; i < lanesNum; i++)
            {
                lastObject = InstantiateNextTo(lastObject, lanePrefab);
                laneControllers.Add(lastObject.GetComponentInChildren<LaneController>());
                
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
            var originalRenderer = original.GetComponentInChildren<Renderer>();
            var originalTransform = original.transform;
            var originalWidth = originalRenderer.bounds.size.x * originalTransform.localScale.x;
            return Instantiate(prefab, originalTransform.position + new Vector3(originalWidth,0,0), Quaternion.identity, transform);
        }
    }
}