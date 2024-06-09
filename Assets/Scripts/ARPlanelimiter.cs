using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlanelimiter : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public int maxPlaneCount = 10; // 최대 Plane 수

    private void OnEnable()
    {
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // 현재 활성화된 ARPlane의 수를 측정
        int currentPlaneCount = arPlaneManager.trackables.count;

        // 새로운 플레인 추가 시 최대 값을 초과하면 처리
        if (currentPlaneCount > maxPlaneCount)
        {
            RemoveExtraPlanes(currentPlaneCount - maxPlaneCount);
        }
    }

    private void RemoveExtraPlanes(int planesToRemove)
    {
        // planesToRemove 값이 0 이하인 경우 바로 종료
        if (planesToRemove <= 0)
            return;

        // ARPlane을 제거할 개수만큼 반복하여 제거
        int i = 0;
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            if(i > maxPlaneCount)
            {
                plane.transform.localScale = Vector3.zero;
            }
            i++;
        }
    }

}
