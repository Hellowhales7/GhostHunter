using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlanelimiter : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public int maxPlaneCount = 10; // �ִ� Plane ��

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
        // ���� Ȱ��ȭ�� ARPlane�� ���� ����
        int currentPlaneCount = arPlaneManager.trackables.count;

        // ���ο� �÷��� �߰� �� �ִ� ���� �ʰ��ϸ� ó��
        if (currentPlaneCount > maxPlaneCount)
        {
            RemoveExtraPlanes(currentPlaneCount - maxPlaneCount);
        }
    }

    private void RemoveExtraPlanes(int planesToRemove)
    {
        // planesToRemove ���� 0 ������ ��� �ٷ� ����
        if (planesToRemove <= 0)
            return;

        // ARPlane�� ������ ������ŭ �ݺ��Ͽ� ����
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
