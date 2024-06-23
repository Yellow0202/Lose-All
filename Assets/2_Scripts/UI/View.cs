using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);

        //// ���� GameObject�� ������ Camera ������Ʈ�� �������� �ڵ�
        //Camera cam = GetComponent<Camera>();

        //// ���� ī�޶��� ����Ʈ ������ �������� �ڵ�
        //Rect viewportRect = cam.rect;

        //// ���ϴ� ���� ���� ������ ����ϴ� �ڵ�
        //float screenAspectRatio = (float)Screen.width / Screen.height;
        //float targetAspectRatio = 16f / 9f; // ���ϴ� ���� ���� ���� (��: 16:9)

        //// ȭ�� ���� ���� ������ ���� ����Ʈ ������ �����ϴ� �ڵ�
        //if (screenAspectRatio < targetAspectRatio)
        //{
        //    // ȭ���� �� '����'�� (���ΰ� �� ��ٸ�) ���θ� �����ϴ� �ڵ�
        //    viewportRect.height = screenAspectRatio / targetAspectRatio;
        //    viewportRect.y = (1f - viewportRect.height) / 2f;
        //}
        //else
        //{
        //    // ȭ���� �� '�д�'�� (���ΰ� �� ��ٸ�) ���θ� �����ϴ� �ڵ�.
        //    viewportRect.width = targetAspectRatio / screenAspectRatio;
        //    viewportRect.x = (1f - viewportRect.width) / 2f;
        //}

        //// ������ ����Ʈ ������ ī�޶� �����ϴ� �ڵ�
        //cam.rect = viewportRect;

        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //int width = 1920;
        //int height = 1080;
        //float res = (float)width / height;

        //int deviceWidth = Screen.width;
        //int deviceHeight = Screen.height;
        //float deviceRes = (float)deviceWidth / deviceHeight;

        //// SetResolution �Լ� ����� ����ϱ�
        //Screen.SetResolution(width, (int)(((float)deviceHeight / deviceWidth) * width), true);

        //if (res < deviceRes)
        //{
        //    // ����� �ػ� �� �� ū ���
        //    float newWidth = res / deviceRes;
        //    Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        //}
        //else
        //{
        //    // ������ �ػ� �� �� ū ���
        //    float newHeight = deviceRes / res;
        //    Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetResolution(); // �ʱ⿡ ���� �ػ� ����
        //setupCamera();
        OnPreCull();
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }

    /* �ػ� �����ϴ� �Լ� */
    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

    private void setupCamera()
    {
        //���� ȭ�� ����
        float targetWidthAspect = 16.0f;

        //���� ȭ�� ����
        float targetHeightAspect = 9.0f;

        //���� ī�޶�
        Camera mainCamera = Camera.main;

        mainCamera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        // 16_10�������� ���ΰ� �F�ٸ�(4_3 ����)
        // 16_10�������� ���ΰ� ª�ٸ�(16_9 ����)
        // ���� ������ 0���� ������ش�
        if (heightRatio > widthRatio)
            widthtadd = 0.0f;
        else
            heightadd = 0.0f;


        mainCamera.rect = new Rect(
            mainCamera.rect.x + Math.Abs(widthtadd),
            mainCamera.rect.y + Math.Abs(heightadd),
            mainCamera.rect.width + (widthtadd * 2),
            mainCamera.rect.height + (heightadd * 2));
    }


}
