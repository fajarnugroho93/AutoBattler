using System;
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

namespace SpaceKomodo.Utilities
{
    [RequireComponent(typeof(BoxCollider))]
    public class WorldLayoutGroup : MonoBehaviour
    {
        [Header("Layout Settings")]
        public LayoutAxis axis = LayoutAxis.Y;
        public bool isInverted;
        public LayoutAlignment alignment = LayoutAlignment.Start;
        
        [Header("Spacing Settings")]
        public Vector3 padding = Vector3.zero;
        public float spacing = 0.1f;

        private BoxCollider parentCollider;
        private readonly List<GameObject> layoutChildren = new List<GameObject>();

        private void Awake()
        {
            parentCollider = GetComponent<BoxCollider>();
        }

        private void OnValidate()
        {
            parentCollider = GetComponent<BoxCollider>();
        }

        public void RecalculateLayout()
        {
            if (parentCollider == null)
            {
                Debug.LogError("WorldLayoutGroup requires a BoxCollider on the same GameObject.");
                return;
            }

            layoutChildren.Clear();
            
            foreach (Transform child in transform)
            {
                var childCollider = child.GetComponent<BoxCollider>();
                if (childCollider != null)
                {
                    layoutChildren.Add(child.gameObject);
                }
            }

            if (layoutChildren.Count == 0)
                return;

            PositionChildren();
        }

        private void PositionChildren()
        {
            var parentSize = parentCollider.size;
            var parentMin = -parentSize / 2f + padding;
            var parentMax = parentSize / 2f - padding;
            
            var layoutAxisLength = GetLayoutAxisLength(parentMin, parentMax);
            
            var currentOffset = isInverted ? parentMax[GetAxisIndex()] : parentMin[GetAxisIndex()];
            var totalChildrenSize = CalculateTotalChildrenSize();
            var freeSpace = layoutAxisLength - totalChildrenSize;
            
            AdjustStartOffsetForAlignment(ref currentOffset, freeSpace);
            
            foreach (var childGameObject in layoutChildren)
            {
                var childCollider = childGameObject.GetComponent<BoxCollider>();
                var childSize = childCollider.size;
                
                var newPosition = childGameObject.transform.localPosition;
                
                if (isInverted)
                {
                    newPosition[GetAxisIndex()] = currentOffset - childSize[GetAxisIndex()] / 2f;
                    currentOffset -= childSize[GetAxisIndex()] + spacing;
                }
                else
                {
                    newPosition[GetAxisIndex()] = currentOffset + childSize[GetAxisIndex()] / 2f;
                    currentOffset += childSize[GetAxisIndex()] + spacing;
                }
                
                PositionChildOnNonLayoutAxes(ref newPosition, parentMin, parentMax);
                
                childGameObject.transform.localPosition = newPosition;
            }
        }

        private float CalculateTotalChildrenSize()
        {
            var totalSize = 0f;
            var axisIndex = GetAxisIndex();
            
            for (var i = 0; i < layoutChildren.Count; i++)
            {
                var childCollider = layoutChildren[i].GetComponent<BoxCollider>();
                totalSize += childCollider.size[axisIndex];
                
                if (i < layoutChildren.Count - 1)
                    totalSize += spacing;
            }
            
            return totalSize;
        }

        private void AdjustStartOffsetForAlignment(ref float offset, float freeSpace)
        {
            switch (alignment)
            {
                case LayoutAlignment.Center:
                    if (isInverted)
                        offset -= freeSpace / 2f;
                    else
                        offset += freeSpace / 2f;
                    break;
                    
                case LayoutAlignment.End:
                    if (isInverted)
                        offset -= freeSpace;
                    else
                        offset += freeSpace;
                    break;
                    
                case LayoutAlignment.Start:
                default:
                    break;
            }
        }

        private void PositionChildOnNonLayoutAxes(ref Vector3 position, Vector3 parentMin, Vector3 parentMax)
        {
            var axisIndex = GetAxisIndex();
            
            for (var i = 0; i < 3; i++)
            {
                if (i == axisIndex)
                    continue;
                
                var parentCenter = (parentMin[i] + parentMax[i]) / 2f;
                position[i] = parentCenter;
            }
        }

        private float GetLayoutAxisLength(Vector3 min, Vector3 max)
        {
            var axisIndex = GetAxisIndex();
            return max[axisIndex] - min[axisIndex];
        }

        private int GetAxisIndex()
        {
            switch (axis)
            {
                case LayoutAxis.X: return 0;
                case LayoutAxis.Y: return 1;
                case LayoutAxis.Z: return 2;
                default: return 1;
            }
        }
        
        public enum LayoutAxis
        {
            X,
            Y,
            Z
        }

        public enum LayoutAlignment
        {
            Start,
            Center,
            End
        }
        
#if UNITY_EDITOR
        [Button]
        public void DoRecalculateLayout()
        {
            RecalculateLayout();
        }
#endif
    }
}